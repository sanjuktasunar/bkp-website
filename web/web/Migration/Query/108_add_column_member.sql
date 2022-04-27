

GO
ALTER TABLE [dbo].[Member]
ADD IsActive bit null default(0)
GO

GO
ALTER TABLE [dbo].[Member]
ADD UserId int null Constraint Member_User_UserId References Users(UserId)
GO
GO
CREATE UNIQUE INDEX  Member_UserId_ui ON
[dbo].[Member](UserId) WHERE UserId IS NOT NULL
GO

GO
DROP VIEW [dbo].[MemberView]
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT A.*,B.FirstName AS RefernceFirstName,B.MiddleName AS ReferenceMiddleName,B.LastName AS ReferenceLastName,
B.ReferalCode AS ReferenceReferalCode
FROM [dbo].[Member] AS A
LEFT JOIN [dbo].[Member] AS B ON A.ReferenceId=B.MemberId
GO


GO
CREATE VIEW AddressView
AS
SELECT A.*,PP.ProvinceName AS PermanentProvinceName,PD.DistrictName AS PermanentDistrictName,
TP.ProvinceName as TemporaryProvinceName,TD.DistrictName AS TemporaryDistrictName,
PM.Name AS PermanentMunicipalityName,TM.Name AS TemporaryMunicipalityName,
PC.Name AS PermanentCountryName,TC.Name AS TemporaryCountryName
FROM Address AS A
LEFT JOIN [dbo].[Province] AS PP ON PP.ProvinceId=A.PermanentProvinceId
LEFT JOIN [dbo].[District] AS PD ON PD.DistrictId=A.PermanentDistrictId
LEFT JOIN [dbo].[Province] AS TP ON TP.ProvinceId=A.TemporaryProvinceId
LEFT JOIN [dbo].[District] AS TD ON TD.DistrictId=A.TemporaryDistrictId
LEFT JOIN [dbo].[MunicipalityType] AS PM ON PM.Id=A.PermanentMunicipalityTypeId
LEFT JOIN [dbo].[MunicipalityType] AS TM ON TM.Id=A.TemporaryMunicipalityTypeId
LEFT JOIN [dbo].[Country] AS PC ON PC.Id=A.PermanentCountryId
LEFT JOIN [dbo].[Country] AS TC ON TC.Id=A.TemporaryCountryId
GO


GO
DROP VIEW MemberView
GO


GO
CREATE VIEW MemberView
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
G.GenderName
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
GO


GO
ALTER TABLE MenuAccessPermission
ADD RoleId int null Constraint MenuAccessPermission_Role_RoleId_fk References Role(RoleId)
GO


GO
ALTER TABLE Users
ADD RoleId int null Constraint Users_Role_RoleId_fk References Role(RoleId)
GO

GO
UPDATE Role SET RoleName=N'Super Admin' WHERE RoleId=1
GO

GO
SET IDENTITY_INSERT Role ON
GO

GO
INSERT INTO Role(RoleId,RoleName,Status)
SELECT 2,N'Admin',1 UNION ALL
SELECT 3,N'Member',1
GO

GO
SET IDENTITY_INSERT Role OFF
GO

GO
UPDATE Users SET RoleId=1 WHERE UserId=1
GO

GO
ALTER TABLE MenuAccessPermission
DROP CONSTRAINT MenuAccessPermission_Staffs_StaffId_fk
GO

GO
DROP INDEX MenuAccessPermission_MenuId_StaffId ON MenuAccessPermission;
GO

GO
ALTER TABLE MenuAccessPermission
DROP COLUMN StaffId
GO


GO
DELETE FROM MenuAccessPermission
GO


GO
DELETE FROM Menus
GO

