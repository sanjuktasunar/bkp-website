using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("~/news")]
        public ActionResult News()
        {
            return View();
        }

        [Route("~/news-detail/{id}")]
        public ActionResult NewsDetail(int id)
        {
            return View();
        }

        [Route("~/action-plan")]
        public ActionResult ActionPlan()
        {
            return View();
        }

        [Route("~/upcoming-project")]
        public ActionResult UpcomingProject()
        {
            return View();
        }

        [Route("~/about-us")]
        public ActionResult AboutUs()
        {
            return View();
        }

        [Route("~/contact-us")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [Route("~/member-register")]
        public ActionResult MemberRegister()
        {
            return View();
        }
    }
}