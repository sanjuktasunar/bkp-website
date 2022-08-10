using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web.Utility;
using web.Web.Entity.Dto;
using web.Web.Services.Services;
using Web.Entity.Infrastructure;

namespace web.Controllers.User
{
    public class AjaxController:Controller
    {
        private IDropDownService _dropDownService;
        private CacheManager cacheManager;
        private ImageSettings imageSettings;
        public AjaxController(IDropDownService dropDownService)
        {
            _dropDownService = dropDownService;
            cacheManager = new CacheManager();
            imageSettings = new ImageSettings();
        }

        [HttpPost]
        public JsonResult Index(DropdownRequestDto requestDto)
        {
            var dropdown = new List<DropdownDto>();
            switch(requestDto.type)
            {
                case "role_list":
                    dropdown = _dropDownService.GetDropDownRole().ToList();
                    break;
                case "user_type_list":
                    dropdown = _dropDownService.GetDropDownUserType().ToList();
                    break;
                case "user_type_for_user_list":
                    dropdown = _dropDownService.GetDropDownUserTypeForUserList().ToList();
                    break;
                case "user_status_list":
                    dropdown = _dropDownService.GetDropDownUserStatus().ToList();
                    break;
                case "fiscal_year_list":
                    dropdown = _dropDownService.GetDropDownFiscalYear().ToList();
                    break;
                case "gender_list":
                    dropdown = _dropDownService.GetDropDownGender().ToList();
                    break;
                case "province_list":
                    dropdown = _dropDownService.GetDropDownProvince().ToList();
                    break;
                case "district_list":
                    cacheManager.ClearCacheByKey(CacheCodes.DISTRICT_LIST);
                    dropdown = _dropDownService.GetDropDownDistrict(requestDto.provinceId)
                                .ToList();
                    break;
                case "occupation_list":
                    dropdown = _dropDownService.GetDropDownOccupation().ToList();
                    break;
                case "share_type_list":
                    dropdown = _dropDownService.GetDropDownShareTypes().ToList();
                    break;
                case "share_type_with_detail_list":
                    dropdown = _dropDownService.GetDropDownShareTypesWithDetails().ToList();
                    break;
                case "agent_Status_list":
                    dropdown = _dropDownService.GetDropDownAgentStatus().ToList();
                    break;
                case "account_Head_list":
                    dropdown = _dropDownService.GetDropDownAccountHead().ToList();
                    break;
                case "marital_Status_List":
                    dropdown = _dropDownService.GetDropDownMaritalStatus().ToList();
                    break;
                case "outside_country_List":
                    dropdown = _dropDownService.GetOutsideCountry().ToList();
                    break;
                case "reference_Agent_List":
                    dropdown = _dropDownService.GetReferenceAgentList().ToList();
                    break;
                case "reference_Member_List":
                    dropdown = _dropDownService.GetReferenceMemberList().ToList();
                    break;
                case "shareholder_List":
                    dropdown = _dropDownService.GetShareholderList().ToList();
                    break;
                default:
                    break;
            }
            return Json(dropdown,JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConvertFileToString()
        {
            string imageString = string.Empty;
            var imageResponse = new ImageResponse();
            imageResponse.messageType = "error";
            imageResponse.message = "Something went Wrong,Please select  another file";
            var file = Request.Files[0];
            if (file != null)
            {
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                string[] extensionArray = { ".jpg",".png",".jpeg" };
                if (extensionArray.Contains(extension))
                {
                    imageString = "data:image;base64," + imageSettings.ConvertToString(file);
                    imageResponse.messageType = "success";
                    imageResponse.message = "";
                    imageResponse.imageBase64String = imageString;
                }
                else
                {
                    imageResponse.message = extension.ToLower()+" Files are not allowed,please select jpg,png or jpeg file";
                }
            }
            return Json(imageResponse, JsonRequestBehavior.AllowGet);
        }
    }
}