GO
SET IDENTITY_INSERT [Menus] ON;
GO
GO
INSERT INTO Menus(MenuId,ParentMenuId,MenuNameEnglish,MenuNameNepali,CheckMenuName,MenuLink,MenuOrder,MenuIcon)
SELECT 1,NULL,N'Administration',N'प्रशासन ',N'Administration',N'#',1,'fas fa-fw fa-cog' UNION ALL
SELECT 2,1,N'Menus',N'मेनु ',N'Menus',N'/MenuList',1,'' UNION ALL
SELECT 3,1,N'Role',N'Role',N'Role',N'/RoleList',4,'' UNION ALL
SELECT 4,1,N'Staffs Info',N'कर्मचारी',N'Staffs',N'/StaffList',7,'' UNION ALL
SELECT 5,1,N'Organization Info',N'Organization Info ',N'OrganizationInfo',N'/OrganizationInfo',2,'' UNION ALL
SELECT 6,1,N'FiscalYear',N'Fiscal Year ',N'FiscalYear','/FiscalYearList',3,'' UNION ALL
SELECT 7,1,N'Designation',N'Designation',N'Designation',N'/DesignationList',5,'' UNION ALL
SELECT 8,1,N'Department',N'Department',N'Department',N'/DepartmentList',6,'' UNION ALL
SELECT 9,1,N'Product',N'Product',N'Product',N'/ProductList',8,'' UNION ALL
SELECT 10,NULL,N'Accounts',N'Accounts ',N'Accounts',N'#',2,'fas fa-fw fa-book' UNION ALL
SELECT 11,10,N'AccountHead',N'AccountHead',N'AccountHead',N'/AccountHeadList',1,'' UNION ALL
SELECT 12,10,N'Unit',N'Unit',N'Unit',N'/UnitList',2,'' UNION ALL
SELECT 13,10,N'Supplier',N'Supplier',N'Supplier',N'/SupplierList',3,'' UNION ALL
SELECT 14,10,N'PurchaseRecord',N'PurchaseRecord',N'PurchaseRecord',N'/PurchaseRecordList',4,'' UNION ALL
SELECT 15,NULL,N'Customer',N'Customer ',N'Customer',N'#',3,'fas fa-fw fa-book' UNION ALL
SELECT 16,15,N'CustomerQuery',N'CustomerQuery',N'CustomerQuery',N'/QueryDetails',1,'' UNION ALL
SELECT 17,NULL,N'Member',N'Member ',N'ParentMember',N'#',4,'fas fa-fw fa-user' UNION ALL
SELECT 18,17,N'Member',N'Member',N'Member',N'/MemberList',1,'' 
GO
GO
SET IDENTITY_INSERT [Menus] OFF;
GO


GO
CREATE UNIQUE INDEX MenuAccessPermission_MenuId_RoleId ON 
MenuAccessPermission(MenuId,RoleId) WHERE RoleId IS NOT NULL
GO


GO
INSERT INTO MenuAccessPermission(MenuId,RoleId,ReadAccess,WriteAccess,ModifyAccess,DeleteAccess,AdminAccess)
SELECT 2,1,1,1,1,1,1 UNION ALL
SELECT 3,1,1,1,1,1,1
GO

GO
DROP VIEW MenuAccessPermissionView
GO

GO
CREATE VIEW MenuAccessPermissionView
AS
SELECT A.*,B.MenuNameEnglish,B.MenuNameNepali,B.CheckMenuName,B.MenuIcon,
B.MenuLink,B.MenuOrder,B.ParentMenuId,B.ParentMenuNameEnglish,B.ParentMenuNameNepali,
R.RoleName
FROM MenuView AS B 
LEFT JOIN  MenuAccessPermission AS A ON A.MenuId=B.MenuId
LEFT JOIN Role AS R ON R.RoleId=A.RoleId
GO


GO
CREATE OR ALTER PROC [dbo].[MenuAccessPermissionForRole]
(
	@RoleId int
)
AS
BEGIN
	SELECT A.*,
	B.ReadAccess,B.WriteAccess,B.AdminAccess,B.ModifyAccess,B.DeleteAccess,
	B.MenuAccessPermissionId
	FROM MenuView AS A
	LEFT JOIN MenuAccessPermissionView AS B 
	ON B.MenuId=A.MenuId AND B.RoleId=@RoleId
	WHERE A.ParentMenuId IS NOT NULL
END
GO

GO
ALTER TABLE Users
ALTER COLUMN PhotoStorageId int null
GO


GO
DROP INDEX Users_ContactNumber_ui ON Users
GO

GO
DROP INDEX Users_EmailAddress_ui ON Users
GO
