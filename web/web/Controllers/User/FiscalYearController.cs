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
    public class FiscalYearController : BaseController
    {
        private readonly IFiscalYearService _fiscalYearService;
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public FiscalYearController(IFiscalYearService fiscalYearService)
        {
            _fiscalYearService = fiscalYearService;
            menu = _initialService.GetMenuPermissionForLoginUser("FiscalYear");
            ViewBag.Menus = menu;
            Logout_Url = _initialService.RedirectUrl_Logout;
        }

        [Route("~/FiscalYearList")]
        public async Task<ActionResult> FiscalYearList()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var obj = await _fiscalYearService.GetAllFiscalYearAsync();
            return View(obj);
        }

        [Route("~/AddFiscalYear")]
        public ActionResult AddFiscalYear()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new FiscalYearDto();
            return View("AddModifyFiscalYear", obj);
        }

        [Route("~/ModifyFiscalYear/{id}")]
        public async Task<ActionResult> ModifyFiscalYear(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = (await _fiscalYearService.GetFiscalYearByIdAsync(Convert.ToInt32(id)));
            return View("AddModifyFiscalYear", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Post(FiscalYearDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _fiscalYearService.InsertAsync(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Put(FiscalYearDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _fiscalYearService.UpdateAsync(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            if (!menu.DeleteAccess)
                return null;

            var response = await _fiscalYearService.DeleteAsync(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}