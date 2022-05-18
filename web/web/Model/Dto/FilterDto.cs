using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Model.Dto
{
    public class MemberFilterDto
    {
        public int? ApprovalStatus { get; set; }
        public int? FormStatus { get; set; }
        public int? ReferenceId { get; set; }
        public int? AgentId { get; set; }
        public int? ShareTypeId { get; set; }

        public string SearchQuery { get; set; }
        public string Code { get; set; }
    }
}