using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Utility;
using web.Web.Services;
using Web.Entity.Dto;

namespace web.Controllers.User
{
    public class BaseController : Controller
    {
        private readonly InitialSetupModel initialSetupModel;
        public BaseController()
        {
            initialSetupModel = new InitialSetupModel();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                //ViewBag.UsersInfo = _repository.GetCurrentUsersInfo();
                ViewBag.VersionInfo = initialSetupModel.GetCurrentVersionInfo();
            }
            else
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.ClearHeaders();
                Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");

                filterContext.HttpContext.Response.Clear();
                filterContext.Result = new RedirectResult(initialSetupModel.RedirectUrl_Logout);
            }
        }
    }
}