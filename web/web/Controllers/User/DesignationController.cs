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
    public class DesignationController : BaseController
    {
        private IDesignationService _designationService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
            menu = _initialService.GetMenuPermissionForLoginUser("Designation");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/DesignationList")]
        public async Task<ActionResult> Designation()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _designationService.GetAllDesignation();
            return View(obj);
        }

        [Route("~/AddDesignation")]
        public ActionResult AddDesignation()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new DesignationDto();
            return View("AddModifyDesignation", obj);
        }

        [Route("~/ModifyDesignation/{id}")]
        public async Task<ActionResult> ModifyDesignation(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _designationService.GetDesignationById(Convert.ToInt32(id)));
            return View("AddModifyDesignation", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(DesignationDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _designationService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(DesignationDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _designationService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _designationService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}