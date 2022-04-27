using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        Task<Response> SaveStep1Data(MemberDto dto);
    }

    public class MemberRegisterService : IMemberRegisterService
    {
        private readonly Repository<Member> _repository;
        private readonly IMemberService _memberService;
        public MemberRegisterService(IMemberService memberService)
        {
            _memberService = memberService;
            _repository = new Repository<Member>();
        }

        public async Task<Response> SaveStep1Data(MemberDto dto)
        {
            var resp = new Response();
            resp.messageType = "error";
            try
            {
                var valid = TrimCitizenshipNumber(dto.CitizenshipNumber);
                if (valid.messageType != "success")
                    return valid;

                dto.CitizenshipNumber = valid.value;
                var memberDto = await _memberService.GetMemberByCitizenshipNumber(dto.CitizenshipNumber);
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
                        int response = await _repository.UpdateAsync(memberDto.ToEntity());
                        resp.id = memberDto.MemberId;
                        resp.messageType = "success";
                    }
                }
                else
                {
                    var entity = new Member
                    { 
                        CitizenshipNumber = dto.CitizenshipNumber,
                        FormStatus=FormStatus.Incomplete,
                        ApprovalStatus=ApprovalStatus.UnApproved
                    };
                    int id = await _repository.InsertAsync(entity);
                    resp.id = id;
                    resp.messageType = "success";
                }
            }
            catch(SqlException ex)
            {
                resp.message = ex.Message.ToString();
            }
            return resp;
        }

        public Response TrimCitizenshipNumber(string citizenshipNumber)
        {
            string returnString = string.Empty;
            var response = new Response();
            response.messageType = "error";
            if (!string.IsNullOrEmpty(citizenshipNumber))
            {
                var split = citizenshipNumber.Replace("-","/").Split('/');
                foreach(var s in split)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        response.messageType = "success";
                        if (s.Trim().All(char.IsDigit))
                            returnString += s.Trim();
                    }
                    else
                    {
                        response.messageType = "error";
                        response.message = "Invalid Citizenship Number";
                        return response;
                    }
                }
            }
            else if (citizenshipNumber.Substring(citizenshipNumber.Length - 1) == "/")
            {
                response.message = "Invalid Citizenship Number";
            }
            else
            {
                response.message = "Please Enter Citizenship Number!!!!";
            }
            response.value = returnString;
            return response;
        }

        public Response ValidateMemberDataStepWise(MemberDto dto,int step)
        {
            var resp = new Response();
            resp.messageType = "success";
            var messageList = new List<string>();
            if (step == 2)
            {
                //if (string.IsNullOrEmpty(dto.CitizenshipNumber))
                //{
                //    resp.messageType = "error";
                //    resp.message = "Please Enter Citizenship Number!!!!";
                //}
                //else if (dto.CitizenshipNumber.Substring(dto.CitizenshipNumber.Length - 1) == "/")
                //{
                //    resp.messageType = "error";
                //    messageList.Add("Invalid Citizenship Number");
                //}
               
            }
            return resp;
        }
    }
}