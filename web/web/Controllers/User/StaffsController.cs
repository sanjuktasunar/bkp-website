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

namespace web.Controllers.User
{
    //[Authorize]
    public class StaffsController : BaseController
    {
        private IDesignationService _designationService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";

        private IStaffsService _staffsService;
        public StaffsController(IStaffsService staffsService)
        {
            _staffsService = staffsService;
            menu = _initialService.GetMenuPermissionForLoginUser("Staffs");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }
        [Route("~/StaffList")]
        public async Task<ActionResult> Staffs()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _staffsService.GetStaffListAsync();
            return View(obj);
        }

        [Route("~/AddStaff")]
        public ActionResult AddStaff()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new StaffsDto();
            obj = _staffsService.DropDownMethods(obj);

            return View("AddModifyStaff", obj);
        }

        [Route("~/ModifyStaff/{id}")]
        public async Task<ActionResult> ModifyStaff(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _staffsService.GetStaffByIdAsync(Convert.ToInt32(id)));
            obj = _staffsService.DropDownMethods(obj);
            return View("AddModifyStaff", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(StaffsDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _staffsService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(StaffsDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _staffsService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}