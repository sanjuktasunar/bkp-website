using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Infrastructure;

namespace Web.Entity.Entity
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string HusbandName { get; set; }
        public string FathersName { get; set; }
        public int Age { get; set; }
        public string CitizenshipNumber { get; set; }
        public string FormerAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int? TemporaryDistrictId { get; set; }
        public string TemporaryMunicipalityName { get; set; }
        public string TemporaryWardNumber { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? AppliedShareKitta { get; set; }
        public int? ShareTypeId { get; set; }
        public decimal? TotalShareAmount { get; set; }
        public decimal? TotalSharePaidAmount { get; set; }
        public int? ReferenceId { get; set; }
        public int? AgentId { get; set; }
        public string NomineeName { get; set; }
        public int? SellerMemberId { get; set; }
        public ApprovalStatus IsApproved { get; set; }
        public string RejectRemarks { get; set; }
        public int? IsShareholder { get; set; }
        public string ReferalCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
    }

    public class MemberDetails
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MemberTypeId { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
