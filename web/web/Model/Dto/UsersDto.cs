using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class UsersDto:BaseDtoData
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int? PhotoStorageId { get; set; }
        public int RoleId { get; set; }

        [Required]
        public string UserName { get; set; }
        public string EncryptUserName { get; set; }
        public string DescryptUserName { get; set; }

        public string Password { get; set; }
        
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public int UserStatusId { get; set; }
        public string Message { get; set; }

        public bool IsSuperAdmin { get; set; }
        public string RoleName { get; set; }
        public string StatusName { get; set; }
        public string UserTypeTitle { get; set; }
        public string FullName { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
