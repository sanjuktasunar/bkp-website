using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Utility;
using web.Web.Services.Services;
using Web.Entity.Dto;
using Web.Entity.Infrastructure;
using Web.Services.Services;

namespace web.Controllers.User
{
    [Authorize]
    public class RoleController : BaseController
    {
        private IRoleService _roleService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
            menu = _initialService.GetMenuPermissionForLoginUser("Role");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/RoleList")]
        public async Task<ActionResult> Roles()
        {
            if(!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _roleService.GetAllRoles();
            return View(obj);
        }

        [Route("~/AddRole")]
        public ActionResult AddRole()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new RoleDto();
            return View("AddModifyRole", obj);
        }

        [Route("~/ModifyRole/{id}")]
        public async Task<ActionResult> ModifyRole(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _roleService.GetRoleById(Convert.ToInt32(id)));
            return View("AddModifyRole", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(RoleDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _roleService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(RoleDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _roleService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _roleService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Route("~/MenuAccessPermission/{roleId}")]
        public async Task<ActionResult> MenuAccessPermission(int roleId)
        {
            if (!menu.AdminAccess)
                return Redirect(Logout_Url);

            var obj = (await _roleService.MenuAccessPermissionAsync(roleId));
            if (obj.Status == true)
                return View(obj);
            else
                return Redirect("/RoleList");
        }

        [HttpPost]
        public async Task<JsonResult> MenuAccessPermission(int id, IEnumerable<MenuAccessPermissionDto> dto)
        {
            if (!menu.AdminAccess)
                return null;
            
            var response = await _roleService.AddMenuAccess(id,dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}