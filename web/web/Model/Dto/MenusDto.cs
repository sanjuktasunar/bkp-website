using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class MenusDto:BaseDto
    {
        public int MenuId { get; set; }
        //public int? ParentMenuId { get; set; }
        //public string ParentMenuNameEnglish { get; set; }
        //public string ParentMenuNameNepali { get; set; }
        public string MenuNameEnglish { get; set; }
        //public string MenuNameNepali { get; set; }
        public string CheckMenuName { get; set; }
        //public string MenuLink { get; set; }
        //public int MenuOrder { get; set; }
        //public string MenuIcon { get; set; }

        //public IEnumerable<MenusDto> GetParentMenus { get; set; }
        //public IEnumerable<MenusDto> GetChildMenus { get; set; }
    }
}
