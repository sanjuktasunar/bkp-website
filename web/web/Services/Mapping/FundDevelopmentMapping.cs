using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace web.Web.Services.Mapping
{
    public static class FundDevelopmentMapping
    {
        public static Agent ToEntity(this AgentDto dto)
        {
            if (dto == null)
                return null;

            return new Agent
            {
                AgentId=dto.AgentId,
                AgentFullName = dto.AgentFullName,
                ProvinceId=dto.ProvinceId,
                DistrictId=dto.DistrictId,
                MunicipalityName = dto.MunicipalityName,
                WardNumber=dto.WardNumber,
                ToleName=dto.ToleName,
                ContactNumber1 = dto.ContactNumber1,
                ContactNumber2 = dto.ContactNumber2,
                EmailAddress = dto.EmailAddress,
                CitizenshipNumber = dto.CitizenshipNumber,
                LicenceNumber = dto.LicenceNumber,
                Qualification = dto.Qualification,
                Occupation = dto.Occupation,
                AgentStatusId = dto.AgentStatusId,
                IsActive = dto.IsActive,
                MemberId = dto.MemberId,
                ReferenceAgentId = dto.ReferenceAgentId,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate
            };
        }
    }
}