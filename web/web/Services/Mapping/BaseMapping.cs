using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity;

namespace Web.Services.Mapping
{
    public static class BaseMapping
    {
        public static BaseDto ToDto(this BaseEntity entity)
        {
            if (entity == null)
                return null;

            return new BaseDto
            {
                Status = entity.Status
            };
        }

        public static BaseEntity ToEntity(this BaseDto dto)
        {
            if (dto == null)
                return null;

            return new BaseEntity
            {
                Status = dto.Status
            };
        }

        public static BaseDtoData ToDTO(this BaseEntityData entity)
        {
            if (entity == null)
                return null;

            return new BaseDtoData
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate=entity.CreatedDate,
                UpdatedBy=entity.UpdatedBy,
                UpdatedDate=entity.UpdatedDate
            };
        }

        public static BaseEntityData ToEntity(this BaseDtoData dto)
        {
            if (dto == null)
                return null;

            return new BaseEntityData
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate
            };
        }

        public static BaseDtoAll ToDto(this BaseEntityAll entity)
        {
            if (entity == null)
                return null;

            return new BaseDtoAll
            {
                Status = entity.Status
            };
        }

        public static BaseEntityAll ToEntity(this BaseDtoAll dto)
        {
            if (dto == null)
                return null;

            return new BaseEntityAll
            {
                Status = dto.Status
            };
        }
    }
}
