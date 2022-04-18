using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public IEnumerable<MenuAccessPermissionDto> MenuAccessPermissions { get; set; }
    }
}
