using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.Web.Services;
using Web.Entity.Dto;

namespace web.Utility
{
    public class InitialSetupModel
    {
        public string RedirectUrl_Logout { get; set; }
        public string RedirectUrl_Dashboard { get; set; }
        public string LoginUrl { get; set; }
        public InitialSetupModel()
        {
            RedirectUrl_Logout = "~/Logout";
            RedirectUrl_Dashboard = "~/Dashboard";
            LoginUrl= "~/Login";
        }
        private Repository<UsersDto> _repository = new Repository<UsersDto>();
        public string GetCurrentVersionInfo()
        {
            string version_info = "";
            if (HttpRuntime.Cache[CacheCodes.VERSION_INFO] == null)
            {
                string version = _repository.Query<string>("select top 1 Version from VersionInfo order by Version desc").FirstOrDefault();

                int i = 0;
                foreach (var v in version)
                {
                    i++;
                    if (version.Count() != i)
                        version_info += v + ".";
                    else
                        version_info += v;
                }
                version_info = "version " + version_info;
                HttpRuntime.Cache[CacheCodes.VERSION_INFO] = version_info;
            }
            else
            {
                version_info = HttpRuntime.Cache[CacheCodes.VERSION_INFO] as string;
            }
            return version_info;
        }

        public int LoginUserId()
        {
            int UserId = 0;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name.ToString());
            }
            return UserId;
        }

        public MenuAccessPermissionDto GetMenuPermissionForLoginUser(string checkMenuName)
        {
            var menus = _repository.StoredProcedure<MenuAccessPermissionDto>("[dbo].[MenuAccessFor_LoginUser]",
               new { UserId = LoginUserId(), CheckMenuName= checkMenuName });

            if (menus.Count() == 0)
                return new MenuAccessPermissionDto { CheckMenuName = checkMenuName };
            var menu = menus.FirstOrDefault();
            if (menu.AdminAccess)
            {
                menu.ReadAccess = menu.WriteAccess = menu.ModifyAccess = menu.DeleteAccess = true;
                menu.ApprovalAccess = menu.RejectAccess = true;
            }
            return menu;
        }

        public OrganizationInfoDto GetOrganizationInfo()
        {
            var dto = _repository.Query<OrganizationInfoDto>("SELECT * FROM OrganizationInfo").FirstOrDefault();
            return dto;
        }

        public bool ValidateCitizenshipNumber(string citizenshipNumber)
        {
            bool isValid = true;
            citizenshipNumber = citizenshipNumber.Replace("/", "-");
            var citizenSplit = citizenshipNumber.Split('-').ToList();
            int i = 0;
            foreach (var c in citizenSplit)
            {
                if (!int.TryParse(c.Trim(), out i))
                    return false;
            }
            return isValid;
        }
    }
}