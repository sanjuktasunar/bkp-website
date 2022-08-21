using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Model.Dto;
using web.Web.Services;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace web.Services
{
    public interface IMemberRepository
    {
        Task<MemberDto> GetMemberByIdAsync(int? MemberId);
        Task<MemberDto> GetMemberByReferalCodeAsync(string ReferalCode);
        Task<MemberDto> GetMemberByPhoneNumberAsync(string contactNumber);
        Task<string> GetReferalCode();
        Task<string> GetMemberCode();
        Task<RefernceIdsDto> GetRefernceAgentMemberId(string ReferenceLicenceNumber,
            string PhoneNumber);
        Task<IEnumerable<MemberPaymentLogDto>> GetMemberPaymentLog(int memberId);
        Task<ShareholderDto> GetShareholderByMemberId(int memberId);
        Task<ShareholderDto> GetShareholderByShareholderIdAsync(int id);
    }

    public class MemberRepository:IMemberRepository
    {
        private readonly Repository<Member> _repository;
        private readonly IAgentRepository _agentRepository;
        private readonly NumberSettings numberSettings;
        public MemberRepository(IAgentRepository agentRepository)
        {
            _repository = new Repository<Member>();
            _agentRepository = agentRepository;
            numberSettings = new NumberSettings();
        }

        public async Task<MemberDto> GetMemberByIdAsync(int? MemberId)
        {
            string query = "select * from dbo.[MemberView] " +
                            "where MemberId=@MemberId";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { MemberId })).FirstOrDefault();
            if (obj != null)
            {
                obj.TotalShareAmount = obj.AppliedShareKitta * obj.SharePricePerKitta;
                obj.AppliedShareKittaString = numberSettings.CommaSeparate(Convert.ToDecimal(obj.AppliedShareKitta));
                obj.SharePricePerKittaString = numberSettings.CommaSeparate(Convert.ToDecimal(obj.SharePricePerKitta));
                obj.TotalShareAmountString = numberSettings.CommaSeparate(Convert.ToDecimal(obj.TotalShareAmount));
                obj.TotalSharePaidAmountString = numberSettings.CommaSeparate(Convert.ToDecimal(obj.TotalSharePaidAmount));
                obj.TotalShareDueAmount = (Convert.ToDecimal(obj.TotalShareAmount) - Convert.ToDecimal(obj.TotalSharePaidAmount));
                obj.TotalShareDueAmountString = numberSettings.CommaSeparate(obj.TotalShareDueAmount);
                obj.MemberPaymentLogDtos = (await GetMemberPaymentLog(obj.MemberId)).ToList();
            }
            return obj;
        }

        public async Task<MemberDto> GetMemberByReferalCodeAsync(string ReferalCode)
        {
            string query = "select MemberId,ReferalCode,ReferenceId from dbo.[Member] " +
                            "where ReferalCode=@ReferalCode and " +
                            "IsApproved=2";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { ReferalCode })).FirstOrDefault();
            return obj;
        }

        public async Task<MemberDto> GetMemberByPhoneNumberAsync(string contactNumber)
        {
            string query = "select MemberId,ReferalCode,ReferenceId from dbo.[Member] " +
                            "where ContactNumber=@contactNumber and " +
                            "IsApproved=2";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { contactNumber })).FirstOrDefault();
            return obj;
        }

        public async Task<ShareholderDto> GetShareholderByMemberId(int memberId)
        {
            string query = "select * from dbo.[Shareholder] " +
                            "where MemberId=@memberId";

            var obj = (await _repository.QueryAsync<ShareholderDto>(query, new { memberId })).FirstOrDefault();
            return obj;
        }

        public async Task<ShareholderDto> GetShareholderByShareholderIdAsync(int id)
        {
            string query = "select top 1 * from dbo.[Shareholder] where ShareholderId=@id";
            var obj = (await _repository.QueryAsync<ShareholderDto>
                (query, new { id })).FirstOrDefault();
            return obj;
        }

        public async Task<IEnumerable<MemberPaymentLogDto>> GetMemberPaymentLog(int memberId)
        {
            string query = "select * from dbo.[MemberPaymentLog] " +
                            "where MemberId=@memberId and IsDeleted=0 ";

            var obj = (await _repository.QueryAsync<MemberPaymentLogDto>(query, new { memberId }));
            return obj;
        }

        public async Task<string> GetReferalCode()
        {
            var members = (await _repository.QueryAsync<Member>("SELECT TOP 1 " +
                "ReferalCode,MemberId FROM Member where ReferalCode is not null " +
                "ORDER BY cast((substring(ReferalCode, 10,20)) as int ) DESC"));
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
            var memberDto = (await _repository.QueryAsync<MemberDto>("SELECT TOP 1 " +
                "MemberCode FROM Member " +
                "where MemberCode is not null " +
                "ORDER BY cast((substring(MemberCode, 10,20)) as int ) DESC"))
                .FirstOrDefault();

            string memberCode = MakeMemberCodeString(memberDto);
            return memberCode;
        }

        public async Task<MemberDto> GetMemberByMemberCode(string memberCode)
        {
            var memberDto = (await _repository.QueryAsync<MemberDto>("select top 1 " +
                "MemberCode from dbo.Member " +
                "where MemberCode=@memberCode", new { memberCode })).FirstOrDefault();

            return memberDto;
        }

        public string MakeMemberCodeString(MemberDto memberDto)
        {
            string memberCode = string.Empty;
            int i = 78;
            DateTime currentDate = DateTime.Now;
            if (memberDto!=null)
            {
                var number = memberDto.MemberCode.Split('-');
                i = Convert.ToInt32(number[2]);
                i = i + 1;
            }
            memberCode = "BKP-" + currentDate.Year + "-" + i;
            return memberCode;
        }

        public async Task<RefernceIdsDto> GetRefernceAgentMemberId(string ReferenceLicenceNumber, 
            string PhoneNumber)
        {
            var response = new RefernceIdsDto();
            int? MemberId = null, AgentId = null;
            var member = new MemberDto();
            var agent = new AgentDto();
            if (!string.IsNullOrEmpty(ReferenceLicenceNumber))
            {
                member = await GetMemberByReferalCodeAsync(ReferenceLicenceNumber.ToUpper());

                if (member != null)
                    MemberId = member.MemberId;
                else
                {
                    agent = await _agentRepository.GetAgentByLicenceNumberAsync(ReferenceLicenceNumber.ToUpper());
                    if (agent != null)
                        AgentId = agent.AgentId;
                }
            }
            else
            {
                member = await GetMemberByPhoneNumberAsync(PhoneNumber);
                if (member != null)
                    MemberId = member.MemberId;
                else
                {
                    agent = await _agentRepository.GetAgentByPhoneNumberAsync(PhoneNumber);
                    if (agent != null)
                        AgentId = agent.AgentId;
                }
            }
            response.AgentId = AgentId;
            response.MemberId = MemberId;
            return response;
        }
    }
}