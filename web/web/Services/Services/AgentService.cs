using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Services;
using web.Utility;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Web.Services.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<AgentDto>> GetAllAgent(int? ProvinceId, int? DistrictId, int? AgentStatusId, bool AdminAccess = false);
        Task<AgentDto> GetAgentById(int? id);
        Task<Response> Insert(AgentDto dto);
        Task<Response> Update(AgentDto dto);
    }

    public class AgentService : IAgentService
    {
        private readonly Repository<Agent> _repository;
        private readonly MessageClass _messageClass;
        private readonly InitialSetupModel initialSetupModel;
        private readonly IMemberRepository _memberRepository;
        private readonly IAgentRepository _agentRepository;
        public AgentService(IMemberRepository memberRepository,
            IAgentRepository agentRepository)
        {
            _repository = new Repository<Agent>();
            _messageClass = new MessageClass();
            initialSetupModel = new InitialSetupModel();
            _agentRepository = agentRepository;
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<AgentDto>> GetAllAgent(int ? ProvinceId, int? DistrictId,int ? AgentStatusId, bool AdminAccess=false)
        {
            int UserId = 0;
            if (!AdminAccess)
                UserId = _repository.UserIdentity();

            string _query = "select * from dbo.[AgentView] where " +
                //"(isnull(@ProvinceId,0)=0 or ProvinceId=@ProvinceId) and " +
                "(isnull(@DistrictId,0)=0 or DistrictId=@DistrictId) and " +
                "(isnull(@AgentStatusId,0)=0 or AgentStatusId=@AgentStatusId) and " +
                "(isnull(@UserId,0)=0 or CreatedBy=@UserId)";

            var obj = (await _repository.QueryAsync<AgentDto>(_query,
                new
                {
                    ProvinceId,DistrictId,AgentStatusId,UserId
                }));
            return obj;
        }

        public async Task<AgentDto> GetAgentById(int? id)
        {
            var obj = (await _repository.QueryAsync<AgentDto>("SELECT * FROM dbo.[AgentView] WHERE AgentId=@id", new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<Response> Insert(AgentDto dto)
        {
            var result = new Response();
            result.messageType = "error";
            try
            {
                dto = await ValidateAgent(dto);
                if (dto.response.messageType == "error")
                    return dto.response;
                
                dto.IsActive = true;
                dto.LicenceNumber = await _agentRepository.GetLicenceNumberAsync();
                dto.AgentStatusId = 1;
                dto.CreatedBy = _repository.UserIdentity();
                dto.CreatedDate = DateTime.Now;

                var entity = dto.ToEntity();
                int agentId = await _repository.InsertAsync(entity);
                result = _messageClass.SaveMessage(agentId);
                result.id = agentId;
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> Update(AgentDto dto)
        {
            var result = new Response();
            result.messageType = "error";
            try
            {
                var agentDto = await GetAgentById(dto.AgentId);
                if (agentDto == null)
                    return _messageClass.NotFoundMessage();

                dto = await ValidateAgent(dto);
                if (dto.response.messageType == "error")
                    return dto.response;

                var entity = agentDto.ToEntity();
                //entity.CitizenshipNumber = dto.CitizenshipNumber.Replace("/", "-");
                //bool isValidCitizen = initialSetupModel.ValidateCitizenshipNumber(entity.CitizenshipNumber);
                //if (isValidCitizen == false)
                //{
                //    result.message = "Citizenship Number is not Valid !!!!";
                //    return result;
                //}

                //if (dto.ReferenceLicenceNumber != agentDto.ReferenceLicenceNumber)
                //{
                //    entity.MemberId = null;
                //    entity.ReferenceAgentId = null;
                //    var member = await _memberService
                //                    .GetMemberByReferalCode(dto.ReferenceLicenceNumber.ToUpper());
                //    if (member != null)
                //    {
                //        entity.MemberId = member.MemberId;
                //    }
                //    else
                //    {
                //        var agent = await GetAgentByLicenceNumber(dto.ReferenceLicenceNumber.ToUpper());
                //        if (agent != null)
                //            entity.ReferenceAgentId = agent.AgentId;
                //    }
                //}
                //if (entity.ReferenceAgentId == null && entity.MemberId == null)
                //{
                //    result.message = "Lincence Number or Referal Code is not Valid !!!!";
                //    return result;
                //}
                entity.AgentFullName = dto.AgentFullName;
                entity.ContactNumber1 = dto.ContactNumber1;
                entity.ContactNumber2 = dto.ContactNumber2;
                entity.EmailAddress = dto.EmailAddress;
                entity.Occupation = dto.Occupation;
                entity.DistrictId = dto.DistrictId;
                entity.MemberId = dto.MemberId;
                entity.ReferenceAgentId = dto.ReferenceAgentId;
                entity.MunicipalityName = dto.MunicipalityName;
                entity.WardNumber = dto.WardNumber;
                entity.ToleName = dto.ToleName;
                entity.Qualification = dto.Qualification;
                entity.UpdatedBy = _repository.UserIdentity();
                entity.UpdatedDate = DateTime.Now;
                int agentId = await _repository.UpdateAsync(entity);
                result = _messageClass.SaveMessage(agentId);
                result.id = agentDto.AgentId;
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<AgentDto> ValidateAgent(AgentDto dto)
        {
            var response = new Response();
            dto.response = new Response();
            response.messageType = "success";
            var messageList = new List<string>();

            dto.CitizenshipNumber = dto.CitizenshipNumber.Replace("/", "-");
            bool isValidCitizen = initialSetupModel.ValidateCitizenshipNumber(dto.CitizenshipNumber);

            if (isValidCitizen == false)
                messageList.Add("Citizenship Number is not Valid !!!!");

            var member = new MemberDto();
            var agent = new AgentDto();
            if (!string.IsNullOrEmpty(dto.ReferenceLicenceNumber))
            {
                member = await _memberRepository
                          .GetMemberByReferalCodeAsync(dto.ReferenceLicenceNumber.ToUpper());
                if (member != null)
                    dto.MemberId = member.MemberId;
                else
                {
                     agent = await _agentRepository.GetAgentByLicenceNumberAsync(dto.ReferenceLicenceNumber.ToUpper());
                    if (agent != null)
                        dto.ReferenceAgentId = agent.AgentId;
                }
            }
            else
            {
                member = await _memberRepository
                     .GetMemberByPhoneNumberAsync(dto.ReferencePhoneNumber.ToUpper());

                if (member != null)
                    dto.MemberId = member.MemberId;
                else
                {
                    agent = await _agentRepository.GetAgentByPhoneNumberAsync(dto.ReferencePhoneNumber);
                    if (agent != null)
                        dto.ReferenceAgentId = agent.AgentId;
                }
            }
            if (dto.ReferenceAgentId == null && dto.MemberId == null)
            {
                messageList.Add("Lincence Number or Referal Code or Phone Number " +
                    "is not Valid !!!!");
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