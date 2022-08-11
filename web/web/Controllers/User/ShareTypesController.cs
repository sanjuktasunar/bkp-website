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
    //[Authorize]
    public class ShareTypesController : BaseController
    {
        private IShareTypesService _shareTypeService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";

        public ShareTypesController(IShareTypesService shareTypesService)
        {
            _shareTypeService = shareTypesService;
            menu = _initialService.GetMenuPermissionForLoginUser("ShareType");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [Route("~/ShareTypeList")]
        public async Task<ActionResult> ShareTypeList()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _shareTypeService.GetShareTypesAsync();
            return View(obj);
        }
        [Route("~/AddShareType")]
        public ActionResult AddShareType()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new ShareTypesDto();
            return View("AddModifyShareType", obj);
        }

        [Route("~/ModifyShareType/{id}")]
        public async Task<ActionResult> ModifyShareType(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _shareTypeService.GetShareTypesByIdAsyc(Convert.ToInt32(id)));
            return View("AddModifyShareType", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(ShareTypesDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _shareTypeService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(ShareTypesDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _shareTypeService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _shareTypeService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}