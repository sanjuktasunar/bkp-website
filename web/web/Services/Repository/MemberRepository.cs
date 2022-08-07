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
    public interface IMemberRepository
    {
        Task<MemberDto> GetMemberForUpdateAsync(int? MemberId);
        Task<MemberDto> GetMemberByIdAsync(int? id);
        Task<MemberDto> GetMemberByReferalCodeAsync(string ReferalCode);
        Task<MemberDto> GetMemberByPhoneNumberAsync(string contactNumber);
        Task<string> GetReferalCode();
        Task<string> GetMemberCode();
    }

    public class MemberRepository:IMemberRepository
    {
        private readonly Repository<Member> _repository;
        public MemberRepository()
        {
            _repository = new Repository<Member>();
        }

        public async Task<MemberDto> GetMemberForUpdateAsync(int? MemberId)
        {
            string query = "select * from dbo.[Member] " +
                            "where MemberId=@MemberId";

            var obj = (await _repository.QueryAsync<MemberDto>(query, new { MemberId })).FirstOrDefault();
            return obj;
        }

        public async Task<MemberDto> GetMemberByIdAsync(int? id)
        {
            var obj = (await _repository.QueryAsync<MemberDto>("SELECT * " +
                "FROM dbo.MemberDetailView " +
                "WHERE MemberId=@id", new { id })).FirstOrDefault();
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
    }
}