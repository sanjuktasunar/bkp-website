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
                    int? ShareTypeId = null, int? AgentId = null,string SearchQuery= null, int? SellerMemberId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus == null ? 2 : ApprovalStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;
            ViewBag.SearchQuery = SearchQuery;
            ViewBag.SellerMemberId = SellerMemberId;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FilterMemberList(int? ApprovalStatus, int? ReferenceId, int? ShareTypeId = null, int? AgentId = null,string SearchQuery=null, int ? SellerMemberId=null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;
            ViewBag.SearchQuery = SearchQuery;
            ViewBag.SellerMemberId = SellerMemberId;

            var filterDto = new MemberFilterDto
            {
                ApprovalStatus = ApprovalStatus,
                ReferenceId = ReferenceId,
                ShareTypeId = ShareTypeId,
                AgentId = AgentId,
                SellerMemberId=SellerMemberId,
                SearchQuery=SearchQuery
            };
            var obj = await _memberService.Filter(filterDto);
            return PartialView("FilterMemberList", obj);
        }

        [Route("~/AddNewMember")]
        public ActionResult AddNewMember()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var member = new MemberDto();
            return View("AddModifyNewMember", member);
        }

        [Route("~/ModifyMember/{memberId}")]
        public async Task<ActionResult> ModifyMember(int memberId)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var member = await _memberService.GetMemberById(memberId);
            return View("AddModifyNewMember", member);
        }

        [Route("~/ModifyApprovedMember/{memberId}")]
        public async Task<ActionResult> ModifyApprovedMember(int memberId)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var member = await _memberService.GetMemberById(memberId);
            return View(member);
        }

        [Route("~/MemberDetails/{memberId}")]
        public async Task<ActionResult> MemberDetails(int memberId)
        {
            if (!menu.ModifyAccess)
                return Redirect(Logout_Url);

            var member = await _memberService.GetMemberById(memberId);
            return View(member);
        }

        public async Task<JsonResult> Insert(MemberDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var resp = await _memberService.Insert(dto);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(MemberDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var resp = await _memberService.Update(dto);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateApprovedMember(MemberDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var resp = await _memberService.UpdateApprovedMember(dto);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Approve(int memberId)
        {
            if (!menu.ApprovalAccess)
                return null;

            var resp = await _memberService.Approve(memberId);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Reject(int memberId,string remarks)
        {
            if (!menu.RejectAccess)
                return null;

            var resp = await _memberService.Reject(memberId,remarks);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddPaymentLog(MemberPaymentLogDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var resp = await _memberService.AddMemberPaymentLog(dto);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}