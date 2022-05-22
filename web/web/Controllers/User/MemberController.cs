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
        public ActionResult MemberList(int? approvalStatus, int? FormStatus, int? ReferenceId, 
            int? ShareTypeId = null, int? AgentId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = approvalStatus==null?2:approvalStatus;
            ViewBag.FormStatus = FormStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FilterMemberList(int? ApprovalStatus, int? FormStatus, int? ReferenceId,int ? ShareTypeId= null, int? AgentId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ApprovalStatus = ApprovalStatus;
            ViewBag.FormStatus = FormStatus;
            ViewBag.ReferenceId = ReferenceId;
            ViewBag.ShareTypeId = ShareTypeId;
            ViewBag.AgentId = AgentId;

            var filterDto = new MemberFilterDto 
            {
                ApprovalStatus= ApprovalStatus,
                FormStatus = FormStatus,
                ReferenceId = ReferenceId,
                ShareTypeId = ShareTypeId,
                AgentId=AgentId
            };
            var obj = await _memberService.Filter(filterDto);
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
        public async Task<ActionResult> ModifyMember(int memberId,string return_Url=null)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");

            ViewBag.ReturnUrl = string.IsNullOrEmpty(return_Url)?"/MemberList":return_Url;
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
        public async Task<JsonResult> Update(MemberDto dto)
        {
            if (!menu.ReadAccess)
                return null;

            var resp = await _memberService.Update(dto);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ApproveMember(int MemberId, int AccountHeadId,int ShareTypeId)
        {
            if (menu.ApprovalAccess != true)
                return null;
            var obj = await _memberService.ApproveMember(MemberId, AccountHeadId,ShareTypeId);
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

        [HttpPost]
        public async Task<JsonResult> GetMemberDocuments(int memberId)
        {
            var obj = await _memberService.GetMemberDocuments(memberId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddMemberToShareholder(ShareholderDto dto)
        {
            if (menu.ApprovalAccess != true)
                return null;

            var obj = await _memberService.AddMemberToShareholder(dto);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}