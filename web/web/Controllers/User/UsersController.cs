using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Utility;
using web.Web.Services.Services;
using Web.Entity.Dto;

namespace web.Controllers.User
{
    //[Authorize]
    public class UsersController : BaseController
    {
        private IUsersService _usersService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
            menu = _initialService.GetMenuPermissionForLoginUser("Users");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [Route("~/UserList")]
        public async Task<ActionResult> UserList()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _usersService.GetUsersList();
            return View(obj);
        }

        [Route("~/AddUser")]
        public ActionResult AddUser()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new UsersDto();
            return View("AddModifyUser",obj);
        }

        [Route("~/ModifyUser/{id}")]
        public async Task<ActionResult> ModifyUser(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = await _usersService.GetUserById(id);
            return View("AddModifyUser",obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(UsersDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _usersService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UsersDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _usersService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}