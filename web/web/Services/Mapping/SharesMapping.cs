using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Model.Dto;
using web.Model.Entity;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class SharesMapping
    {
        public static ShareTypes ToEntity(this ShareTypesDto dto)
        {
            if (dto == null)
                return null;

            return new ShareTypes
            {
                ShareTypeId=dto.ShareTypeId,
                ShareTypeName = dto.ShareTypeName?.Trim(),
                FiscalYearId = dto.FiscalYearId,
                NumberOfIssuedShares = dto.NumberOfIssuedShares,
                MaxSharePerPerson = dto.MaxSharePerPerson,
                MinSharePerPerson = dto.MinSharePerPerson,
                Status = dto.Status,
                IsPrimary = dto.IsPrimary,
                PricePerKitta=dto.PricePerKitta,
                RegistrationAmount=dto.RegistrationAmount,

            };
        }

        public static Shareholder ToEntity(this ShareholderDto dto)
        {
            if (dto == null)
                return null;

            return new Shareholder
            {
                ShareholderId = dto.ShareholderId,
                MemberId = dto.MemberId,
                ShareTypeId = dto.ShareTypeId,
                TotalKitta = dto.TotalKitta,
                IsActive = dto.IsActive,
                ApprovedDate = dto.ApprovedDate,
            };
        }
    }
}
