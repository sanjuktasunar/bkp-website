using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class OrganizationInfo
    {
        public int OrganizationInfoId { get; set; }
        public string OrganizationName { get; set; }
        public string AppName { get; set; }
        public string Address { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }
        public string POBoxNumber { get; set; }
        public string Logo { get; set; }
        public string Favicon { get; set; }
        public string NormalizedName { get; set; }
    }
}
