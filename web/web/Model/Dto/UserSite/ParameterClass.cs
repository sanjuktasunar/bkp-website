using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entity.Dto.UserSite
{
    public class ParameterClass
    {
        public string Home { get; set; }
        public string AboutUs { get; set; }
        public string ContactUs { get; set; }
        public string Login { get; set; }
        public string Products { get; set; }
        public string OurProducts { get; set; }
        public string Per { get; set; }
        public string Rs { get; set; }
        public string Search { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string ContactUsHeader { get; set; }
        public string OurCompany { get; set; }
        public string ClientContact { get; set; }
        public string FutureProjects { get; set; }
        public string OurVision { get; set; }
        public string OurMission { get; set; }
        public string OurGoal { get; set; }
        public string StayUptoDate { get; set; }
        public string MemberRegistration { get; set; }
        public string Forms { get; set; }

        public MenuLink MenuLink { get; set; }
    }

    public class MenuLink
    {
        public string HomeLink { get; set; }
        public string AboutUsLink { get; set; }
        public string ContactUsLink { get; set; }
        public string LoginLink { get; set; }
        public string ProductLink { get; set; }
        public string MemberRegistrationLink { get; set; }
        public string FormLink { get; set; }
    }
}