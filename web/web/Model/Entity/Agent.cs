using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entity.Entity
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string AgentFullName { get; set; }
        //public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string MunicipalityName { get; set; }
        public string WardNumber { get; set; }
        public string ToleName { get; set; }
        //public string AgentCurrentAddress { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string EmailAddress { get; set; }
        public string CitizenshipNumber { get; set; }
        public string LicenceNumber { get; set; }
        public string Qualification { get; set; }
        public string Occupation { get; set; }
        public int AgentStatusId { get; set; }
        public bool IsActive { get; set; }
        public int? MemberId { get; set; }
        public int? ReferenceAgentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}