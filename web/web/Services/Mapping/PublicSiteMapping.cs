using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Dto.UserSite;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class PublicSiteMapping
    {
        public static CustomerQuery ToEntity(this CustomerQueryDto dto)
        {
            if (dto == null)
                return null;

            return new CustomerQuery
            {
                Id=dto.Id,
                Name=dto.Name,
                PhoneNumber = dto.PhoneNumber,
                EmailAddress = dto.EmailAddress,
                Address = dto.Address,
                Subject = dto.Subject,
                Message = dto.Message,
                CreatedDate=dto.CreatedDate
            };
        }
    }
}
