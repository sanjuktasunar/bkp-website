//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using web.Services.Services;
//using Web.Entity.Dto;

//namespace web.Controllers.Public
//{
//    public class RegisterController : Controller
//    {

//        private readonly IMemberRegisterService _memberRegisterService;
//        public RegisterController(IMemberRegisterService memberRegisterService)
//        {
//            _memberRegisterService = memberRegisterService;
//        }
//        public async Task<JsonResult> SaveStep1Data(MemberDto dto)
//        {
//            var obj = await _memberRegisterService.SaveStep1Data(dto);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }

//        public async Task<JsonResult> SaveStep2Data(MemberDto dto)
//        {
//            var obj = await _memberRegisterService.SaveStep2Data(dto);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }

//        public async Task<JsonResult> SaveStep3Data(MemberDto dto)
//        {
//            var obj = await _memberRegisterService.SaveStep3Data(dto);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }
//        public async Task<JsonResult> SaveStep4Data(UserDocumentDto dto)
//        {
//            var obj = await _memberRegisterService.SaveStep4Data(dto);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }
//        public async Task<JsonResult> SaveStep5Data(MemberDto dto)
//        {
//            var obj = await _memberRegisterService.SaveStep5Data(dto);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }

//        public async Task<JsonResult> GetMemberByQuery(string query)
//        {
//            var obj = await _memberRegisterService.GetMemberBySearchQuery(query);
//            return Json(obj, JsonRequestBehavior.AllowGet);
//        }
//    }
//}