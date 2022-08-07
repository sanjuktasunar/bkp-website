using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Model.Dto;
using web.Services.Services;
using web.Utility;
using Web.Entity.Dto;

namespace web.Controllers.User
{
    [Authorize]
    public class MemberController : BaseController
    {
        private IMemberService _memberService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
            menu = _initialService.GetMenuPermissionForLoginUser("Member");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }
        [Route("~/MemberList")]
        public ActionResult MemberList(int? ApprovalStatus, int? ReferenceId,
                    int? ShareTypeId = null, int? AgentId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus == null ? 2 : ApprovalStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FilterMemberList(int? ApprovalStatus, int? ReferenceId, int? ShareTypeId = null, int? AgentId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;

            var filterDto = new MemberFilterDto
            {
                ApprovalStatus = ApprovalStatus,
                ReferenceId = ReferenceId,
                ShareTypeId = ShareTypeId,
                AgentId = AgentId
            };
            var obj = await _memberService.Filter(filterDto);
            return PartialView("FilterMemberList", obj);
        }
    }
}