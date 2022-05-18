using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
    public interface IMemberRegisterService
    {
        Task<MemberResponse> SaveStep1Data(MemberDto dto);
        Task<MemberResponse> SaveStep2Data(MemberDto dto);
        Task<MemberResponse> SaveStep3Data(MemberDto dto);
        Task<MemberResponse> SaveStep4Data(UserDocumentDto dto);
        Task<MemberResponse> SaveStep5Data(MemberDto dto);
        Task<MemberResponse> GetMemberBySearchQuery(string query);
    }

    public class MemberRegisterService : IMemberRegisterService
    {
        private readonly Repository<Member> _repository;
        private readonly Repository<Address> _addressRepository;
        private readonly Repository<UserDocuments> _userDocumentRepository;
        private readonly Repository<BankDeposit> _bankDepositRepository;
        private readonly IMemberService _memberService;
        private readonly IAgentService _agentService;
        private readonly MemberDto memberDto = new MemberDto();
        private readonly DateSettings _dateSettings;
        private readonly SqlConnectionDetails _sql;
        private readonly ImageSettings _imageSettings;
        private readonly string extension= ".jpg";
        public MemberRegisterService(IMemberService memberService,
            IAgentService agentService)
        {
            _memberService = memberService;
            _agentService = agentService;
            _repository = new Repository<Member>();
            _addressRepository = new Repository<Address>();
            _userDocumentRepository = new Repository<UserDocuments>();
            _bankDepositRepository = new Repository<BankDeposit>();
            _dateSettings = new DateSettings();
            _sql = _repository.GetSqlTransactionDetails();
            _imageSettings = new ImageSettings();
        }

        public async Task<MemberResponse> SaveStep1Data(MemberDto dto)
        {
            var resp = new MemberResponse();
            resp.messageType = "error";
            try
            {
                var valid = TrimCitizenshipNumber(dto.CitizenshipNumber);
                if (valid.messageType != "success")
                    return valid;

                dto.CitizenshipNumber = valid.value;
                var memberDto = await _memberService.GetMemberByCitizenshipNumber(dto.CitizenshipNumber,dto.MemberId);
                if (memberDto != null)
                {
                    if (memberDto.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        resp.message = "Your form Has been already approved,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else if (memberDto.ApprovalStatus == ApprovalStatus.Rejected)
                    {
                        resp.message = "Your form Has been rejected,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else
                    {
                        memberDto.CitizenshipNumber = dto.CitizenshipNumber;
                        int response = await _repository.UpdateAsync(memberDto.ToEntity());
                        resp.messageType = "success";
                    }
                }
                else
                {
                    memberDto = new MemberDto();
                    var entity = new Member
                    { 
                        CitizenshipNumber = dto.CitizenshipNumber,
                        FormStatus=FormStatus.Incomplete,
                        ApprovalStatus=ApprovalStatus.UnApproved,
                        CreatedDate=DateTime.Now
                    };
                    int id = await _repository.InsertAsync(entity);
                    memberDto.MemberId = id;
                    resp.messageType = "success";
                }
                resp.memberDto = memberDto;
            }
            catch(SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<MemberResponse> SaveStep2Data(MemberDto dto)
        {
            var resp = new MemberResponse();
            resp.messageType = "error";
            try
            {
                var valid =await ValidateMemberDataStepWise(dto,2);
                if (valid.messageType != "success")
                    return valid;
                
                var memberDto = await _memberService.GetMemberForUpdate(dto.MemberId);
                if(memberDto==null)
                {
                    valid.messageType = "error";
                    valid.message = "Invalid Member,Please contact to office";
                }
                else
                {
                    if (memberDto.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        resp.message = "Your form Has been already approved,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else if (memberDto.ApprovalStatus == ApprovalStatus.Rejected)
                    {
                        resp.message = "Your form Has been rejected,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else
                    {
                        var addressDto = await _memberService.GetAddressByMemberId(dto.MemberId);
                        var entity = memberDto.ToEntity();
                        entity.FirstName = dto.FirstName;
                        entity.MiddleName = dto.MiddleName;
                        entity.LastName = dto.LastName;
                        entity.DateOfBirthBS = dto.DateOfBirthBS;
                        entity.DateOfBirthAD = valid.value;
                        entity.MobileNumber = dto.MobileNumber;
                        entity.Email = dto.Email;
                        if (dto.OccupationId < 0)
                            dto.OccupationId = null;
                        if (dto.OccupationId > 0)
                            dto.OtherOccupationRemarks = null;

                        entity.OccupationId = dto.OccupationId;
                        entity.OtherOccupationRemarks = dto.OtherOccupationRemarks;
                        entity.GenderId = dto.GenderId;
                        entity.MaritalStatusId = dto.MaritalStatusId;

                        int response = await _repository.UpdateAsync(entity);
                        resp.id = memberDto.MemberId;
                        resp.messageType = "success";
                        if (addressDto == null) addressDto = new MemberDto();

                        addressDto.MemberId = dto.MemberId;
                        resp.memberDto = addressDto;
                    }
                }
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<MemberResponse> SaveStep3Data(MemberDto dto)
        {
            var resp = new MemberResponse();
            resp.messageType = "error";
            try
            {
                var valid = await ValidateMemberDataStepWise(dto, 3);
                if (valid.messageType != "success")
                    return valid;

                var memberDto = await _memberService.GetMemberForUpdate(dto.MemberId);
                if (memberDto == null)
                {
                    valid.messageType = "error";
                    valid.message = "Invalid Member,Please try again";
                }
                else
                {
                    if (memberDto.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        resp.message = "Your form Has been already approved,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else if (memberDto.ApprovalStatus == ApprovalStatus.Rejected)
                    {
                        resp.message = "Your form Has been rejected,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else
                    {
                        int respId = 0;
                        var entity = dto.ToAddressEntity();
                        var addressDto = await _memberService.GetAddressByMemberId(dto.MemberId);
                        if (addressDto == null)
                        {
                            respId= await _addressRepository.InsertAsync(entity);
                        }
                        else
                        {
                            entity.Id = addressDto.Id;
                            respId = await _addressRepository.UpdateAsync(entity);
                        }
                        resp.messageType = "success";
                        resp.memberDto = memberDto;
                        resp.userDocumentDto = await GetDocumentByMemberId(dto.MemberId);
                    }
                }
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<MemberResponse> SaveStep4Data(UserDocumentDto dto)
        {
            var resp = new MemberResponse();
            resp.messageType = "error";
            try
            {
                var valid = await ValidateMemberDataStepWise(null, 4,dto);
                if (valid.messageType != "success")
                    return valid;

                var memberDto = await _memberService
                    .GetMemberWithRefernceDetailsForUpdate(dto.MemberId);
                if (memberDto == null)
                {
                    valid.messageType = "error";
                    valid.message = "Invalid Member,Please try again";
                }
                else
                {
                    if (memberDto.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        resp.message = "Your form Has been already approved,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else if (memberDto.ApprovalStatus == ApprovalStatus.Rejected)
                    {
                        resp.message = "Your form Has been rejected,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else
                    {
                        int respId = 0;
                        var entity = dto.ToEntity();
                        if (!string.IsNullOrEmpty(dto.MemberPhotoString))
                        {
                            var photo = _imageSettings.SaveImage(dto.MemberPhotoString, "MP-" + dto.MemberId);
                            if (photo)
                                entity.Photo = "MP-" + dto.MemberId + extension;

                            //entity.Photo = dto.MemberPhotoString;
                        }
                        if (!string.IsNullOrEmpty(dto.CitizenshipFrontImageString))
                        {
                            var citizenFront = _imageSettings.SaveImage(dto.CitizenshipFrontImageString, "CF-" + dto.MemberId);

                            if (citizenFront)
                                entity.CitizenshipFront = "CF-" + dto.MemberId + extension;
                            //entity.CitizenshipFront = dto.CitizenshipFrontImageString;
                        }
                        if (!string.IsNullOrEmpty(dto.CitizenshipBackImageString))
                        {
                            //entity.CitizenshipBack = dto.CitizenshipBackImageString;
                            var citizenBack = _imageSettings.SaveImage(dto.CitizenshipBackImageString, "CB-" + dto.MemberId);
                            if (citizenBack)
                                entity.CitizenshipBack = "CB-" + dto.MemberId + extension;
                        }
                        var userDocumentDto = await GetDocumentByMemberId(Convert.ToInt32(dto.MemberId));

                        if (userDocumentDto == null)
                        {
                            respId = await _userDocumentRepository.InsertAsync(entity);
                        }
                        else
                        {
                            entity.UserDocumentId = userDocumentDto.UserDocumentId;
                            respId = await _userDocumentRepository.UpdateAsync(entity);
                        }
                        resp.messageType = "success";
                        resp.memberDto = memberDto;
                    }
                }
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public async Task<MemberResponse> SaveStep5Data(MemberDto dto)
        {
            var resp = new MemberResponse();
            resp.messageType = "error";
            try
            {
                var valid = await ValidateMemberDataStepWise(dto, 5);
                if (valid.messageType != "success")
                    return valid;

                var memberDto = await _memberService.GetMemberForUpdate(dto.MemberId);
                if (memberDto == null)
                {
                    valid.messageType = "error";
                    valid.message = "Invalid Member,Please try again";
                }
                else
                {
                    if (memberDto.ApprovalStatus == ApprovalStatus.Approved)
                    {
                        resp.message = "Your form Has been already approved,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else if (memberDto.ApprovalStatus == ApprovalStatus.Rejected)
                    {
                        resp.message = "Your form Has been rejected,Please contact to our team for further query,Thanks!!!!!";
                    }
                    else
                    {
                        var bankDepositDto = await _memberService.GetMemberBankDepositById(dto.MemberId);
                        var bankDeposit = new BankDeposit
                        {
                            MemberId = dto.MemberId,
                            IsVoucherDeposit = true,
                            //VoucherImage = dto.VoucherImageString
                        };
                        if (dto.VoucherImageString != null)
                        {
                            var voucher = _imageSettings.SaveImage(dto.VoucherImageString, "VI-" + dto.MemberId);
                            if (voucher)
                                bankDeposit.VoucherImage = "VI-" + dto.MemberId + extension;
                        }

                        if (bankDepositDto != null)
                        {
                            bankDeposit.Id = bankDepositDto.Id;
                            if (string.IsNullOrEmpty(dto.VoucherImageString))
                            {
                                bankDeposit.VoucherImage = bankDepositDto.VoucherImage;
                            }
                            await _bankDepositRepository.UpdateAsync(bankDeposit, 
                                _sql.conn, _sql.trans);

                        }
                        else
                        {
                            await _bankDepositRepository.InsertAsync(bankDeposit,
                                _sql.conn, _sql.trans);
                        }
                        var entity = memberDto.ToEntity();
                        entity.FormStatus = FormStatus.Complete;
                        entity.ApprovalStatus = ApprovalStatus.UnApproved;
                        entity.ReferenceId = dto.ReferenceId;
                        entity.AgentId = dto.AgentId;
                        await _repository.UpdateAsync(entity, _sql.conn, _sql.trans);

                        resp.messageType = "success";
                        resp.memberDto = memberDto;
                    }
                }
                _sql.trans.Commit();
            }
            catch (SqlException ex)
            {
                resp.message = ex.Message.ToString();
                _sql.trans.Rollback();
            }
            return resp;
        }

        public MemberResponse TrimCitizenshipNumber(string citizenshipNumber)
        {
            string returnString = string.Empty;
            var response = new MemberResponse();
            response.messageType = "error";

            if (!string.IsNullOrEmpty(citizenshipNumber))
            {
                if (citizenshipNumber.Substring(citizenshipNumber.Length - 1) == "/")
                {
                    response.message = "Invalid Citizenship Number";
                    return response;
                }
                var split = citizenshipNumber.Replace("-", "/").Split('/');
                foreach (var s in split)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s.Trim().All(char.IsDigit))
                        {
                            response.messageType = "success";
                            if (string.IsNullOrEmpty(returnString))
                                returnString = s.Trim();
                            else
                                returnString = returnString + "/" + s;
                        }
                        else
                        {
                            response.messageType = "error";
                            response.message = "Invalid Citizenship Number";
                            return response;
                        }
                    }
                    else
                    {
                        response.messageType = "error";
                        response.message = "Invalid Citizenship Number";
                        return response;
                    }
                }
                if (string.IsNullOrEmpty(returnString))
                {
                    response.messageType = "error";
                    response.message = "Invalid Citizenship Number";
                }
            }
            else
            {
                response.message = "Please Enter Citizenship Number!!!!";
            }
            response.value = returnString;
            return response;
        }

        public async Task<MemberResponse> ValidateMemberDataStepWise(MemberDto dto,int step,UserDocumentDto documentDto=null)
        {
            var resp = new MemberResponse();
            resp.messageType = "success";
            var messageList = new List<string>();
            if (step == 2)
            {
                if (dto.MemberId == 0)
                {
                    messageList.Add("Something Went Wrong,Please contact to office");
                }
                if (string.IsNullOrEmpty(dto.FirstName))
                {
                    messageList.Add("Please Enter First Name!!!!");
                }
                if (string.IsNullOrEmpty(dto.LastName))
                {
                    messageList.Add("Please Enter Last Name!!!!");
                }
                if (string.IsNullOrEmpty(dto.DateOfBirthBS))
                {
                    messageList.Add("Please Enter Date Of Birth!!!!");
                }
                if(dto.GenderId==null || dto.GenderId==0)
                {
                    messageList.Add("Please Select Gender.");
                }
                if (dto.MaritalStatusId == null || dto.MaritalStatusId == 0)
                {
                    messageList.Add("Please Select Marital Status.");
                }
                if (string.IsNullOrEmpty(dto.MobileNumber))
                {
                    messageList.Add("Please Enter Mobile Number");
                }
                if (!string.IsNullOrEmpty(dto.DateOfBirthBS))
                {
                    string dateOfBirthAD = _dateSettings.ConvertToEnglishDate(dto.DateOfBirthBS);
                    if (string.IsNullOrEmpty(dateOfBirthAD) || dateOfBirthAD == "0/0/0")
                    {
                        messageList.Add("Invalid Date Of Birth!!");
                    }
                    else
                    {
                        resp.value = dateOfBirthAD;
                    }
                }
                if (!string.IsNullOrEmpty(dto.MobileNumber))
                {
                    var member =await _memberService.GetMemberByMobileNumber(dto.MobileNumber?.Trim(),dto.MemberId);
                    if (member!=null)
                    {
                        messageList.Add("Mobile Number Already Exists,Please contact at office!!!");
                    }
                }
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    var member = await _memberService
                        .GetMemberByEmail(dto.Email?.Trim(),dto.MemberId);
                    if (member != null)
                    {
                        messageList.Add("Email Address Already Exists,Please contact at office!!!");
                    }
                }
            }
            else if (step == 3)
            {
                if (dto.FormerDistrictId==null || dto.FormerDistrictId == 0)
                    messageList.Add("Please Select Former District");

                if (string.IsNullOrEmpty(dto.FormerMunicipalityName))
                    messageList.Add("Please Enter Former Municipality Name");

                if(string.IsNullOrEmpty(dto.FormerWardNumber))
                    messageList.Add("Please Enter Former Ward Number");

                if (dto.PermanentDistrictId == null || dto.PermanentDistrictId == 0)
                    messageList.Add("Please Select Permanent District");

                if (string.IsNullOrEmpty(dto.PermanentMunicipality))
                    messageList.Add("Please Enter Permanent Municipality Name");

                if(string.IsNullOrEmpty(dto.PermanentWardNumber))
                    messageList.Add("Please Enter Permanent Ward Number");

                if (dto.TemporaryIsOutsideNepal)
                {
                    if (dto.PermanentDistrictId == null || dto.PermanentDistrictId == 0)
                        messageList.Add("Please Select Country");

                    if (string.IsNullOrEmpty(dto.TemporaryAddress))
                        messageList.Add("Please Enter Temporary Address");
                }
                else
                {
                    if (dto.TemporaryDistrictId == null || dto.TemporaryDistrictId == 0)
                        messageList.Add("Please Select Temporary District");

                    if (string.IsNullOrEmpty(dto.TemporaryMunicipality))
                        messageList.Add("Please Enter Temporary Municipality Name");

                    if (string.IsNullOrEmpty(dto.TemporaryWardNumber))
                        messageList.Add("Please Enter Temporary Ward Number");
                }
            }
            else if (step == 4)
            {
                if (string.IsNullOrEmpty(documentDto.Photo)
                    && string.IsNullOrEmpty(documentDto.MemberPhotoString))
                    messageList.Add("Please Upload Photo");

                if (string.IsNullOrEmpty(documentDto.CitizenshipFront) 
                    && string.IsNullOrEmpty(documentDto.CitizenshipFrontImageString))
                    messageList.Add("Please Upload Citizenship Front");

                if (string.IsNullOrEmpty(documentDto.CitizenshipBack)
                    && string.IsNullOrEmpty(documentDto.CitizenshipBackImageString))
                    messageList.Add("Please Upload Citizenship Back");
            }
            else if (step == 5)
            {
                if (string.IsNullOrEmpty(dto.ReferenceReferalCode))
                {
                    messageList.Add("Please enter referal code or agent licenece number");
                }
                else
                {
                    var member = await _memberService
                                .GetMemberByReferalCode(dto.ReferenceReferalCode.ToUpper());
                    if (member != null)
                    {
                        dto.ReferenceId = member.MemberId;
                    }
                    else
                    {
                        var agent = await _agentService.GetAgentByLicenceNumber(dto.ReferenceReferalCode.ToUpper());
                        if (agent != null)
                            dto.AgentId = agent.AgentId;
                    }

                    if (dto.AgentId == null && dto.ReferenceId == null)
                    {
                        messageList.Add("Invalid Referal Code !!!!");
                    }
                }
                if (string.IsNullOrEmpty(dto.VoucherImageString)
                    && string.IsNullOrEmpty(dto.VoucherImage))
                    messageList.Add("Please Upload Payment Proof");

                
            }
            if (messageList.Count() > 0)
                resp.messageType = "error";

            resp.messageList = messageList;
            return resp;
        }
        public async Task<UserDocumentDto> GetDocumentByMemberId(int memberId)
        {
            string _sql = "select UserDocumentId,Photo,CitizenshipFront," +
                "CitizenshipBack,IsImageString,MemberId " +
                "from dbo.[UserDocuments] where MemberId=@memberId";
            var obj = await _repository.QueryAsync<UserDocumentDto>(_sql, new { memberId });
            return obj.FirstOrDefault();
        }

        public async Task<MemberResponse> GetMemberBySearchQuery(string query)
        {
            string _sql = "select top 1 MemberId,CitizenshipNumber,ApprovalStatus " +
                         "from dbo.[Member] where " +
                         "trim(CitizenshipNumber)=trim(@query) or " +
                         "trim(MobileNumber)=trim(@query) or " +
                         "trim(Email)=trim(@query) ";

            var obj = (await _repository.QueryAsync<MemberDto>(_sql, new { query }))
                        .FirstOrDefault();
            var resp = new MemberResponse 
            { 
                messageType="error",
                message="Data cannot be found,please try again with another value"
            };
            if (obj!=null)
            {
                resp.messageType = "success";
                resp.memberDto = obj;
                if (obj.ApprovalStatus != ApprovalStatus.UnApproved)
                {
                    resp.messageType = "error";
                    resp.message = "You cannot modify data,either it is approved or rejected!!!";
                }
                
            }
            return resp;
        }
    }
}