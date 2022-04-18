using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entity.Dto.UserSite
{
    public class DisplayParamters
    {
        public int GetLanguageId()
        {
            int LanguageId = 2;
            if (HttpContext.Current.Session.Count>0)
            {
                LanguageId = Convert.ToInt32(HttpContext.Current.Session["LangId"]);
            }
            //return LanguageId;
            return 2;
        }
      
        public ParameterClass GetParameters()
        {
            int LanguageId = GetLanguageId();
            var obj = new ParameterClass();
            if(LanguageId==2)
            {
                obj.Home = "गृहपृष्ठ";
                obj.AboutUs = "हाम्रोबारेमा";
                obj.ContactUs = "संपर्क";
                obj.Login = "लगइन";
                obj.Products = "सामनहरु";
                obj.OurProducts = "हाम्रोसामनहरु";
                obj.Per = "प्रति";
                obj.Rs = "रु.";
                obj.Search = "खोजीगर्नुहोस्";
                obj.PhoneNumber = "संपर्क न.";
                obj.Address = "ठेगाना";
                obj.EmailAddress = "ईमेल";
                obj.ContactUsHeader = "हामी सगं जुट्नुहोस्";
                obj.OurCompany = "हाम्रो कम्पनी";
                obj.ClientContact = "ग्राहक संपर्क";
                obj.FutureProjects = "भावी परियोजना";
                obj.OurVision = "भिजन";
                obj.OurMission = "मिसन";
                obj.OurGoal = "लक्ष्य";
                obj.StayUptoDate = "हामी सगं जोड्डिनुहोस्";
                obj.MemberRegistration = "Register";
                obj.Forms = "फारामहरू";
            }
            else
            {
                obj.Home = "Home";
                obj.AboutUs = "About US";
                obj.ContactUs = "Contact Us";
                obj.Login = "Log In";
                obj.Products = "Products";
                obj.OurProducts = "Our Products";
                obj.Per = "Per";
                obj.Rs = "Rs.";
                obj.Search = "Search";
                obj.PhoneNumber = "Phone Number";
                obj.Address = "Address";
                obj.EmailAddress = "Email Address";
                obj.ContactUsHeader = "Create success campaign with us";
                obj.OurCompany = "Our Company";
                obj.ClientContact = "For Client";
                obj.FutureProjects = "Future Projects";
                obj.OurVision = "Our Vision";
                obj.OurMission = "Our Mission";
                obj.OurGoal = "Our Goal";
                obj.StayUptoDate = "Stay up to date with us";
                obj.MemberRegistration = "Register";
                obj.Forms = "Forms";
            }
            return obj;
        }

        public MenuLink GetMenuLink()
        {
            return new MenuLink
            {
                HomeLink = "/",
                AboutUsLink = "/AboutUs",
                ContactUsLink = "/ContactUs",
                LoginLink = "/Login",
                ProductLink = "/Products",
                MemberRegistrationLink = "/MemberRegistration",
                FormLink = "/Forms",
            };
        }
    }
}