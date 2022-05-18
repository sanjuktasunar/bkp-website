using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using web.Web.Services.Mapping;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;

namespace web.Web.Services.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<AgentDto>> GetAllAgent(int? ProvinceId, int? DistrictId, int? AgentStatusId, bool AdminAccess = false);
        Task<AgentDto> GetAgentById(int? id);
        Task<Response> Insert(AgentDto dto);
        Task<Response> Update(AgentDto dto);
        Task<AgentDto> GetAgentByLicenceNumber(string LicenceNumber);
    }

    public class AgentService : IAgentService
    {
        private readonly Repository<Agent> _repository;
        private readonly MessageClass _messageClass;
        private readonly InitialSetupModel initialSetupModel;
        private readonly IMemberService _memberService;
        public AgentService(IMemberService memberService)
        {
            _repository = new Repository<Agent>();
            _messageClass = new MessageClass();
            initialSetupModel = new InitialSetupModel();
            _memberService = memberService;
        }

        public async Task<IEnumerable<AgentDto>> GetAllAgent(int ? ProvinceId, int? DistrictId,int ? AgentStatusId, bool AdminAccess=false)
        {
            int UserId = 0;
            if (!AdminAccess)
                UserId = _repository.UserIdentity();

            string _query = "select * from dbo.[AgentView] " +
                "where " +
                "(isnull(@ProvinceId,0)=0 or ProvinceId=@ProvinceId) and " +
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
                var entity = dto.ToEntity();
                entity.CitizenshipNumber = dto.CitizenshipNumber.Replace("/", "-");
                entity.IsActive = true;
                entity.LicenceNumber = await GetLicenceNumber();
                bool isValidCitizen = initialSetupModel.ValidateCitizenshipNumber(entity.CitizenshipNumber);

                if (isValidCitizen == false)
                {
                    result.message = "Citizenship Number is not Valid !!!!";
                    return result;
                }

                var member = await _memberService
                                .GetMemberByReferalCode(dto.ReferenceLicenceNumber.ToUpper());
                if (member != null)
                {
                    entity.MemberId = member.MemberId;
                }
                else
                {
                    var agent = await GetAgentByLicenceNumber(dto.ReferenceLicenceNumber.ToUpper());
                    if (agent != null)
                        entity.ReferenceAgentId = agent.AgentId;
                }

                if(entity.ReferenceAgentId == null && entity.MemberId == null)
                {
                    result.message = "Lincence Number or Referal Code is not Valid !!!!";
                    return result;
                }
                entity.AgentStatusId = 1;
                entity.CreatedBy = _repository.UserIdentity();
                entity.CreatedDate = DateTime.Now;
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

                var entity = agentDto.ToEntity();
                entity.CitizenshipNumber = dto.CitizenshipNumber.Replace("/", "-");
                bool isValidCitizen = initialSetupModel.ValidateCitizenshipNumber(entity.CitizenshipNumber);
                if (isValidCitizen == false)
                {
                    result.message = "Citizenship Number is not Valid !!!!";
                    return result;
                }

                if (dto.ReferenceLicenceNumber != agentDto.ReferenceLicenceNumber)
                {
                    entity.MemberId = null;
                    entity.ReferenceAgentId = null;
                    var member = await _memberService
                                    .GetMemberByReferalCode(dto.ReferenceLicenceNumber.ToUpper());
                    if (member != null)
                    {
                        entity.MemberId = member.MemberId;
                    }
                    else
                    {
                        var agent = await GetAgentByLicenceNumber(dto.ReferenceLicenceNumber.ToUpper());
                        if (agent != null)
                            entity.ReferenceAgentId = agent.AgentId;
                    }
                }
                if (entity.ReferenceAgentId == null && entity.MemberId == null)
                {
                    result.message = "Lincence Number or Referal Code is not Valid !!!!";
                    return result;
                }
                entity.AgentFullName = dto.AgentFullName;
                entity.ContactNumber1 = dto.ContactNumber1;
                entity.ContactNumber2 = dto.ContactNumber2;
                entity.EmailAddress = dto.EmailAddress;
                entity.Occupation = dto.Occupation;
                entity.ProvinceId = dto.ProvinceId;
                entity.DistrictId = dto.DistrictId;
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

        public async Task<AgentDto> GetAgentByLicenceNumber(string LicenceNumber)
        {
            string _query = "select AgentId,AgentFullName,LicenceNumber from dbo.[Agent] where LicenceNumber=@LicenceNumber " +
                            "and IsActive=1";
            var obj = await _repository.QueryAsync<AgentDto>(_query,new { LicenceNumber });
            return obj.FirstOrDefault();
        }

        public async Task<string> GetLicenceNumber()
        {
            string LincenceNumber = "LIN-01";
            string _query = "select top 1 LicenceNumber from dbo.[Agent] order by AgentId desc";
            var agent = (await _repository.QueryAsync<AgentDto>(_query)).FirstOrDefault();
            if (agent != null)
            {
                int linLastNum = Convert.ToInt32(agent.LicenceNumber.Split('-')[1])+1;
                string linLastNumString = (linLastNum > 10 ? linLastNum.ToString()
                                           : ("0" + linLastNum));
                LincenceNumber = "LIN-" + linLastNumString;
            }
            return LincenceNumber;
        }
    }
}