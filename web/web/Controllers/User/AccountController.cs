using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using web.Utility;
using web.Web.Services.Services;
using Web.Entity.Dto;
using Web.Entity.Infrastructure;

namespace web.Controllers.User
{
    //[Authorize]
    public class AccountController : Controller
    {
        private IUsersService _usersService;
        private readonly InitialSetupModel setupModel;
        private readonly CacheManager cacheManager;
        private readonly SessionManager sessionManager;
        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
            setupModel = new InitialSetupModel();
            cacheManager = new CacheManager();
            sessionManager = new SessionManager();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("~/Login")]
        public ActionResult Login()
        {
            if (Session.Count > 0)
            {
                if (Session[SessionCodes.SITE_USER_LOGIN_ID].ToString() != null)
                {
                    return Redirect(setupModel.RedirectUrl_Dashboard);
                }
                sessionManager.ClearAllSession();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(UsersDto dto)
        {
            var resp = new Response
            {
                messageType = "error",
                message = "Username or Password do not match"
            };
            var data = await _usersService.GetLoginUser(dto);
            if (data != null)
            {
                if (data.UserStatusId == 1)
                {
                    resp.messageType = "success";
                    resp.message = "Login Successfull!!!";
                    sessionManager.SetToSessionCookie(data);
                }
                else if (data.UserStatusId == 2)
                {
                    dto.Message = "Your Account is not active";
                }

                else if (data.UserStatusId == 3)
                {
                    dto.Message = "You are suspended,Please contact to admin for further information";
                }
            }
            return Json(resp,JsonRequestBehavior.AllowGet);
        }

        [Route("~/Logout")]
        public ActionResult Logout_User()
        {
            EnsureLogout();
            return Redirect(setupModel.LoginUrl);
        }

        public void EnsureLogout()
        {
            ClearCookie();
            cacheManager.ClearAllCache();
            sessionManager.ClearAllSession();
        }

        public void ClearCookie()
        {
            HttpCookie versionCookie = new HttpCookie("versionInfo");
            versionCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(versionCookie);
        }

        [HttpPost]
        public JsonResult ClearAllCache()
        {
            var resp = new Response();
            resp.messageType = "success";
            try
            {
                cacheManager.ClearAllCache();
                
            }
            catch(Exception ex)
            {
                resp.messageType = "error";
                resp.message = ex.Message;
            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}