using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Web.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace web.Services
{
    public interface IAgentRepository
    {
        Task<AgentDto> GetAgentByLicenceNumberAsync(string LicenceNumber);
        Task<AgentDto> GetAgentByPhoneNumberAsync(string phoneNumber);
        Task<string> GetLicenceNumberAsync();
    }

    public class AgentRepository:IAgentRepository
    {
        private readonly Repository<Agent> _repository;
        public AgentRepository()
        {
            _repository = new Repository<Agent>();
        }
        public async Task<AgentDto> GetAgentByLicenceNumberAsync(string LicenceNumber)
        {
            string _query = "select AgentId,AgentFullName,LicenceNumber from dbo.[Agent] where LicenceNumber=@LicenceNumber " +
                            "and IsActive=1";
            var obj = await _repository.QueryAsync<AgentDto>(_query, new { LicenceNumber });
            return obj.FirstOrDefault();
        }

        public async Task<AgentDto> GetAgentByPhoneNumberAsync(string phoneNumber)
        {
            string _query = "select AgentId,AgentFullName,LicenceNumber from dbo.[Agent] " +
                            "where ContactNumber1=@phoneNumber " +
                            "and IsActive=1";
            var obj = await _repository.QueryAsync<AgentDto>(_query, new { phoneNumber });
            return obj.FirstOrDefault();
        }

        public async Task<string> GetLicenceNumberAsync()
        {
            string LincenceNumber = "LIN-01";
            string _query = "select top 1 LicenceNumber from dbo.[Agent] order by AgentId desc";
            var agent = (await _repository.QueryAsync<AgentDto>(_query)).FirstOrDefault();
            if (agent != null)
            {
                int linLastNum = Convert.ToInt32(agent.LicenceNumber.Split('-')[1]) + 1;
                string linLastNumString = (linLastNum > 10 ? linLastNum.ToString()
                                           : ("0" + linLastNum));
                LincenceNumber = "LIN-" + linLastNumString;
            }
            return LincenceNumber;
        }
    }
}