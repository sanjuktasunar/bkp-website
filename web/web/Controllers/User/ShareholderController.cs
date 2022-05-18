using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Model.Dto;
using web.Utility;
using web.Web.Services.Services;
using Web.Entity.Dto;
using Web.Entity.Infrastructure;
using Web.Services.Services;


namespace web.Controllers.User
{
    public class ShareholderController : BaseController
    {
        private IMemberService _memberService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public ShareholderController(IMemberService memberService)
        {
            _memberService = memberService;
            menu = _initialService.GetMenuPermissionForLoginUser("Shareholder");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }
        [Route("~/ShareholderList")]
        public ActionResult ShareholderList(MemberFilterDto dto)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ReferenceId = dto.ReferenceId;
            ViewBag.AgentId = dto.AgentId;
            ViewBag.ShareTypeId = dto.ShareTypeId;
            ViewBag.SearchQuery = dto.SearchQuery;
            ViewBag.Code = dto.Code;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FilterShareholderList(MemberFilterDto dto)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ReferenceId = dto.ReferenceId;
            ViewBag.AgentId = dto.AgentId;
            ViewBag.ShareTypeId = dto.ShareTypeId;
            ViewBag.SearchQuery = dto.SearchQuery;
            ViewBag.Code = dto.Code;

            var obj = await _memberService.FilterShareholder(dto);
            return PartialView("FilterShareholderList", obj);
        }

        [Route("~/ModifyShareholder/{id}")]
        public async Task<ActionResult> ModifyShareholder(int id)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var obj = await _memberService.GetShareholderByShareholderId(id);
            return View(obj);
        }

        [HttpPost]
        public async Task<JsonResult> ModifyShareholder(ShareholderDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var obj = await _memberService.ModifyShareholder(dto);
            return Json(obj,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteShareholder(int id)
        {
            if (!menu.ModifyAccess)
                return null;

            var obj = await _memberService.DeleteShareholder(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}