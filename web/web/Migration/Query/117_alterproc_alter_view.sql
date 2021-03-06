
GO
DROP VIEW [dbo].[AddressView]
GO

GO
CREATE VIEW [dbo].[AddressView]
AS
SELECT        A.Id, A.MemberId, A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, 
                         A.TemporaryWardNumber, A.TemporaryToleName, A.TemporaryAddress, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName, PM.Name AS PermanentMunicipalityName, TM.Name AS TemporaryMunicipalityName, PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName, M.FirstName, 
                         M.MiddleName, M.LastName, G.GenderName, M.ReferalCode, M.MobileNumber, M.Email, M.GenderId, M.MemberCode, M.IsActive, M.UserId, M.ApprovalStatus, M.CreatedDate, M.ApprovedDate,M.ReferenceId
FROM        dbo.Address AS A LEFT JOIN
            dbo.Province AS PP ON PP.ProvinceId = A.PermanentProvinceId LEFT JOIN
            dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT JOIN
            dbo.Province AS TP ON TP.ProvinceId = A.TemporaryProvinceId LEFT JOIN
            dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT JOIN
            dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT JOIN
            dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT JOIN
            dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT JOIN
            dbo.Country AS TC ON TC.Id = A.TemporaryCountryId LEFT JOIN
            dbo.Member AS M ON M.MemberId = A.MemberId LEFT JOIN
			dbo.Gender AS G ON G.GenderId=M.GenderId
GO

GO
DROP PROC [dbo].[Sp_MemberReportAddressWise]
GO

GO
CREATE or alter PROC [dbo].[Sp_MemberReportAddressWise]
(
	@ProvinceId int,
	@DistrictId int,
	@GenderId int,
	@CountryId int,
	@OutsideCountry int,
	@IsAdmin bit,
	@UserId int
)
AS
BEGIN
	
	declare @ReferenceMemberId int;
	SET @ReferenceMemberId=0;
	IF(@UserId>0 AND @IsAdmin=0)
	begin
	set @ReferenceMemberId=(Select top 1 MemberId from Member where UserId=@UserId AND IsActive=1
	AND ReferalCode IS NOT NULL)
	end

	SELECT * 
	FROM [dbo].[AddressView] AS A
	WHERE
	((ISNULL(@ProvinceId,'')='') OR A.TemporaryProvinceId=@ProvinceId)
	AND
	((ISNULL(@DistrictId,'')='') OR A.TemporaryDistrictId=@DistrictId)
	AND
	((ISNULL(@GenderId,'')='') OR A.GenderId=@GenderId)
	AND
	(@IsAdmin=1 OR
	 (@IsAdmin=0 AND (A.UserId=@UserId OR A.ReferenceId=@ReferenceMemberId ))
	)
	AND
	(
		@OutsideCountry=2
		OR
		
		(@OutsideCountry=1 AND A.TemporaryIsOutsideNepal=1 AND (A.TemporaryCountryId=@CountryId OR (ISNULL(@CountryId,'')='') ))
		OR
		(@OutsideCountry=0 AND A.TemporaryIsOutsideNepal=0)
	)
	AND 
	A.UserId IS NOT NULL
	AND 
	ReferalCode IS NOT NULL
	
END
GO


--exec [dbo].[Sp_MemberReportAddressWise]
--@ProvinceId=null,
--@DistrictId=null,
--@GenderId =null,
--@CountryId =null,
--@OutsideCountry =0,
--@IsAdmin =1,
--@UserId =0
