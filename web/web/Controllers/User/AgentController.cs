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
    public class AgentController : BaseController
    {
        private IAgentService _agentService { get; set; }
        MenuAccessPermissionDto menu = new MenuAccessPermissionDto();
        InitialSetupModel _initialService = new InitialSetupModel();
        private string Logout_Url = "";

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
            menu = _initialService.GetMenuPermissionForLoginUser("Agent");
            Logout_Url = _initialService.RedirectUrl_Logout;
            ViewBag.Menus = menu;
        }

        [Route("~/AgentList")]
        public async Task<ActionResult> AgentList(int? ProvinceId = null, int? DistrictId = null, int? AgentStatusId = null)
        {
            if (!menu.ReadAccess)
                return Redirect(Logout_Url);

            ViewBag.ProvinceId = ProvinceId;
            ViewBag.DistrictId = DistrictId;
            ViewBag.AgentStatusId = AgentStatusId;
            var obj = await _agentService
                     .GetAllAgent(ProvinceId,DistrictId,AgentStatusId,menu.AdminAccess);
            return View(obj);
        }

        [Route("~/AddAgent")]
        public ActionResult AddAgent()
        {
            if (!menu.WriteAccess)
                return Redirect(Logout_Url);

            var obj = new AgentDto();
            return View("AddModifyAgent", obj);
        }

        [Route("~/ModifyAgent/{id}")]
        public async Task<ActionResult> ModifyAgent(int id)
        {
            if (!menu.ModifyAccess)
                return RedirectToAction("Logout", "Account");
            var obj = (await _agentService.GetAgentById(id));
            return View("AddModifyAgent", obj);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(AgentDto dto)
        {
            if (!menu.WriteAccess)
                return null;

            var response = await _agentService.Insert(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Update(AgentDto dto)
        {
            if (!menu.ModifyAccess)
                return null;

            var response = await _agentService.Update(dto);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Route("~/AgentDetails/{id}")]
        public async Task<ActionResult> AgentDetails(int id)
        {
            if (!menu.ReadAccess)
                return RedirectToAction("Logout", "Account");

            var obj = (await _agentService.GetAgentById(id));
            return View(obj);
        }
    }
}