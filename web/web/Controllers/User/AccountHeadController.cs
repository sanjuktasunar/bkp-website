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
    public class AccountHeadController : BaseController
    {
        private IAccountHeadService _accountHeadService { get; set; }
        private MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        private InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public AccountHeadController(IAccountHeadService accountHeadService)
        {
            _accountHeadService = accountHeadService;
            menu = _initialService.GetMenuPermissionForLoginUser("AccountHead");
            ViewBag.Menus = menu;
            Logout_Url = _initialService.RedirectUrl_Logout;
        }
        [Route("~/AccountHeadList")]
        public async Task<ActionResult> AccountHeadList()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _accountHeadService.GetAllAccountHead();
            return View(obj);
        }

        [Route("~/AddAccountHead")]
        public ActionResult AddAccountHead()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new AccountHeadDto();
            return View("AddModifyAccountHead", obj);
        }

        [Route("~/ModifyAccountHead/{id}")]
        public async Task<ActionResult> ModifyAccountHead(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _accountHeadService.GetAccountHeadById(Convert.ToInt32(id)));
            return View("AddModifyAccountHead", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(AccountHeadDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _accountHeadService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(AccountHeadDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _accountHeadService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _accountHeadService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}