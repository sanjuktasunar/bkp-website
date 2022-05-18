using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Model.Entity
{
    public class Shareholder
    {
        public int ShareholderId { get; set; }
        public int MemberId { get; set; }
        public int ShareTypeId { get; set; }
        public int TotalKitta { get; set; }
        public int IsActive { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}