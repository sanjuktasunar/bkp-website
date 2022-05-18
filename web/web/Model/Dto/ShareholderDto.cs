using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Entity.Dto;

namespace web.Model.Dto
{
    public class ShareholderDto
    {
        public int ShareholderId { get; set; }
        public int MemberId { get; set; }
        public int ShareTypeId { get; set; }
        public int TotalKitta { get; set; }
        public int IsActive { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool Status { get; set; }

        public MemberDto memberDto { get; set; }
    }
}