using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Utility;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class AdministrationMapping
    {
        private static DateSettings dateSettings = new DateSettings();
        public static UsersDto ToDto(this Users entity)
        {
            if (entity == null)
                return null;

            return new UsersDto
            {
                UserId = entity.UserId,
                UserTypeId = entity.UserTypeId,
                PhotoStorageId = entity.PhotoStorageId,
                UserName = entity.UserName,
                Password = entity.Password,
                EmailAddress = entity.EmailAddress,
                ContactNumber=entity.ContactNumber,
                UserStatusId=entity.UserStatusId
            };
        }

        public static Users ToEntity(this UsersDto dto)
        {
            if (dto == null)
                return null;

            return new Users
            {
                UserId = dto.UserId,
                UserTypeId = dto.UserTypeId,
                PhotoStorageId = dto.PhotoStorageId,
                UserName = dto.UserName,
                Password = dto.Password,
                EmailAddress = dto.EmailAddress,
                ContactNumber = dto.ContactNumber,
                IsSuperAdmin=dto.IsSuperAdmin,
                CreatedBy=dto.CreatedBy,
                CreatedDate=dto.CreatedDate,
                UserStatusId = dto.UserStatusId
            };
        }

        public static Menus ToEntity(this MenusDto dto)
        {
            if (dto == null)
                return null;

            return new Menus
            {
                MenuId = dto.MenuId,
                MenuNameEnglish = dto.MenuNameEnglish?.Trim(),
                CheckMenuName = dto.CheckMenuName?.Trim(),
                Status=dto.Status,
            };
        }

        public static Staffs ToEntity(this StaffsDto dto)
        {
            if (dto == null)
                return null;

            return new Staffs
            {
                StaffId=dto.StaffId,
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                DesignationId = dto.DesignationId,
                DepartmentId = dto.DepartmentId,
                StaffName = dto.StaffName,
                GenderId = dto.GenderId,
                TemporaryAddress = dto.TemporaryAddress,
                PermanentAddress = dto.PermanentAddress,
                CitizenshipNumber = dto.CitizenshipNumber,
                PanNumber = dto.PanNumber,
                BasicSalary = dto.BasicSalary,
            };
        }

        public static Users ToUserEntity(this StaffsDto dto)
        {
            if (dto == null)
                return null;

            return new Users
            {
                UserId=dto.UserId,
                UserTypeId = 2,
                PhotoStorageId = dto.PhotoStorageId,
                UserName=dto.UserName,
                Password=dto.Password,
                EmailAddress=dto.EmailAddress,
                ContactNumber=dto.ContactNumber,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                UserStatusId = dto.UserStatusId,
                RoleId=dto.RoleId
            };
        }

        public static MenuAccessPermission ToEntity(this MenuAccessPermissionDto dto)
        {
            if (dto == null)
                return null;

            return new MenuAccessPermission
            {
                MenuAccessPermissionId=dto.MenuAccessPermissionId,
                MenuId=dto.MenuId,
                ReadAccess=dto.ReadAccess,
                WriteAccess=dto.WriteAccess,
                ModifyAccess=dto.ModifyAccess,
                DeleteAccess=dto.DeleteAccess,
                AdminAccess=dto.AdminAccess,
                ApprovalAccess=dto.ApprovalAccess,
                RejectAccess=dto.RejectAccess,
                RoleId=dto.RoleId
            };
        }

        public static Role ToEntity(this RoleDto dto)
        {
            if (dto == null)
                return null;

            return new Role
            {
                RoleId=dto.RoleId,
                RoleName = dto.RoleName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }

        public static Designation ToEntity(this DesignationDto dto)
        {
            if (dto == null)
                return null;

            return new Designation
            {
                DesignationId = dto.DesignationId,
                DesignationName = dto.DesignationName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }

        public static Department ToEntity(this DepartmentDto dto)
        {
            if (dto == null)
                return null;

            return new Department
            {
                DepartmentId = dto.DepartmentId,
                DepartmentName = dto.DepartmentName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }

        public static OrganizationInfo ToEntity(this OrganizationInfoDto dto)
        {
            if (dto == null)
                return null;

            return new OrganizationInfo
            {
                OrganizationInfoId = dto.OrganizationInfoId,
                OrganizationName=dto.OrganizationName,
                AppName = dto.AppName,
                Address = dto.Address,
                ContactNumber1 = dto.ContactNumber1,
                ContactNumber2 = dto.ContactNumber2,
                TelephoneNumber = dto.TelephoneNumber,
                EmailAddress = dto.EmailAddress,
                FaxNumber = dto.FaxNumber,
                POBoxNumber = dto.POBoxNumber,
                Logo = dto.Logo,
                Favicon = dto.Favicon,
                NormalizedName = dto.NormalizedName,
            };
        }

        public static FiscalYear ToEntity(this FiscalYearDto dto)
        {
            if (dto == null)
                return null;

            dto.StartDateAD = Convert.ToDateTime(dateSettings.ConvertToEnglishDate(dto.StartDateBS));
            dto.EndDateAD = Convert.ToDateTime(dateSettings.ConvertToEnglishDate(dto.EndDateBS));
            return new FiscalYear
            {
                FiscalYearId = dto.FiscalYearId,
                FiscalYearName = dto.FiscalYearName?.Trim(),
                StartDateBS = dto.StartDateBS?.Trim(),
                StartDateAD = dto.StartDateAD,
                EndDateBS = dto.EndDateBS?.Trim(),
                EndDateAD = dto.EndDateAD,
                IsCurrent = dto.IsCurrent,
                Status = dto.Status,
            };
        }
    }
}
