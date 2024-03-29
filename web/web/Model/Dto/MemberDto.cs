﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Entity.Model;

namespace Web.Entity.Dto
{
    public class MemberDto: BaseDtoData
    {
        public int MemberId { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DateOfBirthBS { get; set; }
        public string DateOfBirthAD { get; set; }
        public int? GenderId { get; set; }
        public int? OccupationId { get; set; }
        public string OtherOccupationRemarks { get; set; }
        public int? MemberFieldId { get; set; }
        public string CitizenshipNumber { get; set; }
        public bool? IsMemberFilled { get; set; }
        public FormStatus FormStatus { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? ReferenceId { get; set; }
        public string ReferalCode { get; set; }
        public string RefernceFirstName { get; set; }
        public string ReferenceMiddleName { get; set; }
        public string ReferenceLastName { get; set; }
        public string ReferenceReferalCode { get; set; }

        public string PermanentFullAddress { get; set; }
        public string TemporaryFullAddress { get; set; }

        public string PermanentProvinceName { get; set; }
        public string PermanentDistrictName { get; set; }
        public string PermanentMunicipalityName { get; set; }
        public string PermanentCountryName { get; set; }

        public string TemporaryProvinceName { get; set; }
        public string TemporaryDistrictName { get; set; }
        public string TemporaryMunicipalityName { get; set; }
        public string TemporaryCountryName { get; set; }

        public bool TemporaryIsOutsideNepal { get; set; }

        public int? TemporaryProvinceId { get; set; }
        public int? TemporaryDistrictId { get; set; }
        public int? TemporaryMunicipalityTypeId { get; set; }
        public string TemporaryMunicipalityTypeName { get; set; }
        public string TemporaryMunicipality { get; set; }
        public string TemporaryWardNumber { get; set; }
        public string TemporaryToleName { get; set; }

        public int? TemporaryCountryId { get; set; }
        public string TemporaryAddress { get; set; }

        public bool PermanentIsOutsideNepal { get; set; }

        public int? PermanentProvinceId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public int? PermanentMunicipalityTypeId { get; set; }
        public string PermanentMunicipalityTypeName { get; set; }
        public string PermanentMunicipality { get; set; }
        public string PermanentWardNumber { get; set; }
        public string PermanentToleName { get; set; }

        public int? PermanentCountryId { get; set; }
        public string PermanentAddress { get; set; }
        public string OcuupationName { get; set; }
        public string MemberFieldName { get; set; }

        public bool? IsActive { get; set; }

        public string Photo { get; set; }
        public string CitizenshipFront { get; set; }
        public string CitizenshipBack { get; set; }
        public string CitizenshipFrontImageString { get; set; }
        public string CitizenshipBackImageString { get; set; }

        public decimal Amount { get; set; }
        public string VoucherImage { get; set; }
        public string GenderName { get; set; }

        public int? UserId { get; set; }

        public int Id { get; set; }
        public int? ShareTypeId { get; set; }
        public string ShareTypeName { get; set; }
        public int AppliedKitta { get; set; }
        public bool? IsFillup { get; set; }

        public string ApprovalRemarks { get; set; }

        public int UserDocumentId { get; set; }
        public string MemberPhoto { get; set; }
        public string MemberPhotoString { get; set; }
        public string VoucherImageString { get; set; }
        public string ReferenceFullName { get; set; }

        public int? MaritalStatusId { get; set; }

        public int? FormerDistrictId { get; set; }
        public string FormerDistrictName { get; set; }
        public string FormerMunicipalityName { get; set; }
        public string FormerWardNumber { get; set; }

        public int? AgentId { get; set; }
        public int? ShareholderId { get; set; }
        public int TotalKitta { get; set; }
        public int ShareholderIsActive { get; set; }
        public DateTime? ShareholderDate { get; set; }
    }

    public class MemberPersonalInfoDto
    {
        public int MemberId { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="FirstName must be less than 20")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "MiddleName must be less than 20")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "LastName must be less than 20")]
        public string LastName { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "DateOfBirth must be less than 10")]
        public string DateOfBirthBS { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "CitizenshipNumber must be less than 20")]
        public string CitizenshipNumber { get; set; }
    }

    public class MemberContactInfoDto
    {
        public int MemberId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "MobileNumber must be less than 20")]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Email must be less than 150")]
        public string Email { get; set; }
    }

    public class MemberAddressDto
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public bool PermanentIsOutsideNepal { get; set; }

        public int? PermanentProvinceId { get; set; }

        public int? PermanentDistrictId { get; set; }

        public int? PermanentMunicipalityTypeId { get; set; }

        [StringLength(200,ErrorMessage ="Municipality must be less than 200")]
        public string PermanentMunicipality { get; set; }

        [StringLength(4, ErrorMessage = "Ward Number must be less than 4")]
        public string PermanentWardNumber { get; set; }

        [StringLength(200, ErrorMessage = "Tole name must be less than 200")]
        public string PermanentToleName { get; set; }

        public int? PermanentCountryId { get; set; }

        [StringLength(200, ErrorMessage = "Address must be less than 200")]
        public string PermanentAddress { get; set; }

        [Required]
        public bool TemporaryIsOutsideNepal { get; set; }

        public int? TemporaryProvinceId { get; set; }
        public int? TemporaryDistrictId { get; set; }
        public int? TemporaryMunicipalityTypeId { get; set; }

        [StringLength(200, ErrorMessage = "Municipality must be less than 200")]
        public string TemporaryMunicipality { get; set; }

        [StringLength(200, ErrorMessage = "Ward number must be less than 200")]
        public string TemporaryWardNumber { get; set; }

        [StringLength(200, ErrorMessage = "Tole name must be less than 200")]
        public string TemporaryToleName { get; set; }

        public int? TemporaryCountryId { get; set; }

        [StringLength(200, ErrorMessage = "Address must be less than 200")]
        public string TemporaryAddress { get; set; }
    }

    public class MemberOccupationDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int OccupationId { get; set; }

        [StringLength(150,ErrorMessage ="Occupation Remarks must be less than 150")]
        public string OtherOccupationRemarks { get; set; }

        [Required]
        public int MemberFieldId { get; set; }
    }

    public class MemberDocumentsDto
    {
        public int UserDocumentId { get; set; }

        [Required]
        public int MemberId { get; set; }

        public string MemberPhoto { get; set; }

        public string MemberPhotoString { get; set; }

        public string CitizenshipFront { get; set; }

        public string CitizenshipFrontImageString { get; set; }

        public string CitizenshipBack { get; set; }

        public string CitizenshipBackImageString { get; set; }
    }

    public class MemberBankDepositDto
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }


        public string ReferalCode { get; set; }

        //[Required]
        public string VoucherImage { get; set; }

        public string VoucherImageString { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }

    public class SearchMemberDto
    {
        public MemberDto Member { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsNotFoundOrReject { get; set; }
        public IEnumerable<KeyValuePairDto> KeyValuePairDto { get; set; }
    }
}
