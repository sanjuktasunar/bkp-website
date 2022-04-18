using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Utility;

namespace web.Controllers
{
    public class PublicBaseController : Controller
    {
        protected InitialSetupModel _initialSetupModel = new InitialSetupModel();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.OrganizationInfo = _initialSetupModel.GetOrganizationInfo();
        }
    }
}