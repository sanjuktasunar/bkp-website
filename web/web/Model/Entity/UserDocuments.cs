using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class UserDocuments
    {
        public int UserDocumentId { get; set; }
        public int? StaffId { get; set; }
        public int? MemberId { get; set; }
        public string Photo { get; set; }
        public string CitizenshipFront { get; set; }
        public string CitizenshipBack { get; set; }
        public string PanCard { get; set; }
        public string EducationalDocument { get; set; }
        public bool IsImageString { get; set; }
    }
}
