
GO
ALTER TABLE [dbo].[Member]
ADD ShareTypeId int null Constraint Member_ShareType_ShareTypeId References ShareTypes(ShareTypeId)
GO

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
UPDATE [dbo].[Member]
SET ShareTypeId=1 
WHERE UserId IS NOT NULL
GO
