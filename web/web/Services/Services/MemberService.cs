using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Model.Dto;
using web.Model.Entity;
using web.Utility;
using web.Web.Entity.Infrastructure;
using web.Web.Services;
using web.Web.Services.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Services.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto);
        Task<IEnumerable<MemberDto>> FilterShareholder(MemberFilterDto filterDto);
        Task<MemberDto> GetMemberById(int? MemberId);
        Task<Response> Insert(MemberDto dto);
        Task<Response> Update(MemberDto dto);
        Task<Response> UpdateApprovedMember(MemberDto dto);
        Task<Response> Approve(int memberId);
        Task<Response> Reject(int memberId, string rejectRemarks);
        Task<Response> AddMemberPaymentLog(MemberPaymentLogDto dto);
        Task<Response> AddToShareholder(int memberId);
        Task<Response> DeleteShareholder(int id);
    }

    public class MemberService: IMemberService
    {
        private readonly Repository<Member> _repository;
        private readonly Repository<MemberPaymentLog> _memberPaymentRepository;
        private readonly Repository<Shareholder> _shareholderRepository;
        private readonly MessageClass _messageClass;
        private readonly DateSettings _dateSettings;
        private readonly IMemberRepository _memberRepository;
        private readonly InitialSetupModel initialSetupModel;

        public MemberService(IMemberRepository memberRepository)
        {
            _repository = new Repository<Member>();
            _memberPaymentRepository = new Repository<MemberPaymentLog>();
            _shareholderRepository = new Repository<Shareholder>();
            _messageClass = new MessageClass();
            _dateSettings = new DateSettings();
            _memberRepository = memberRepository;
            initialSetupModel = new InitialSetupModel();
        }
        public async Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto)
        {
            var obj = await _repository.StoredProcedureAsync<MemberDto>("[dbo].[FilterMember]"
               , new
               {
                   ApprovalStatus = filterDto.ApprovalStatus,
                   ReferenceId = filterDto.ReferenceId,
                   AgentId = filterDto.AgentId,
                   ShareTypeId = filterDto.ShareTypeId,
                   SearchQuery=filterDto.SearchQuery,
                   SellerMemberId=filterDto.SellerMemberId
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
                   SearchQuery = filterDto.SearchQuery,
                   Code = filterDto.Code,
               });
            return obj;
        }

        public async Task<MemberDto> GetMemberById(int? MemberId)
        {
            var obj =await _memberRepository.GetMemberByIdAsync(MemberId);
            return obj;
        }

        public async Task<Response> Insert(MemberDto dto)
        {
            var resp = new Response();
            var _sql = _repository.GetSqlTransactionDetails();
            resp.messageType = "error";
            try
            {
                dto.TotalShareAmount = dto.AppliedShareKitta * dto.SharePricePerKitta;
                dto.IsApproved = ApprovalStatus.UnApproved;
                dto.CreatedBy = _repository.UserIdentity();
                dto.CreatedDate = DateTime.Now;

                dto = await ValidateMember(dto);
                if (dto.response.messageType == "error")
                    return dto.response;

                int MemberId = await _repository.InsertAsync(dto.ToEntity(), 
                    _sql.conn, _sql.trans);

                var paymentLog = new MemberPaymentLog();
                paymentLog.Amount = Convert.ToDecimal(dto.TotalSharePaidAmount);
                paymentLog.MemberId = MemberId;
                paymentLog.CreatedDate = DateTime.Now;
                paymentLog.CreatedBy = _repository.UserIdentity();
                paymentLog.IsDeleted = false;
                await _memberPaymentRepository.InsertAsync(paymentLog, _sql.conn, _sql.trans);

                resp = _messageClass.SaveMessage(MemberId);
                resp.id = MemberId;
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> Update(MemberDto dto)
        {
            var _sql = _repository.GetSqlTransactionDetails();
            var resp = new Response();
            resp.messageType = "error";
            try
            {
                var memberDto = await GetMemberById(dto.MemberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                if (memberDto.IsApproved != ApprovalStatus.UnApproved)
                {
                    resp.message = "This member is either approved or rejected,You cannot modify data from here,please go through another way!!!";
                    return resp;
                }

                dto.IsApproved = ApprovalStatus.UnApproved;
                dto = await ValidateMember(dto);
                if (dto.response.messageType == "error")
                    return dto.response;


                memberDto.FullName = dto.FullName;
                memberDto.CitizenshipNumber = dto.CitizenshipNumber;
                memberDto.Age = dto.Age;
                memberDto.FathersName = dto.FathersName;
                memberDto.HusbandName = dto.HusbandName;
                memberDto.ContactNumber = dto.ContactNumber;
                memberDto.EmailAddress = dto.EmailAddress;
                memberDto.FormerAddress = dto.FormerAddress;
                memberDto.PermanentAddress = dto.PermanentAddress;
                memberDto.TemporaryDistrictId = dto.TemporaryDistrictId;
                memberDto.TemporaryMunicipalityName = dto.TemporaryMunicipalityName;
                memberDto.TemporaryWardNumber = dto.TemporaryWardNumber;
                memberDto.ShareTypeId = dto.ShareTypeId;
                memberDto.AppliedShareKitta = dto.AppliedShareKitta;
                memberDto.TotalShareAmount = dto.AppliedShareKitta * dto.SharePricePerKitta;
                memberDto.TotalSharePaidAmount = dto.TotalSharePaidAmount;
                memberDto.SellerMemberId = dto.SellerMemberId;
                memberDto.ReferenceId = dto.ReferenceId;
                memberDto.AgentId = dto.AgentId;
                memberDto.NomineeName = dto.NomineeName;
                memberDto.UpdatedBy = _repository.UserIdentity();
                memberDto.UpdatedDate = DateTime.Now;

                int MemberId = await _repository.UpdateAsync(memberDto.ToEntity(),_sql.conn,_sql.trans);

                var paymentDto = await _memberRepository.GetMemberPaymentLog(memberDto.MemberId);
                if (paymentDto.Count() == 0)
                {
                    var paymentLog = new MemberPaymentLog();
                    paymentLog.Amount = Convert.ToDecimal(dto.TotalSharePaidAmount);
                    paymentLog.MemberId = memberDto.MemberId;
                    paymentLog.CreatedDate = DateTime.Now;
                    paymentLog.CreatedBy = _repository.UserIdentity();
                    paymentLog.IsDeleted = false;
                    await _memberPaymentRepository.InsertAsync(paymentLog, _sql.conn, _sql.trans);
                }
                else
                {
                    var paymentLog = paymentDto.FirstOrDefault();
                    paymentLog.Amount = Convert.ToDecimal(dto.TotalSharePaidAmount);
                    await _memberPaymentRepository.UpdateAsync(paymentLog.ToEntity(), _sql.conn, _sql.trans);
                }
                resp = _messageClass.SaveMessage(MemberId);
                resp.id = memberDto.MemberId;
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> UpdateApprovedMember(MemberDto dto)
        {
            var resp = new Response();
            resp.messageType = "error";
            try
            {
                var memberDto = await GetMemberById(dto.MemberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                dto = await ValidateMember(dto);
                if (dto.response.messageType == "error")
                    return dto.response;

                memberDto.FullName = dto.FullName;
                memberDto.CitizenshipNumber = dto.CitizenshipNumber;
                memberDto.Age = dto.Age;
                memberDto.FathersName = dto.FathersName;
                memberDto.HusbandName = dto.HusbandName;
                memberDto.ContactNumber = dto.ContactNumber;
                memberDto.EmailAddress = dto.EmailAddress;
                memberDto.FormerAddress = dto.FormerAddress;
                memberDto.PermanentAddress = dto.PermanentAddress;
                memberDto.TemporaryDistrictId = dto.TemporaryDistrictId;
                memberDto.TemporaryMunicipalityName = dto.TemporaryMunicipalityName;
                memberDto.TemporaryWardNumber = dto.TemporaryWardNumber;
               
                memberDto.SellerMemberId = dto.SellerMemberId;
                memberDto.ReferenceId = dto.ReferenceId;
                memberDto.AgentId = dto.AgentId;
                memberDto.NomineeName = dto.NomineeName;
                memberDto.UpdatedBy = _repository.UserIdentity();
                memberDto.UpdatedDate = DateTime.Now;
                int MemberId = await _repository.UpdateAsync(memberDto.ToEntity());
                resp = _messageClass.SaveMessage(MemberId);
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<Response> Approve(int memberId)
        {
            var _sql = _repository.GetSqlTransactionDetails();
            var resp = new Response();
            resp.messageType = "error";
            resp.message = "Something went wrong,please contact to admin!!!!!";
            try
            {
                var memberDto = await GetMemberById(memberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                var mem_check = ChecMemberDetailskBeforeApprove(memberDto);
                if (mem_check.messageType == "error")
                    return mem_check;

                memberDto.IsApproved = ApprovalStatus.Approved;
                memberDto.ApprovedBy = _repository.UserIdentity();
                memberDto.ApprovedDate = DateTime.Now;
                memberDto.ReferalCode =await _memberRepository.GetReferalCode();
                memberDto.MemberCode = await _memberRepository.GetMemberCode();
                int UpdateId = await _repository.UpdateAsync(memberDto.ToEntity(),_sql.conn,_sql.trans);
                if (UpdateId<0)
                {
                    resp.message = "Something went wrong,please contact to admin!!!";
                    return resp;
                }

                resp.messageType = "success";
                resp.message = "Member approved successfully!!!!!!";
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> AddToShareholder(int memberId)
        {
            var _sql = _repository.GetSqlTransactionDetails();
            var resp = new Response();
            resp.messageType = "error";
            resp.message = "Something went wrong,please contact to admin!!!!!";
            try
            {
                var memberDto = await GetMemberById(memberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                if (memberDto.IsApproved != ApprovalStatus.Approved)
                {
                    resp.message = "Member must be approved to perform this action";
                    return resp;
                }
                if (memberDto.TotalShareAmount > memberDto.TotalSharePaidAmount)
                {
                    resp.message = "Please make full payment!!!!!";
                    return resp;
                }
                var shareholderDto = await _memberRepository.GetShareholderByMemberId(memberId);

                if (shareholderDto != null)
                {
                    resp.message = "Shareholder already exists !!";
                    return resp;
                }

                memberDto.IsShareholder = 1;
                int UpdateId = await _repository.UpdateAsync(memberDto.ToEntity(), _sql.conn, _sql.trans);
                if (UpdateId < 0)
                {
                    resp.message = "Something went wrong,please contact to admin!!!";
                    return resp;
                }
                var dto = new ShareholderDto();
                dto.MemberId = memberId;
                dto.ShareTypeId = Convert.ToInt32(memberDto.ShareTypeId);
                dto.TotalKitta = Convert.ToInt32(memberDto.AppliedShareKitta);
                dto.IsActive = 1;
                dto.ApprovedDate = DateTime.Now;
                await _shareholderRepository.InsertAsync(dto.ToEntity(), _sql.conn, _sql.trans);

                resp.messageType = "success";
                resp.message = "Shareholder added successfully!!!!!!";
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> Reject(int memberId,string rejectRemarks)
        {
            var resp = new Response();
            resp.messageType = "error";
            resp.message = "Something went wrong,please contact to admin!!!!!";
            try
            {
                var memberDto = await GetMemberById(memberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                memberDto.IsApproved = ApprovalStatus.Rejected;
                memberDto.ApprovedBy = _repository.UserIdentity();
                memberDto.ApprovedDate = DateTime.Now;
                memberDto.RejectRemarks = rejectRemarks;
                int UpdateId = await _repository.UpdateAsync(memberDto.ToEntity());
                if (UpdateId >= 0)
                {
                    resp.messageType = "success";
                    resp.message = "Member rejected successfully!!!!!!";
                }
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<Response> AddMemberPaymentLog(MemberPaymentLogDto dto)
        {
            var _sql = _repository.GetSqlTransactionDetails();
            var resp = new Response();
            resp.messageType = "error";
            resp.message = "Something went wrong,please contact to admin!!!!!";
            try
            {
                var memberDto = await GetMemberById(dto.MemberId);
                if (memberDto == null)
                    return _messageClass.NotFoundMessage();

                if (memberDto.IsApproved != ApprovalStatus.Approved)
                {
                    resp.message = "Member must be approved to perform this action!!!!";
                    return resp;
                }

                if (dto.Amount <= 0)
                {
                    resp.message = "Payment amount must be greater than zero";
                    return resp;
                }

                var dueAmount = memberDto.TotalShareAmount - memberDto.TotalSharePaidAmount;
                if (dueAmount == 0)
                {
                    resp.message = "No Due Left,All Payment has cleared";
                    return resp;
                }
                if (dueAmount < dto.Amount)
                {
                    resp.message = "Due amount is only "+dueAmount;
                    return resp;
                }
                memberDto.TotalSharePaidAmount = Convert.ToDecimal(memberDto.TotalSharePaidAmount) + dto.Amount;

                dto.CreatedBy = _repository.UserIdentity();
                dto.CreatedDate = DateTime.Now;
                await _memberPaymentRepository.InsertAsync(dto.ToEntity(),_sql.conn,_sql.trans);

                await _repository.UpdateAsync(memberDto.ToEntity(), _sql.conn, _sql.trans);
                resp = _messageClass.SaveMessage(0);

                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public async Task<Response> DeleteShareholder(int id)
        {
            var _sql = _repository.GetSqlTransactionDetails();
            var response = new Response();
            response.messageType = "error";
            var shareholderDto = await _memberRepository.GetShareholderByShareholderIdAsync(id);
            if (shareholderDto is null)
            {
                response.message = "Invalid shareholder!!!";
                return response;
            }
            var memberDto = await GetMemberById(shareholderDto.MemberId);
            try
            {
                if (memberDto != null)
                {
                    var member = memberDto.ToEntity();
                    member.IsShareholder = 0;
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


        public Response ChecMemberDetailskBeforeApprove(MemberDto dto)
        {
            var response = new Response();
            response.messageType = "success";
            var messageList = new List<string>();
            if (string.IsNullOrEmpty(dto.FullName))
                messageList.Add("Please update member name");
            if (string.IsNullOrEmpty(dto.FathersName) && string.IsNullOrEmpty(dto.HusbandName))
                messageList.Add("Please update father name or husband name");
            if (string.IsNullOrEmpty(dto.CitizenshipNumber))
                messageList.Add("Please update citizenship number");
            if (dto.Age<=0)
                messageList.Add("Please update age");
            if (string.IsNullOrEmpty(dto.ContactNumber))
                messageList.Add("Please update contact number");
            if (string.IsNullOrEmpty(dto.FormerAddress))
                messageList.Add("Please update former address");
            if (string.IsNullOrEmpty(dto.PermanentAddress))
                messageList.Add("Please update changed address");
            if (dto.TemporaryDistrictId == null)
                messageList.Add("Please update district");
            if (string.IsNullOrEmpty(dto.TemporaryMunicipalityName))
                messageList.Add("Please update municipality name ");
            if (string.IsNullOrEmpty(dto.TemporaryWardNumber) || Convert.ToInt32(dto.TemporaryWardNumber) <=0)
                messageList.Add("Please update ward number");
            if (dto.ShareTypeId == null)
                messageList.Add("Please update share type");
            if (Convert.ToInt32(dto.AppliedShareKitta) <= 0)
                messageList.Add("Please update share kitta");
            if (Convert.ToDecimal(dto.TotalSharePaidAmount) <= 0)
                messageList.Add("Please make payment");
            if (dto.SellerMemberId == null)
                messageList.Add("Please update seller details");
            if (string.IsNullOrEmpty(dto.ReferenceLicenceNumber) && string.IsNullOrEmpty(dto.ReferencePhoneNumber))
                messageList.Add("Please update reference details");
            if (dto.IsApproved == ApprovalStatus.Approved)
                messageList.Add("This Member has already approved!!!!");

            if (messageList.Count() > 0)
            {
                response.messageType = "error";
                response.message = String.Join("\r\n",messageList);
            }
            return response;
        }

        public async Task<MemberDto> ValidateMember(MemberDto dto)
        {
            var response = new Response();
            dto.response = new Response();
            response.messageType = "success";
            var messageList = new List<string>();

            dto.CitizenshipNumber = dto.CitizenshipNumber.Replace("/", "-");
            bool isValidCitizen = initialSetupModel.ValidateCitizenshipNumber(dto.CitizenshipNumber);

            if (isValidCitizen == false)
                messageList.Add("Citizenship Number is not Valid !!!!");

            var reference = await _memberRepository.GetRefernceAgentMemberId(dto.ReferenceLicenceNumber, dto.ReferencePhoneNumber);
            dto.AgentId = reference.AgentId;
            dto.ReferenceId = reference.MemberId;

            if (dto.ReferenceId == null && dto.AgentId == null)
            {
                messageList.Add("Lincence Number or Referal Code or Phone Number " +
                    "is not Valid !!!!");
            }
            if (dto.IsApproved == ApprovalStatus.UnApproved)
            {
                if (dto.AppliedShareKitta < 200)
                {
                    messageList.Add("Share must be greater than or equal to 200 kitta");
                }
                if (dto.TotalSharePaidAmount <= 0)
                    messageList.Add("Payment must be done,please enter total " +
                        "share payment amount !!!!");
            }
            if (messageList.Count() > 0)
            {
                response.messageType = "error";
                response.message = String.Join("\r\n", messageList);
            }
            dto.response = response;
            return dto;
        }
    }
}