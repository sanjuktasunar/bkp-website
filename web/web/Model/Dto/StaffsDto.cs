using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Web.Entity.Dto;

namespace Web.Entity.Dto
{
    public class StaffsDto:BaseDtoData
    {
        public int StaffId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? PhotoStorageId { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoLocation { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? RoleStatus { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool? DesignationStatus { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool? DepartmentStatus { get; set; }
        public string StaffName { get; set; }
        public int GenderId { get; set; }
        public string TemporaryAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string CitizenshipNumber { get; set; }
        public string PanNumber { get; set; }
        public float BasicSalary { get; set; }

        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }

        public string GenderName { get; set; }

        public int UserStatusId { get; set; }

        public string StatusName { get; set; }

        public IEnumerable<DropdownDto> Roles { get; set; }
        public IEnumerable<DropdownDto> Designations { get; set; }
        public IEnumerable<DropdownDto> Departments { get; set; }
        public IEnumerable<DropdownDto> Genders { get; set; }
        public IEnumerable<DropdownDto> UserStatus { get; set; }
        public IEnumerable<MenuAccessPermissionDto> MenuAccessPermissions { get; set; }
    }
}
