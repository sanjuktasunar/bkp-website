using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class FundDevelopmentMapping
    {
        public static string TrimCitizenshipNumber(this string CitizenshipNumber)
        {
            string trimTag = "";
            string checkString = CitizenshipNumber;
            List<string> splitStr = new List<string>();
            if (CitizenshipNumber.Contains('/'))
            {
                checkString = string.Empty;
                trimTag = "/";
                splitStr = CitizenshipNumber.Split('/').ToList();
                int i = 0;
                foreach (var s in splitStr)
                {
                    if (i == 0)
                        checkString = s.Trim();
                    else
                        checkString += trimTag + s.Trim();
                    i++;
                }
                if (splitStr.Count() > 0)
                {
                    CitizenshipNumber = checkString;
                }
            }
            if (CitizenshipNumber.Contains('-'))
            {
                trimTag = "-";
                splitStr = new List<string>();
                checkString = string.Empty;
                splitStr = CitizenshipNumber.Split('-').ToList();
                int i = 0;
                foreach (var s in splitStr)
                {
                    if (i == 0)
                        checkString += s.Trim();
                    else
                        checkString += trimTag + s.Trim();
                    i++;
                }
                if (splitStr.Count() > 0)
                {
                    CitizenshipNumber = checkString;
                }
            }
            return CitizenshipNumber;
        }

        public static Agent ToEntity(this AgentDto dto)
        {
            if (dto == null)
                return null;

            return new Agent
            {
                AgentId=dto.AgentId,
                AgentFullName = dto.AgentFullName,
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

        public static Member ToEntity(this MemberDto dto)
        {
            if (dto == null)
                return null;

            return new Member
            {
                MemberId = dto.MemberId,
                MemberCode = dto.MemberCode?.Trim(),
                FullName = dto.FullName?.Trim(),
                HusbandName = dto.HusbandName?.Trim(),
                FathersName = dto.FathersName?.Trim(),
                Age = dto.Age,
                CitizenshipNumber = dto.CitizenshipNumber?.Trim(),
                FormerAddress = dto.FormerAddress?.Trim(),
                PermanentAddress = dto.PermanentAddress?.Trim(),
                TemporaryDistrictId = dto.TemporaryDistrictId,
                TemporaryMunicipalityName = dto.TemporaryMunicipalityName?.Trim(),
                TemporaryWardNumber = dto.TemporaryWardNumber?.Trim(),
                ContactNumber = dto.ContactNumber?.Trim(),
                EmailAddress = dto.EmailAddress?.Trim(),
                AppliedShareKitta = dto.AppliedShareKitta,
                ShareTypeId = dto.ShareTypeId,
                TotalShareAmount = dto.TotalShareAmount,
                TotalSharePaidAmount = dto.TotalSharePaidAmount,
                ReferenceId = dto.ReferenceId,
                AgentId = dto.AgentId,
                NomineeName = dto.NomineeName?.Trim(),
                SellerMemberId = dto.SellerMemberId,
                IsApproved = dto.IsApproved,
                RejectRemarks = dto.RejectRemarks?.Trim(),
                IsShareholder = dto.IsShareholder,
                ReferalCode = dto.ReferalCode?.Trim(),
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                ApprovedBy=dto.ApprovedBy,
                ApprovedDate=dto.ApprovedDate
            };
        }

        public static MemberPaymentLog ToEntity(this MemberPaymentLogDto dto)
        {
            if (dto == null)
                return null;

            return new MemberPaymentLog
            {
                PaymentId = dto.PaymentId,
                MemberId = dto.MemberId,
                Amount = dto.Amount,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                IsDeleted = dto.IsDeleted,
                DeletedDate = dto.DeletedDate,
                DeletedBy = dto.DeletedBy,
            };
        }
    }
}