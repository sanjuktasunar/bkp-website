using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using web.Web.Entity.Dto;
using web.Web.Entity.Infrastructure;
using web.Web.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Web.Services.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> Filter(int? ApprovalStatus, int? FormStatus, int? ReferenceId);
        Task<MemberDto> GetMemberByIdAsync(int? id);
        Task<Response> Insert(MemberDto dto);
        Task<MemberDto> GetMemberByReferalCode(string ReferalCode);
        Task<Response> ApproveMember(int MemberId, int AccountHeadId);
        Task<Response> RejectMember(int MemberId, string remarks);
        Task<IEnumerable<DropdownDto>> GetRefernceMemberDropdown();
    }

    public class MemberService:IMemberService
    {
        private readonly Repository<Member> _repository;
        private readonly Repository<Address> _addressRepository;
        private readonly Repository<UserDocuments> _documentRepository;
        private readonly Repository<BankDeposit> _bankDepositRepository;
        private readonly Repository<Users> _userRepository;
        private readonly IShareTypesService _shareTypesService;
        private readonly IUsersService _usersService;
        private readonly MessageClass _messageClass;
        private readonly ImageSettings _imageSettings;
        private readonly SqlConnectionDetails _sql;
        private readonly DateSettings dateSettings;
        public MemberService(IShareTypesService shareTypesService,
            IUsersService usersService)
        {
            _repository = new Repository<Member>();
            _addressRepository = new Repository<Address>();
            _documentRepository = new Repository<UserDocuments>();
            _bankDepositRepository = new Repository<BankDeposit>();
            _shareTypesService = shareTypesService;
            _messageClass = new MessageClass();
            _imageSettings = new ImageSettings();
            _sql = _repository.GetSqlTransactionDetails();
            dateSettings = new DateSettings();
            _usersService = usersService;
            _userRepository = new Repository<Users>();
        }
        public async Task<IEnumerable<MemberDto>> Filter(int? ApprovalStatus, int? FormStatus, int? ReferenceId)
        {
            if (ApprovalStatus == null)
                ApprovalStatus = 1;

            if (ApprovalStatus == 2)
                FormStatus = null;

            if (FormStatus == 0)
                FormStatus = null;

            if (ReferenceId == 0)
                ReferenceId = null;

            var obj = await _repository.StoredProcedureAsync<MemberDto>("[dbo].[FilterMember]"
               , new
               {
                   ApprovalStatus,
                   FormStatus,
                   ReferenceId
               });
            return obj;
        }

        public async Task<MemberDto> GetMemberForUpdate(int? MemberId)
        {
            string query = "select * from dbo.[Member] " +
                            "where MemberId=@MemberId";

            var obj = (await _repository.QueryAsync<MemberDto>(query,new { MemberId })).FirstOrDefault();
            return obj;
        }

        public async Task<MemberDto> GetMemberByIdAsync(int? id)
        {
            var obj = (await _repository.QueryAsync<MemberDto>("SELECT * FROM MemberView " +
                "WHERE MemberId=@id", new { id })).FirstOrDefault();
            if (obj.PermanentIsOutsideNepal == true)
            {
                obj.PermanentFullAddress = obj.PermanentAddress + "," + obj.PermanentCountryName;
            }
            else
            {
                obj.PermanentFullAddress = obj.PermanentMunicipalityName + "-" + obj.PermanentWardNumber + "," + obj.PermanentDistrictName;
            }
            if (obj.TemporaryIsOutsideNepal == true)
            {
                obj.TemporaryFullAddress = obj.TemporaryAddress + "," + obj.TemporaryCountryName;
            }
            else
            {
                obj.TemporaryFullAddress = obj.TemporaryMunicipalityName + "-" + obj.TemporaryWardNumber + "," + obj.TemporaryDistrictName;
            }
            return obj;
        }

        public async Task<MemberDto> GetMemberByReferalCode(string ReferalCode)
        {
            string query = "select * from dbo.[Member] " +
                            "where ReferalCode=@ReferalCode and IsActive=1 and " +
                            "ApprovalStatus=2";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { ReferalCode })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(MemberDto dto)
        {
            var resp = new Response();
            resp.messageType = "error";
            try
            {
                dto.DateOfBirthAD = dateSettings.ConvertToEnglishDate(dto.DateOfBirthBS);
                var Referal = await GetMemberByReferalCode(dto.ReferenceReferalCode?.Trim());
                dto.ReferenceId = Referal?.ReferenceId;

                resp = MemberValidation(dto);
                if (resp.messageType == "error")
                {
                    return resp;
                }
                var member = dto.ToEntity();
                member.ReferenceId = Referal.MemberId;
                member.CreatedBy = _repository.UserIdentity();
                member.CreatedDate = DateTime.Now;
                member.FormStatus = FormStatus.Complete;
                member.ApprovalStatus = ApprovalStatus.UnApproved;
                member.IsActive = false;
                int MemberId = await _repository.InsertAsync(member,_sql.conn,_sql.trans);

                var address = dto.ToMemberAddress();
                address.MemberId = MemberId;
                await _addressRepository.InsertAsync(address, _sql.conn, _sql.trans);

                dto.MemberId = MemberId;
                var documentResp = await SaveUserDocument(dto);
                if (documentResp.messageType == "error")
                {
                    _sql.trans.Rollback();
                    return documentResp;
                }
                var bankDepositResp = await SaveBankDeposit(dto);
                if (documentResp.messageType == "error")
                {
                    _sql.trans.Rollback();
                    return bankDepositResp;
                }
                resp = _messageClass.SaveMessage(MemberId);
                _sql.trans.Commit();
            }
            catch(SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> Update(MemberDto dto)
        {
            var resp = new Response();
            try
            {
                var Referal = await GetMemberByReferalCode(dto.ReferenceReferalCode);
                if (Referal == null)
                {
                    resp.message = "Invalid Referal Code";
                    return resp;
                }

                var obj = await GetMemberForUpdate(dto.MemberId);
                if (obj == null)
                    resp = _messageClass.NotFoundMessage();

                var member = dto.ToEntity();
                member.UpdatedBy = _repository.UserIdentity();
                member.UpdatedDate = DateTime.Now;
                member.FormStatus = obj.FormStatus;
                member.ApprovalStatus = obj.ApprovalStatus;
                member.IsActive = obj.IsActive;
                member.ReferalCode = obj.ReferalCode;
                int MemberId = await _repository.InsertAsync(member, _sql.conn, _sql.trans);

                var address = dto.ToMemberAddress();
                address.MemberId = MemberId;
                await _addressRepository.InsertAsync(address, _sql.conn, _sql.trans);

                var documentResp = await SaveUserDocument(dto);
                if (documentResp.messageType == "error")
                {
                    _sql.trans.Rollback();
                    return documentResp;
                }
                var bankDepositResp = await SaveBankDeposit(dto);
                if (documentResp.messageType == "error")
                {
                    _sql.trans.Rollback();
                    return bankDepositResp;
                }
                resp = _messageClass.SaveMessage(MemberId);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.messageType = "error";
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public Response MemberValidation(MemberDto dto)
        {
            var resp = new Response();
            resp.messageType = "success";
            var messageList = new List<string>();
            if (dto.ReferenceId == null)
            {
                resp.messageType = "error";
                messageList.Add("Invalid Referal Code");
            }
            if (dto.DateOfBirthAD == null)
            {
                resp.messageType = "error";
                messageList.Add("Invalid Date Of Birth");
            }
            if (dto.CitizenshipNumber.Substring(dto.CitizenshipNumber.Length - 1) == "/")
            {
                resp.messageType = "error";
                messageList.Add("Invalid Citizenship Number");
            }
            if (string.IsNullOrEmpty(dto.MemberPhotoString))
            {
                resp.messageType = "error";
                messageList.Add("Please select Photo");
            }
            if (string.IsNullOrEmpty(dto.CitizenshipFrontImageString))
            {
                resp.messageType = "error";
                messageList.Add("Please select Citizenship Front");
            }
            if (string.IsNullOrEmpty(dto.CitizenshipBackImageString))
            {
                resp.messageType = "error";
                messageList.Add("Please select Citizenship Back");
            }
            if (string.IsNullOrEmpty(dto.VoucherImageString))
            {
                resp.messageType = "error";
                messageList.Add("Please select Payment Voucher or Screenshot");
            }
            
            resp.messageList = messageList;
            return resp;
        }

        public async Task<Response> SaveUserDocument(MemberDto dto)
        {
            var resp = new Response();
            try
            {
                var document = dto.ToDocumentEntity();
                var userDocument = (await _documentRepository.QueryAsync<UserDocumentDto>("select * from dbo.[UserDocuments] where MemberId=@MemberId", new { MemberId = dto.MemberId })).FirstOrDefault();
                if (userDocument == null)
                {
                    document.UserDocumentId = await _documentRepository.InsertAsync(document, _sql.conn, _sql.trans);
                }
                
                string extension = ".jpg";
                if (dto.CitizenshipFrontImageString != null)
                {
                    var citizenFront = _imageSettings.SaveImage(dto.CitizenshipFrontImageString, "CF-" + dto.MemberId);

                    if (citizenFront)
                        document.CitizenshipFront = "CF-" + dto.MemberId + extension;
                }

                if (dto.CitizenshipBackImageString != null)
                {
                    var citizenBack = _imageSettings.SaveImage(dto.CitizenshipBackImageString, "CB-" + dto.MemberId);
                    if (citizenBack)
                        document.CitizenshipBack = "CB-" + dto.MemberId + extension;
                }
                if (dto.MemberPhotoString != null)
                {
                    var photo = _imageSettings.SaveImage(dto.MemberPhotoString, "MP-" + dto.MemberId);
                    if (photo)
                        document.Photo = "MP-" + dto.MemberId + extension;
                }

                dto.UserDocumentId = _documentRepository.Update(document, _sql.conn, _sql.trans);
                resp.messageType = "success";
            }
            catch(Exception ex)
            {
                resp.messageType = "error";
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<Response> SaveBankDeposit(MemberDto dto)
        {
            var resp = new Response();
            try
            {
                var bankDeposit = dto.ToBankDeposit();
                var bankDepositDto = (await _bankDepositRepository.QueryAsync<BankDepositDto>("select * from dbo.[BankDeposit] where MemberId=@MemberId", new { MemberId = dto.MemberId })).FirstOrDefault();
                if (bankDepositDto == null)
                {
                    bankDeposit.Id = await _bankDepositRepository.InsertAsync(bankDeposit, _sql.conn, _sql.trans);
                }
                string extension = ".jpg";
                if (dto.VoucherImageString != null)
                {
                    var voucher = _imageSettings.SaveImage(dto.VoucherImageString, "VI-" + dto.MemberId);
                    if (voucher)
                        bankDeposit.VoucherImage = "VI-" + dto.MemberId + extension;
                }
                dto.Id = _bankDepositRepository.Update(bankDeposit, _sql.conn, _sql.trans);
                resp.messageType = "success";
            }
            catch (Exception ex)
            {
                resp.messageType = "error";
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<Response> ApproveMember(int MemberId, int AccountHeadId)
        {
            var obj = await GetMemberForUpdate(MemberId);
            if (obj is null)
                return null;

            var bankDeposit = await GetMemberBankDepositById(MemberId);
            var response = new Response();
            response.messageType = "error";
            try
            {
                if (obj.ApprovalStatus == ApprovalStatus.Rejected && obj.FormStatus == FormStatus.Complete)
                {
                    obj.ApprovalStatus = ApprovalStatus.Approved;
                    obj.IsActive = true;
                    await _repository.UpdateAsync(obj.ToEntity(), _sql.conn,_sql.trans);
                    response = _messageClass.SaveMessage(1);
                }
                else
                {
                    if (obj.FormStatus == FormStatus.Complete)
                    {
                        if ((obj.ApprovalStatus == ApprovalStatus.UnApproved))
                        {
                            obj.ApprovalStatus = ApprovalStatus.Approved;
                            obj.IsActive = true;
                            obj.ReferalCode = await GetReferalCode();
                            obj.ApprovedBy =_repository.UserIdentity();
                            obj.ApprovedDate = DateTime.Now;
                            await _repository.UpdateAsync(obj.ToEntity(), _sql.conn, _sql.trans);

                            if (bankDeposit != null)
                            {
                                bankDeposit.IsApproved = true;
                                bankDeposit.ApprovedDate = DateTime.Now;
                                bankDeposit.AccountHeadId = AccountHeadId;
                                await _bankDepositRepository.UpdateAsync(bankDeposit.ToEntity(),
                                    _sql.conn, _sql.trans);
                            }
                            response = _messageClass.SaveMessage(MemberId);
                        }
                        else if (obj.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            
                            response.message = "This Member has been approved already";
                        }
                      
                        else
                        {
                            response.message = "This member cannot be approved," +
                                "Please contact to admin";
                        }
                    }
                    else
                    {
                        response.message = "Incomplete Form <br /> " +
                            "Please complete all steps";
                    }
                }
                _sql.trans.Commit();
               
            }
            catch (SqlException ex)
            {
                response.message = ex.Message.ToString() ;
                _sql.trans.Rollback();
            }
            return response;
        }

        public async Task<Response> RejectMember(int MemberId, string remarks)
        {
            var obj = await GetMemberForUpdate(MemberId);
            if (obj is null)
                return null;

            var response = new Response();
            response.messageType = "error";
            try
            {
                if (obj.ApprovalStatus != ApprovalStatus.Rejected)
                {
                    obj.ApprovalStatus = ApprovalStatus.Rejected;
                    obj.ApprovalRemarks = remarks;
                    obj.ApprovedBy = _repository.UserIdentity();
                    obj.ApprovedDate = DateTime.Now;
                    obj.IsActive = false;
                    await _repository.UpdateAsync(obj.ToEntity());
                    response = _messageClass.SaveMessage(MemberId);
                }
                else
                {
                    response.message = "This Member has been rejected already+-1";
                }
            }
            catch (SqlException ex)
            {
                response.message = ex.Message.ToString();
            }

            return response;
        }

        public async Task<BankDepositDto> GetMemberBankDepositById(int memberId)
        {
            var obj = await _repository.QueryAsync<BankDepositDto>("SELECT * FROM [dbo].[BankDeposit] WHERE MemberId=@id", new { id = memberId });
            return obj.FirstOrDefault();
        }

        public async Task<string> GetReferalCode()
        {
            var members = (await _repository.QueryAsync<Member>("SELECT TOP 1 * FROM Member where ReferalCode is not null ORDER BY MemberId DESC"));
            DateTime currentDate = DateTime.Now;
            string referalCode = "";
            int i = 888;
            if (members.Count() > 0)
            {
                var number = members.FirstOrDefault().ReferalCode.Split('-');
                i = Convert.ToInt32(number[2]);
                i = i + 1;
            }
            referalCode = "REF-" + currentDate.Year + "-" + i;
            return referalCode;
        }

        public async Task<IEnumerable<DropdownDto>> GetRefernceMemberDropdown()
        {
            var obj = await _repository.StoredProcedureAsync<DropdownDto>("[dbo].[Sp_GetReferenceMembers]");
            return obj;
        }
    }
}