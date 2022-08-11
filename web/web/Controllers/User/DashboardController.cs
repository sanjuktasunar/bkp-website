using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Utility;

namespace web.Controllers.User
{
    //[Authorize]
    public class DashboardController : BaseController
    {
        private readonly InitialSetupModel _initialSetupModel;
        public DashboardController()
        {
            _initialSetupModel = new InitialSetupModel();
        }

        [Route("~/Dashboard")]
        public ActionResult Index()
        {
            return View();
        }
    }
}