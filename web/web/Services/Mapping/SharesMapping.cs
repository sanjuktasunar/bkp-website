using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                PricePerKitta=dto.PricePerKitta
            };
        }
    }
}
