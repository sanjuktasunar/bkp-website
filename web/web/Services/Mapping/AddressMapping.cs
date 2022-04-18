using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class AddressMapping
    {
        public static Country ToEntity(this CountryDto dto)
        {
            if (dto == null)
                return null;
            return new Country
            {
                Id=dto.Id,
                Name=dto.Name?.Trim(),
                NepaliName=dto.NepaliName?.Trim(),
                Status=dto.Status,
                IsOutsideNepal=dto.IsOutsideNepal,
            };
        }
    }
}
