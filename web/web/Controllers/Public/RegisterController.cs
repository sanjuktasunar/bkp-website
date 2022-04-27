using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Services.Services;
using Web.Entity.Dto;

namespace web.Controllers.Public
{
    public class RegisterController : Controller
    {

        private readonly IMemberRegisterService _memberRegisterService;
        public RegisterController(IMemberRegisterService memberRegisterService)
        {
            _memberRegisterService = memberRegisterService;
        }
        public async Task<JsonResult> SaveStep1Data(MemberDto dto)
        {
            var obj = await _memberRegisterService.SaveStep1Data(dto);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}