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
    public class MemberController : Controller
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
        public ActionResult MemberList(int? approvalStatus, int? FormStatus, int? ReferenceId)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = 2;
            ViewBag.FormStatus = FormStatus;
            ViewBag.ReferenceId = ReferenceId;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FilterMemberList(int? ApprovalStatus, int? FormStatus, int? ReferenceId)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus;
            ViewBag.FormStatus = FormStatus;
            ViewBag.ReferenceId = ReferenceId;
            var obj = await _memberService.Filter(ApprovalStatus, FormStatus, ReferenceId);
            return PartialView("FilterMemberList", obj);
        }

        [Route("~/MemberDetails/{id}")]
        public async Task<ActionResult> MemberDetails(int ? id)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);
            var obj = await _memberService.GetMemberByIdAsync(id);
            return View(obj);
        }

        [Route("~/AddNewMember")]
        public ActionResult AddNewMember()
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            var member = new MemberDto();
            return View("AddModifyNewMember",member);
        }

        [Route("~/ModifyMember/{memberId}")]
        public async Task<ActionResult> ModifyMember(int memberId)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = await _memberService.GetMemberByIdAsync(memberId);
            return View("AddModifyNewMember", obj);
        }

        public async Task<JsonResult> Insert(MemberDto dto)
        {
            if (!menu.ReadAccess)
                return null;

            var resp = await _memberService.Insert(dto);
            return Json(resp,JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ApproveMember(int MemberId, int AccountHeadId)
        {
            if (menu.ApprovalAccess != true)
                return null;
            var obj = await _memberService.ApproveMember(MemberId, AccountHeadId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> RejectMember(int MemberId, string remarks)
        {
            if (menu.RejectAccess != true)
                return null;
            var obj = await _memberService.RejectMember(MemberId, remarks);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetRefernceMember()
        {
            var obj = await _memberService.GetRefernceMemberDropdown();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}