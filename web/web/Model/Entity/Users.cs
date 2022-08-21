using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Users : BaseEntityData
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int? PhotoStorageId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public int? RoleId { get; set; }

        public int UserStatusId { get; set; }

        public bool IsSuperAdmin { get; set; }
    }
}
