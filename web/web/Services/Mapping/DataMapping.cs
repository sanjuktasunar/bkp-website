using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Model;

namespace Web.Services.Mapping
{
    public static class DataMapping
    {
        public static DropdownList ToCountryDropdown(this CountryDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.Id,
                Value = dto.Name,
            };
        }

        public static DropdownList ToProvinceDropdown(this ProvinceDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id =dto.ProvinceId,
                Value=dto.ProvinceName,
            };
        }

        public static DropdownList ToDistrictDropdown(this DistrictDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.DistrictId,
                Value = dto.DistrictName,
            };
        }

        public static DropdownList ToMunicipalityTypeDropdown(this MunicipalityTypeDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.Id,
                Value = dto.Name,
            };
        }

        public static DropdownList ToGenderDropdown(this GenderDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.GenderId,
                Value = dto.GenderName,
            };
        }

        public static DropdownList ToMemberFieldDropdown(this MemberFieldDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.Id,
                Value = dto.Name,
            };
        }

        public static DropdownList ToOccupationDropdown(this OccupationDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.Id,
                Value = dto.Name,
            };
        }

        public static DropdownList ToAccountHeadDto(this AccountHeadDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.AccountHeadId,
                Value = dto.AccountHeadName + '('+dto.AccountNumber + ')',
            };
        }

        public static DropdownList ToMemberDto(this MemberDto dto)
        {
            if (dto is null)
                return null;
            return new DropdownList
            {
                Id = dto.MemberId,
                Value = dto.FullName,
            };
        }

        public static DropdownList ToFiscalYearDto(this FiscalYearDto dto)
        {
            if (dto is null)
                return null;

            return new DropdownList
            {
                Id = dto.FiscalYearId,
                Value = dto.FiscalYearName,
            };
        }
    }
}
