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
    public class DepartmentController : BaseController
    {
        private IDepartmentService _departmentService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            menu = _initialService.GetMenuPermissionForLoginUser("Department");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [HttpGet]
        [Route("~/DepartmentList")]
        public async Task<ActionResult> Department()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _departmentService.GetAllDepartment();
            return View(obj);
        }

        [Route("~/AddDepartment")]
        public ActionResult AddDepartment()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new DepartmentDto();
            return View("AddModifyDepartment", obj);
        }

        [Route("~/ModifyDepartment/{id}")]
        public async Task<ActionResult> ModifyDepartment(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _departmentService.GetDepartmentById(Convert.ToInt32(id)));
            return View("AddModifyDepartment", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(DepartmentDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _departmentService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(DepartmentDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _departmentService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _departmentService.Delete(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}