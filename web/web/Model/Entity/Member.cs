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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DateOfBirthBS { get; set; }
        public string DateOfBirthAD { get; set; }
        public int? GenderId { get; set; }
        public int? OccupationId { get; set; }
        public string OtherOccupationRemarks { get; set; }
        public int? MemberFieldId { get; set; }
        public string CitizenshipNumber { get; set; }
        public bool? IsMemberFilled { get; set; }
        public FormStatus FormStatus { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? ReferenceId { get; set; }
        public string ReferalCode { get; set; }
        public string ApprovalRemarks { get; set; }
        public bool? IsActive { get; set; }
        public int? UserId { get; set; }
        public int? ShareTypeId { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public int? AppliedKitta { get; set; }

        public int? MaritalStatusId { get; set; }
        public int? AgentId { get; set; }
        public int? ShareholderId { get; set; }
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
