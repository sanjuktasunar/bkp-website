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
        private readonly SessionManager sessionManager;
        public BaseController()
        {
            initialSetupModel = new InitialSetupModel();
            sessionManager = new SessionManager();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session.Count>0)
            {
                if(!string.IsNullOrEmpty(Session[SessionCodes.SITE_USER_LOGIN_ID].ToString()))
                {
                    ViewBag.VersionInfo = initialSetupModel.GetCurrentVersionInfo();
                    ViewBag.UsersInfo = sessionManager.ReadFromSessionCookie();
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

                    sessionManager.ClearAllSession();
                    filterContext.Result = new RedirectResult(initialSetupModel.RedirectUrl_Logout);
                }
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

                sessionManager.ClearAllSession();
                filterContext.Result = new RedirectResult(initialSetupModel.RedirectUrl_Logout);
            }
        }
    }
}