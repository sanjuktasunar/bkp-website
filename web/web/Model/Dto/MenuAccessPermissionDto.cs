using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class MenuAccessPermissionDto
    {
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string ParentMenuNameEnglish { get; set; }
        public string ParentMenuNameNepali { get; set; }
        public string MenuNameEnglish { get; set; }
        public string MenuNameNepali { get; set; }
        public string CheckMenuName { get; set; }
        public string MenuLink { get; set; }
        public int MenuOrder { get; set; }
        public string MenuIcon { get; set; }


        public int MenuAccessPermissionId { get; set; }

        public int? StaffId { get; set; }

        public bool ReadAccess { get; set; }

        public bool WriteAccess { get; set; }

        public bool ModifyAccess { get; set; }

        public bool DeleteAccess { get; set; }

        public bool AdminAccess { get; set; }

        public bool ApprovalAccess { get; set; }

        public bool RejectAccess { get; set; }

        public string MenuName { get; set; }
        public string ParentMenuName { get; set; }

        public IEnumerable<MenuAccessPermissionDto> GetParentMenus { get; set; }
        public IEnumerable<MenuAccessPermissionDto> GetChildMenus { get; set; }

        public int? RoleId { get; set; }
    }
}
