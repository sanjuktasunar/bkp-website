

--GO
--ALTER TABLE [Member]
--ADD AppliedKitta int null default(0)
--GO

--GO
--UPDATE Member SET AppliedKitta=2250
--WHERE UserId IS NOT NULL AND IsActive=1
--GO

--GO
--UPDATE Member SET AppliedKitta=33750
--WHERE MemberId=1
--GO

GO
DROP VIEW [dbo].[MemberView]
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT B.*,A.PermanentIsOutsideNepal,A.PermanentCountryId,A.PermanentProvinceId,
A.PermanentDistrictId,A.PermanentMunicipalityTypeId,A.PermanentMunicipality,PermanentWardNumber,A.PermanentToleName,
A.PermanentAddress,A.TemporaryIsOutsideNepal,A.TemporaryCountryId,A.TemporaryProvinceId,A.TemporaryDistrictId,A.TemporaryMunicipalityTypeId,
A.TemporaryMunicipality,A.TemporaryAddress,A.TemporaryWardNumber,A.TemporaryToleName,
PP.ProvinceName AS PermanentProvinceName,PD.DistrictName AS PermanentDistrictName,
TP.ProvinceName as TemporaryProvinceName,TD.DistrictName AS TemporaryDistrictName,
PM.Name AS PermanentMunicipalityTypeName,TM.Name AS TemporaryMunicipalityTypeName,
PC.Name AS PermanentCountryName,TC.Name AS TemporaryCountryName,

B1.FirstName AS RefernceFirstName,B1.MiddleName AS ReferenceMiddleName,B1.LastName AS ReferenceLastName,
B1.ReferalCode AS ReferenceReferalCode,
O.Name AS OcuupationName,MF.Name AS MemberFieldName,UD.Photo,UD.CitizenshipFront,UD.CitizenshipBack,
BD.Amount,BD.VoucherImage,
G.GenderName,
H.ShareTypeName
FROM
Member AS B 
LEFT JOIN Address AS A ON A.MemberId=B.MemberId
LEFT JOIN Province AS PP ON PP.ProvinceId=A.PermanentProvinceId
LEFT JOIN District AS PD ON PD.DistrictId=A.PermanentDistrictId
LEFT JOIN Province AS TP ON TP.ProvinceId=A.TemporaryProvinceId
LEFT JOIN District AS TD ON TD.DistrictId=A.TemporaryDistrictId
LEFT JOIN MunicipalityType AS PM ON PM.Id=A.PermanentMunicipalityTypeId
LEFT JOIN MunicipalityType AS TM ON TM.Id=A.TemporaryMunicipalityTypeId
LEFT JOIN Country AS PC ON PC.Id=A.PermanentCountryId
LEFT JOIN Country AS TC ON TC.Id=A.TemporaryCountryId
LEFT JOIN Member AS B1 ON B.ReferenceId=B1.MemberId
LEFT JOIN Occupation AS O ON O.Id=B.OccupationId
LEFT JOIN MemberField AS MF ON MF.Id=B.MemberFieldId
LEFT JOIN UserDocuments AS UD ON UD.MemberId=B.MemberId
LEFT JOIN BankDeposit AS BD ON BD.MemberId=B.MemberId AND BD.IsVoucherDeposit=1 
LEFT JOIN Gender AS G ON G.GenderId=B.GenderId
LEFT JOIN [dbo].[ShareTypes] AS H ON H.ShareTypeId=B.ShareTypeId
GO

GO
DROP VIEW [dbo].[AddressView]
GO


GO
CREATE VIEW [dbo].[AddressView]
AS
SELECT  A.Id, A.MemberId, A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, 
                         A.TemporaryWardNumber, A.TemporaryToleName, A.TemporaryAddress, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName, PM.Name AS PermanentMunicipalityName, TM.Name AS TemporaryMunicipalityName, PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName
						 ,M.FirstName,M.MiddleName,M.LastName,M.GenderName,M.ReferalCode,
						 M.MobileNumber,M.Email,M.GenderId,M.MemberCode,M.IsActive,M.UserId,
						 M.ApprovalStatus,M.CreatedDate,M.ApprovedDate
						 

FROM    dbo.Address AS A LEFT OUTER JOIN
        dbo.Province AS PP ON PP.ProvinceId = A.PermanentProvinceId LEFT OUTER JOIN
        dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT OUTER JOIN
        dbo.Province AS TP ON TP.ProvinceId = A.TemporaryProvinceId LEFT OUTER JOIN
        dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT OUTER JOIN
        dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT OUTER JOIN
        dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT OUTER JOIN
        dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT OUTER JOIN
        dbo.Country AS TC ON TC.Id = A.TemporaryCountryId INNER JOIN
		dbo.MemberView AS M ON M.MemberId=A.MemberId
GO

GO
CREATE OR ALTER PROC [dbo].[Sp_MemberReportAddressWise]
(
	@ProvinceId int,
	@DistrictId int,
	@GenderId int,
	@CountryId int,
	@OutsideCountry int
)
AS
BEGIN
	SELECT * 
	FROM [dbo].[AddressView] AS A
	WHERE
	((ISNULL(@ProvinceId,'')='') OR A.TemporaryProvinceId=@ProvinceId)
	AND
	((ISNULL(@DistrictId,'')='') OR A.TemporaryDistrictId=@DistrictId)
	AND
	((ISNULL(@GenderId,'')='') OR A.GenderId=@GenderId)
	AND
	((ISNULL(@CountryId,'')='') OR A.TemporaryCountryId=@CountryId)
	AND 
	UserId IS NOT NULL
	AND 
	ReferalCode IS NOT NULL
	
END
GO
