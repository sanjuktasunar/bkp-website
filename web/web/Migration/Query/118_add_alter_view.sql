
GO
CREATE OR ALTER VIEW [dbo].[MemberDocumentView]
AS
SELECT b.CitizenshipFront,b.CitizenshipBack,b.Photo,a.MemberId,a.UserId,
a.FirstName +case when a.MiddleName is not null then +' ' + a.MiddleName else '' end + ' ' +a.LastName as FullName,
a.MobileNumber,a.ReferalCode,a.CitizenshipNumber,
c.AppliedKitta,c.IsFillup
from [dbo].[Member] as a
left join [dbo].[UserDocuments] as b on b.MemberId=a.MemberId
left join PratigyaPatraFormFillups as c on c.MemberId=a.MemberId
GO

GO
ALTER TABLE [dbo].[MenuAccessPermission]
ADD ApprovalAccess bit null default(0)
GO


GO
ALTER TABLE [dbo].[MenuAccessPermission]
ADD RejectAccess bit null default(0)
GO


GO
DROP VIEW [dbo].[MenuAccessPermissionView]
GO

GO
CREATE VIEW [dbo].[MenuAccessPermissionView]
AS
SELECT        A.MenuAccessPermissionId, A.MenuId, A.ReadAccess, A.WriteAccess, A.ModifyAccess, A.DeleteAccess,
			  A.ApprovalAccess,A.RejectAccess,
			  A.AdminAccess, A.RoleId, B.MenuNameEnglish, B.MenuNameNepali, B.CheckMenuName, B.MenuIcon, B.MenuLink, 
                         B.MenuOrder, B.ParentMenuId, B.ParentMenuNameEnglish, B.ParentMenuNameNepali, R.RoleName
FROM            dbo.MenuView AS B LEFT OUTER JOIN
                         dbo.MenuAccessPermission AS A ON A.MenuId = B.MenuId LEFT OUTER JOIN
                         dbo.Role AS R ON R.RoleId = A.RoleId
GO			

GO

GO
DROP PROC [dbo].[MenuAccessPermissionForRole]
GO

GO
CREATE PROC [dbo].[MenuAccessPermissionForRole]
(
	@RoleId int
)
AS
BEGIN
	SELECT A.*,
	B.ReadAccess,B.WriteAccess,B.AdminAccess,B.ModifyAccess,B.DeleteAccess,
	B.ApprovalAccess,B.RejectAccess,
	B.MenuAccessPermissionId
	FROM MenuView AS A
	LEFT JOIN MenuAccessPermissionView AS B 
	ON B.MenuId=A.MenuId AND B.RoleId=@RoleId
	WHERE A.ParentMenuId IS NOT NULL
END
GO

GO
SET IDENTITY_INSERT [dbo].[UserStatus] ON;
GO

GO
INSERT INTO [dbo].[UserStatus](StatusId,StatusName,UserTypeId)
VALUES(4,N'Rejected',NULL)
GO

GO
SET IDENTITY_INSERT [dbo].[UserStatus] OFF;
GO

GO
ALTER TABLE [dbo].[Member]
ADD ApprovalRemarks nvarchar(max) NULL DEFAULT('')
GO

GO
DROP VIEW [dbo].[MemberView]
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT        B.MemberId, B.MemberCode, B.FirstName, B.MiddleName, B.LastName, B.MobileNumber, B.Email, B.DateOfBirthBS, B.DateOfBirthAD, B.GenderId, B.OccupationId, B.OtherOccupationRemarks, B.MemberFieldId, 
                         B.CitizenshipNumber, B.IsMemberFilled, B.FormStatus, B.CreatedDate, B.CreatedBy, B.UpdatedDate, B.UpdatedBy, B.ApprovalStatus, B.ApprovedDate, B.ApprovedBy, B.ReferalCode, B.ReferenceId, B.IsActive, B.UserId, 
                         B.ShareTypeId,B.ApprovalRemarks,
						 A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, A.PermanentToleName, 
                         A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, A.TemporaryAddress, 
                         A.TemporaryWardNumber, A.TemporaryToleName, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName, PM.Name AS PermanentMunicipalityTypeName, TM.Name AS TemporaryMunicipalityTypeName, PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName, 
                         B1.FirstName AS RefernceFirstName, B1.MiddleName AS ReferenceMiddleName, B1.LastName AS ReferenceLastName, B1.ReferalCode AS ReferenceReferalCode, O.Name AS OcuupationName, 
                         MF.Name AS MemberFieldName, UD.Photo, UD.CitizenshipFront, UD.CitizenshipBack, BD.Amount, BD.VoucherImage, G.GenderName, H.ShareTypeName
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
                         dbo.ShareTypes AS H ON H.ShareTypeId = B.ShareTypeId
GO
