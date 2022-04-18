using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class CustomerMapping
    {
        public static CustomerInfo ToEntity(this CustomerInfoDto dto)
        {
            if (dto == null)
                return null;

            return new CustomerInfo
            {
                CustomerId=dto.CustomerId,
                CustomerName=dto.CustomerName?.Trim(),
                Address = dto.Address?.Trim(),
                ProvinceId = dto.ProvinceId,
                DistrictId = dto.DistrictId,
                ContactNumber = dto.ContactNumber?.Trim(),
                ContactNumber1 = dto.ContactNumber1?.Trim(),
                EmailAddress = dto.EmailAddress?.Trim(),
                PanNumber = dto.PanNumber?.Trim(),
                Status = dto.Status,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
            };
        }
    }
}
