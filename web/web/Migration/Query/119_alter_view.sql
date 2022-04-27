

GO
DROP VIEW [dbo].[MemberView]
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT        B.MemberId, B.MemberCode, B.FirstName, B.MiddleName, B.LastName, B.MobileNumber, B.Email, B.DateOfBirthBS, B.DateOfBirthAD, B.GenderId, B.OccupationId, B.OtherOccupationRemarks, B.MemberFieldId, 
                         B.CitizenshipNumber, B.IsMemberFilled, B.FormStatus, B.CreatedDate, B.CreatedBy, B.UpdatedDate, B.UpdatedBy, B.ApprovalStatus, B.ApprovedDate, B.ApprovedBy, B.ReferalCode, B.ReferenceId, B.IsActive, B.UserId, 
                         B.ShareTypeId, B.ApprovalRemarks, A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, A.TemporaryAddress, 
                         A.TemporaryWardNumber, A.TemporaryToleName, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName, PM.Name AS PermanentMunicipalityTypeName, TM.Name AS TemporaryMunicipalityTypeName, PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName, 
                         B1.FirstName AS RefernceFirstName, B1.MiddleName AS ReferenceMiddleName, B1.LastName AS ReferenceLastName, B1.ReferalCode AS ReferenceReferalCode, O.Name AS OcuupationName, 
                         MF.Name AS MemberFieldName, UD.Photo, UD.CitizenshipFront, UD.CitizenshipBack, BD.Amount, BD.VoucherImage, G.GenderName, H.ShareTypeName,PF.AppliedKitta
FROM            dbo.Member AS B LEFT OUTER JOIN
                         dbo.Address AS A ON A.MemberId = B.MemberId LEFT OUTER JOIN
                         dbo.Province AS PP ON PP.ProvinceId = A.PermanentProvinceId LEFT OUTER JOIN
                         dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT OUTER JOIN
                         dbo.Province AS TP ON TP.ProvinceId = A.TemporaryProvinceId LEFT OUTER JOIN
                         dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT OUTER JOIN
                         dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT OUTER JOIN
                         dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT OUTER JOIN
                         dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT OUTER JOIN
                         dbo.Country AS TC ON TC.Id = A.TemporaryCountryId LEFT OUTER JOIN
                         dbo.Member AS B1 ON B.ReferenceId = B1.MemberId LEFT OUTER JOIN
                         dbo.Occupation AS O ON O.Id = B.OccupationId LEFT OUTER JOIN
                         dbo.MemberField AS MF ON MF.Id = B.MemberFieldId LEFT OUTER JOIN
                         dbo.UserDocuments AS UD ON UD.MemberId = B.MemberId LEFT OUTER JOIN
                         dbo.BankDeposit AS BD ON BD.MemberId = B.MemberId AND BD.IsVoucherDeposit = 1 LEFT OUTER JOIN
                         dbo.Gender AS G ON G.GenderId = B.GenderId LEFT OUTER JOIN
                         dbo.ShareTypes AS H ON H.ShareTypeId = B.ShareTypeId LEFT OUTER JOIN
						 dbo.PratigyaPatraFormFillups AS PF ON PF.MemberId=B.MemberId
GO

GO
CREATE OR ALTER PROC [dbo].[Sp_GetReferenceMembers]
AS
BEGIN
create table #temp
(
	Id int Identity(1,1),
	ReferenceID int not null
)
INSERT INTO #temp(ReferenceID)
SELECT DISTINCT ReferenceId FROM dbo.Member WHERE ReferenceId IS NOT NULL

CREATE TABLE #refMem
(
	ID int Identity(1,1),
	MemberId int,
	ReferenceName nvarchar(400) 
)
insert into #refMem
(
	MemberId,ReferenceName
)
SELECT b.MemberId,TRIM(b.FirstName)+case when b.MiddleName is null then '' else ' '+TRIM(b.MiddleName) end+' '+TRIM(b.LastName)+'('+b.MobileNumber+')'
	as ReferenceName
FROM Member as b
join #temp as a on a.ReferenceID=b.MemberId

select MemberId AS Id,ReferenceName AS [Value] from #refMem
END
GO
