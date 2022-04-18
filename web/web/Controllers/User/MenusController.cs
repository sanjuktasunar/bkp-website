using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Utility;
using Web.Entity.Dto;
using Web.Entity.Infrastructure;
using Web.Services.Services;

namespace web.Controllers.User
{
    public class MenusController : BaseController
    {
        private IMenusService _menusService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public MenusController(IMenusService menusService)
        {
            _menusService = menusService;
            menu = _initialService.GetMenuPermissionForLoginUser("Menus");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [Route("~/MenuList")]
        public async Task<ActionResult> Menus()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var data = await _menusService.GetMenusAsync();
            return View(data);
        }

        [Route("~/AddModifyMenus")]
        [Route("~/AddModifyMenus/{id}")]
        public async Task<ActionResult> AddModifyMenus(int? id)
        {

            var obj = new MenusDto();
            if (id > 0)
            {
                if (!menu.ModifyAccess)
                    return Redirect(Logout_Url);

                obj = await _menusService.GetMenusByIdAsync(Convert.ToInt32(id));
            }
            else
            {
                if (!menu.WriteAccess)
                    return Redirect(Logout_Url);
            }
            return View(obj);

        }

        [HttpPost]
        public async Task<JsonResult> Insert(MenusDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _menusService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(MenusDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _menusService.Update(dto);
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _menusService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}