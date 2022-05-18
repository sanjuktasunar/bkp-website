﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using web.Model.Dto;
using web.Model.Entity;
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
        Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto);
        Task<IEnumerable<MemberDto>> FilterShareholder(MemberFilterDto filterDto);
        Task<MemberDto> GetMemberByIdAsync(int? id);
        Task<Response> Insert(MemberDto dto);
        Task<MemberDto> GetMemberByReferalCode(string ReferalCode);
        Task<Response> ApproveMember(int MemberId, int AccountHeadId, int ShareTypeId);
        Task<Response> RejectMember(int MemberId, string remarks);
        Task<IEnumerable<DropdownDto>> GetRefernceMemberDropdown();
        Task<MemberDto> GetMemberByCitizenshipNumber(string citizenshipNumber, int memberId);
        Task<MemberDto> GetMemberForUpdate(int? MemberId);
        Task<MemberDto> GetMemberWithRefernceDetailsForUpdate(int? MemberId);
        Task<MemberDto> GetMemberByMobileNumber(string mobileNumber, int memberId);
        Task<MemberDto> GetMemberByEmail(string emailAddress, int memberId);
        Task<MemberDto> GetAddressByMemberId(int memberId);
        Task<BankDepositDto> GetMemberBankDepositById(int memberId);
        Task<UserDocumentDto> GetMemberDocuments(int memberId);
        Task<Response> AddMemberToShareholder(ShareholderDto dto);
        Task<ShareholderDto> GetShareholderByMemberId(int memberId);
        Task<ShareholderDto> GetShareholderByShareholderId(int id);
        Task<Response> ModifyShareholder(ShareholderDto dto);
        Task<Response> DeleteShareholder(int id);
    }

    public class MemberService:IMemberService
    {
        private readonly Repository<Member> _repository;
        private readonly Repository<Address> _addressRepository;
        private readonly Repository<UserDocuments> _documentRepository;
        private readonly Repository<BankDeposit> _bankDepositRepository;
        private readonly Repository<Shareholder> _shareholderRepository;
        private readonly Repository<Users> _userRepository;
        private readonly IShareTypesService _shareTypesService;
        private readonly IUsersService _usersService;
        private readonly MessageClass _messageClass;
        private readonly ImageSettings _imageSettings;
        private readonly SqlConnectionDetails _sql;
        private readonly DateSettings dateSettings;
        private readonly ImageSettings imageSettings;
        
        public MemberService(IShareTypesService shareTypesService,
            IUsersService usersService)
        {
            _repository = new Repository<Member>();
            _addressRepository = new Repository<Address>();
            _documentRepository = new Repository<UserDocuments>();
            _bankDepositRepository = new Repository<BankDeposit>();
            _shareholderRepository = new Repository<Shareholder>();
            _shareTypesService = shareTypesService;
            _messageClass = new MessageClass();
            _imageSettings = new ImageSettings();
            _sql = _repository.GetSqlTransactionDetails();
            dateSettings = new DateSettings();
            _usersService = usersService;
            _userRepository = new Repository<Users>();
            imageSettings = new ImageSettings();
        }
        public async Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto)
        {
            if (filterDto.ApprovalStatus == 2)
                filterDto.FormStatus = null;

            var obj = await _repository.StoredProcedureAsync<MemberDto>("[dbo].[FilterMember]"
               , new
               {
                   ApprovalStatus=filterDto.ApprovalStatus,
                   FormStatus=filterDto.FormStatus,
                   ReferenceId=filterDto.ReferenceId,
                   AgentId = filterDto.AgentId,
                   ShareTypeId=filterDto.ShareTypeId
               });
            return obj;
        }

        public async Task<IEnumerable<MemberDto>> FilterShareholder(MemberFilterDto filterDto)
        {
            var obj = await _repository.StoredProcedureAsync<MemberDto>("[dbo].[FilterShareholder]"
               , new
               {
                   ReferenceId = filterDto.ReferenceId,
                   AgentId = filterDto.AgentId,
                   ShareTypeId = filterDto.ShareTypeId,
                   SearchQuery=filterDto.SearchQuery,
                   Code=filterDto.Code,
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

        public async Task<MemberDto> GetMemberWithRefernceDetailsForUpdate(int? MemberId)
        {
            string query = "select B.*,D.VoucherImage, " +
                "(case when isnull(B.ReferenceId,0)=0 then AG.LicenceNumber " +
                "else B1.ReferalCode end)  as ReferenceReferalCode " +
                "from dbo.[Member] B " +
                "left join dbo.Member AS B1 ON B.ReferenceId = B1.MemberId " +
                "left join dbo.Agent AS AG on AG.AgentId=B.AgentId " +
                "left join dbo.BankDeposit AS D on D.MemberId=B.MemberId " +
                "where B.MemberId=@MemberId";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { MemberId })).FirstOrDefault();
            return obj;
        }

        public async Task<MemberDto> GetMemberByIdAsync(int? id)
        {
            var obj = (await _repository.QueryAsync<MemberDto>("SELECT * " +
                "FROM dbo.MemberDetailView " +
                "WHERE MemberId=@id", new { id })).FirstOrDefault();

            obj.PermanentFullAddress = obj.PermanentMunicipalityName + "-" + obj.PermanentWardNumber + "," + obj.PermanentDistrictName;

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
            string query = "select MemberId,ReferalCode,ReferenceId from dbo.[Member] " +
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

        public async Task<Response> ApproveMember(int MemberId, int AccountHeadId,int ShareTypeId)
        {
            var response = new Response();
            response.messageType = "error";
            var obj = await GetMemberForUpdate(MemberId);
            if (obj is null)
            {
                response.message = "Invalid Member";
            }

            if (AccountHeadId == 0)
            {
                response.message = "Please Select Account Head";
                return response;
            }

            if (ShareTypeId == 0)
            {
                response.message = "Please Select Share Type";
                return response;
            }

            var bankDeposit = await GetMemberBankDepositById(MemberId);
            var shareType = await _repository.QueryAsync<ShareTypesDto>("select * from " +
                "dbo.ShareTypes where ShareTypeId=@id and Status=1",new { id=ShareTypeId });
            
            try
            {
                obj.ShareTypeId = ShareTypeId;
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
                            obj.MemberCode = await GetMemberCode();
                            obj.ApprovedBy =_repository.UserIdentity();
                            obj.ApprovedDate = DateTime.Now;
                            await _repository.UpdateAsync(obj.ToEntity(), _sql.conn, _sql.trans);

                            if (bankDeposit != null)
                            {
                                bankDeposit.IsApproved = true;
                                bankDeposit.ApprovedDate = DateTime.Now;
                                bankDeposit.AccountHeadId = AccountHeadId;
                                if (shareType.Count() > 0)
                                {
                                    bankDeposit.Amount = Convert.ToDecimal(shareType
                                        .FirstOrDefault().RegistrationAmount);
                                }
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
            var members = (await _repository.QueryAsync<Member>("SELECT TOP 1 " +
                "ReferalCode,MemberId FROM Member where ReferalCode is not null ORDER BY ReferalCode DESC"));
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

        public async Task<string> GetMemberCode()
        {
            var members = (await _repository.QueryAsync<Member>("SELECT TOP 1 MemberCode," +
                "MemberId FROM Member where MemberCode is not null ORDER BY MemberCode DESC"));
            DateTime currentDate = DateTime.Now;
            string memberCode = "";
            int i = 78;
            if (members.Count() > 0)
            {
                var number = members.FirstOrDefault().MemberCode.Split('-');
                i = Convert.ToInt32(number[2]);
                i = i + 1;
            }
            memberCode = "BKP-" + currentDate.Year + "-" + i;
            return memberCode;
        }

        public async Task<IEnumerable<DropdownDto>> GetRefernceMemberDropdown()
        {
            var obj = await _repository.StoredProcedureAsync<DropdownDto>("[dbo].[Sp_GetReferenceMembers]");
            return obj;
        }

        public async Task<MemberDto> GetMemberByCitizenshipNumber(string citizenshipNumber,int memberId)
        {
            string _sql = "select * from dbo.[Member] " +
                "where CitizenshipNumber=@citizenshipNumber " +
                "or MemberId=@memberId";
            var obj = await _repository.QueryAsync<MemberDto>(_sql, new { citizenshipNumber,memberId });
            return obj.FirstOrDefault();
        }

        public async Task<MemberDto> GetMemberByMobileNumber(string mobileNumber,int memberId)
        {
            string _sql = "select * from dbo.[Member] where MobileNumber=@mobileNumber " +
                "and MemberId!=@memberId";
            var obj = await _repository.QueryAsync<MemberDto>(_sql, new { mobileNumber,memberId });
            return obj.FirstOrDefault();
        }
        public async Task<MemberDto> GetMemberByEmail(string emailAddress,int memberId)
        {
            string _sql = "select * from dbo.[Member] where Email=@emailAddress " +
                "and Email is not null and Email!='' and MemberId!=@memberId";
            var obj = await _repository.QueryAsync<MemberDto>(_sql, new { emailAddress,memberId });
            return obj.FirstOrDefault();
        }

        public async Task<MemberDto> GetAddressByMemberId(int memberId)
        {
            string _sql = "select * from dbo.[Address] where MemberId=@memberId";
            var obj = await _repository.QueryAsync<MemberDto>(_sql, new { memberId });
            return obj.FirstOrDefault();
        }

        public async Task<UserDocumentDto> GetMemberDocuments(int memberId)
        {
            string _sql = "";
            //_sql = "select (case when @type=N'front' then u.CitizenshipFront when " +
            //   "@type=N'back' then u.CitizenshipBack " +
            //   "when @type = N'photo' then u.Photo when @type=N'payment' then " +
            //   "b.VoucherImage else '' end) as Photo " +
            //   "from dbo.UserDocuments u " +
            //   "left join dbo.BankDeposit b on b.MemberId=u.MemberId " +
            //   "where u.MemberId=@memberId";

            _sql = "select u.CitizenshipFront,u.CitizenshipBack,u.Photo,b.VoucherImage " +
               "from dbo.UserDocuments u " +
               "left join dbo.BankDeposit b on b.MemberId=u.MemberId " +
               "where u.MemberId=@memberId";

            var obj = (await _repository.QueryAsync<UserDocumentDto>(_sql, new { memberId })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> AddMemberToShareholder(ShareholderDto dto)
        {
            var response = new Response();
            response.messageType = "error";
            var memberDto = await GetMemberForUpdate(dto.MemberId);
            if (memberDto is null)
            {
                response.message = "Invalid member!!!";
                return response;
            }
            if (dto.TotalKitta <= 0)
            {
                response.message = "Please enter share kitta!!!";
                return response;
            }
            try
            {
                if (memberDto.ApprovalStatus != ApprovalStatus.Approved)
                {
                    response.message = "Please approved this member then add to shareholder!!!!";
                }
                else
                {
                    var shareholderDto = await GetShareholderByMemberId(dto.MemberId);
                    if (shareholderDto != null)
                    {
                        response.message = "Shareholder already exists";
                        return response;
                    }
                    var entity = dto.ToEntity();
                    entity.IsActive = dto.Status?1:0;
                    entity.ApprovedDate = DateTime.Now;
                    int ShareholderId = await _shareholderRepository.InsertAsync(entity, _sql.conn, _sql.trans);

                    var member = memberDto.ToEntity();
                    member.ShareholderId = ShareholderId;
                    member.ShareTypeId = entity.ShareTypeId;
                    await _repository.UpdateAsync(member, _sql.conn, _sql.trans);
                    response = _messageClass.SaveMessage(ShareholderId);
                }
                _sql.trans.Commit();

            }
            catch (SqlException ex)
            {
                response.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return response;
        }

        public async Task<ShareholderDto> GetShareholderByMemberId(int memberId)
        {
            string query = "select top 1 * from dbo.[Shareholder] where MemberId=@memberId";
            var obj=(await _shareholderRepository.QueryAsync<ShareholderDto>
                (query,new { memberId })).FirstOrDefault();

            return obj;
        }

        public async Task<ShareholderDto> GetShareholderByShareholderId(int id)
        {
            string query = "select top 1 * from dbo.[Shareholder] where ShareHolderId=@id";
            var obj = (await _shareholderRepository.QueryAsync<ShareholderDto>
                (query, new { id })).FirstOrDefault();

            if (obj != null)
                obj.memberDto = await GetMemberForUpdate(obj.MemberId);

            return obj;
        }

        public async Task<Response> ModifyShareholder(ShareholderDto dto)
        {
            var response = new Response();
            response.messageType = "error";
            var shareholderDto = await GetShareholderByShareholderId(dto.ShareholderId);
            var memberDto = await GetMemberForUpdate(dto.MemberId);
            if (shareholderDto is null)
            {
                response.message = "Invalid shareholder!!!";
                return response;
            }
            if (dto.TotalKitta <= 0)
            {
                response.message = "Please enter share kitta!!!";
                return response;
            }
            try
            {
                var entity = shareholderDto.ToEntity();
                entity.ShareTypeId = dto.ShareTypeId;
                entity.TotalKitta = dto.TotalKitta;
                entity.IsActive = dto.Status ? 1 : 0;
                await _shareholderRepository.UpdateAsync(entity, _sql.conn, _sql.trans);

                if (memberDto != null)
                {
                    var member = memberDto.ToEntity();
                    member.ShareTypeId = dto.ShareTypeId;
                    await _repository.UpdateAsync(member, _sql.conn, _sql.trans);
                }
                response = _messageClass.SaveMessage(1);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                response.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return response;
        }

        public async Task<Response> DeleteShareholder(int id)
        {
            var response = new Response();
            response.messageType = "error";
            var shareholderDto = await GetShareholderByShareholderId(id);
            if (shareholderDto is null)
            {
                response.message = "Invalid shareholder!!!";
                return response;
            }
            var memberDto = await GetMemberForUpdate(shareholderDto.MemberId);
            try
            {
                if (memberDto != null)
                {
                    var member = memberDto.ToEntity();
                    member.ShareholderId = null;
                    await _repository.UpdateAsync(member, _sql.conn, _sql.trans);
                }
                await _shareholderRepository.DeleteAsync(id, _sql.conn, _sql.trans);
                response = _messageClass.DeleteMessage(1);
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                response.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return response;
        }
    }
}