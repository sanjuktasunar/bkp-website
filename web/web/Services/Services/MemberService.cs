using System;
using System.Collections.Generic;
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

namespace web.Services.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto);
    }

    public class MemberService: IMemberService
    {
        private readonly Repository<Member> _repository;
        private readonly MessageClass _messageClass;
        private readonly SqlConnectionDetails _sql;
        private readonly DateSettings _dateSettings;
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _repository = new Repository<Member>();
            _messageClass = new MessageClass();
            _sql = _repository.GetSqlTransactionDetails();
            _dateSettings = new DateSettings();
            _memberRepository = memberRepository;
        }
        public async Task<IEnumerable<MemberDto>> Filter(MemberFilterDto filterDto)
        {
            if (filterDto.ApprovalStatus == 2)
                filterDto.FormStatus = null;

            var obj = await _repository.StoredProcedureAsync<MemberDto>("[dbo].[FilterMember]"
               , new
               {
                   ApprovalStatus = filterDto.ApprovalStatus,
                   ReferenceId = filterDto.ReferenceId,
                   AgentId = filterDto.AgentId,
                   ShareTypeId = filterDto.ShareTypeId
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
    }
}