
GO
UPDATE dbo.[Users]
SET UserName=N'XX96H12akoL2okcC7Ur1ow==',[Password]=N'OHzTHRAngCB2vlRNc5g9Ig=='
WHERE UserId=1
GO

GO
ALTER TABLE [dbo].[Menus]
DROP COLUMN MenuNameNepali
GO

GO
DROP INDEX Menus_MenuLink_ui ON [dbo].[Menus]
GO

GO
ALTER TABLE [dbo].[Menus]
DROP COLUMN MenuLink
GO

GO
ALTER TABLE [dbo].[Menus]
DROP COLUMN MenuOrder
GO

GO
ALTER TABLE [dbo].[Menus]
DROP COLUMN MenuIcon
GO

GO
ALTER TABLE [dbo].[Menus]
DROP CONSTRAINT Menus_fk
GO

GO
DROP INDEX Menus_MenuNameEnglish ON [dbo].[Menus]
GO

GO
ALTER TABLE [dbo].[Menus]
DROP COLUMN ParentMenuId
GO

GO
DROP VIEW [dbo].[MenuView]
GO

GO
DROP VIEW [dbo].[MenuAccessPermissionView]
GO

GO
CREATE VIEW [dbo].[MenuAccessPermissionView]
AS
SELECT        A.MenuAccessPermissionId, A.ReadAccess, A.WriteAccess, A.ModifyAccess, A.DeleteAccess,
			  A.ApprovalAccess,A.RejectAccess,
			  A.AdminAccess, A.RoleId, 
			  B.MenuId,B.MenuNameEnglish, B.CheckMenuName,
                         R.RoleName
FROM            dbo.Menus AS B LEFT OUTER JOIN
                         dbo.MenuAccessPermission AS A ON A.MenuId = B.MenuId LEFT OUTER JOIN
                         dbo.[Role] AS R ON R.RoleId = A.RoleId
GO

GO
ALTER TABLE [dbo].[Users]
ADD IsSuperAdmin bit null default(0)
GO

GO
UPDATE dbo.[Users]
SET IsSuperAdmin=1 WHERE UserId=1
GO

GO
DROP VIEW [dbo].[StaffsView]
GO

GO
CREATE VIEW [dbo].[StaffsView]
AS
SELECT A.StaffId,A.UserId,A.RoleId,A.DesignationId,A.DepartmentId,A.StaffName,
A.GenderId,A.TemporaryAddress,A.PermanentAddress,A.CitizenshipNumber,A.PanNumber,
A.BasicSalary,
B.UserName,B.UserTypeId,B.PhotoStorageId,B.ContactNumber,
B.EmailAddress,B.UserStatusId,
US.StatusName,
C.Photo,C.PhotoLocation,
R.RoleName,R.Status AS RoleStatus,D.DesignationName,D.Status AS DesignationStatus,
DP.DepartmentName,DP.Status AS DepartmentStatus,
G.GenderName,G.Status AS GenderStatus
FROM Staffs AS A
JOIN Users AS B ON B.UserId=A.UserId
JOIN UserStatus AS US ON US.StatusId=B.UserStatusId
JOIN PhotoStorages AS C ON C.PhotoStorageId=B.PhotoStorageId
LEFT JOIN Role AS R ON R.RoleId=A.RoleId
LEFT JOIN Designation AS D ON D.DesignationId=A.DesignationId
LEFT JOIN Department AS DP ON DP.DepartmentId=A.DepartmentId
LEFT JOIN Gender AS G ON G.GenderId=A.GenderId
GO

GO
CREATE PROC [dbo].[MenuAccessFor_LoginUser]
(
	@UserId int,
	@CheckMenuName nvarchar(100)
)
AS
BEGIN

-- exec [dbo].[MenuAccessFor_LoginUser] 1,'test'
	declare @IsSuperAdmin bit,@RoleName nvarchar(100)
	select @IsSuperAdmin=u.IsSuperAdmin,@RoleName=r.RoleName
					from dbo.[Users] u
					left join dbo.[Role] r on r.RoleId=u.RoleId
					where u.UserId=@UserId

	if(@IsSuperAdmin=1)
	begin
		select 1 as AdminAccess
		return;
	end
	else
	begin
		SELECT map.*,@RoleName as RoleName
		FROM
			dbo.MenuAccessPermission map
		INNER JOIN 
			dbo.Menus m on m.MenuId=map.MenuId
		LEFT JOIN
			dbo.[Role] r on r.RoleId=map.RoleId
		LEFT JOIN
			dbo.[Users] u on u.RoleId=r.RoleId

		WHERE
			u.UserId=@UserId
			and
			u.UserStatusId=1
			and
			m.[Status]=1
			and
			r.[Status]=1
			and
			TRIM(m.CheckMenuName)=TRIM(@CheckMenuName)
	end
	
END
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
	SELECT
		A.*,
		B.ReadAccess,B.WriteAccess,B.AdminAccess,B.ModifyAccess,B.DeleteAccess,
		B.ApprovalAccess,B.RejectAccess,
		B.MenuAccessPermissionId
	FROM 
		Menus AS A
	LEFT JOIN
		MenuAccessPermission AS B 
		ON B.MenuId=A.MenuId AND B.RoleId=@RoleId

END
GO

GO
CREATE PROC [dbo].[Get_CurrentUser]
(
	@UserId int
)
AS
BEGIN
	select 
		u.*,
		r.RoleName
	from 
		dbo.[Users] u
	inner join 
		dbo.[Role] r on r.RoleId=r.RoleId
	where 
	u.UserId=@UserId 
	and 
	u.UserStatusId=1
	and 
	r.[Status]=1
END
GO

GO
CREATE PROC [dbo].[Get_LoginUser]
(
	@UserName nvarchar(200),
	@Password nvarchar(200)
)
AS
BEGIN
	select 
		u.*,
		r.RoleName
	from 
		dbo.[Users] u
	inner join 
		dbo.[Role] r on r.RoleId=r.RoleId
	where 
		u.UserName=@UserName
		and
		u.[Password]=@Password
		and 
		r.[Status]=1
END
GO



--GO
--CREATE TABLE [dbo].[MemberList]
--(
--	MemberListId int not null Identity(1,1) Constraint MemberList_pk Primary Key,
--	MemberTypeId int not null,
--	FullName nvarchar(200) null,
--	MobileNumber nvarchar(20) null,
--	MobileNumber1 nvarchar(20) null,
--	EmailAddress nvarchar(200) null,
--	DateOfBirthBS nvarchar(10) null,
--	DateOfBirthAD nvarchar(10) null,
--	GenderId int null Constraint MemberList_GenderId_fk References dbo.[Gender](GenderId),
--	CitizenShipNumber nvarchar(200) null,
--	ShareTypeId int null Constraint MemberList_ShareTypeId_fk References dbo.[Sharetypes](ShareTypeId),
--	--PermanentAddress nvarchar(500) null,
--	--TemporaryAddress nvarchar(500) null,
--	OccupationId int null Constraint MemberList_OccupationId_fk References dbo.[Occupation](Id),
--	OccupationRemarks nvarchar(100) null
--	--PanNumber nvarchar(100) null
--);
--GO


GO
ALTER TABLE [dbo].[Member]
ALTER COLUMN FirstName nvarchar(50) null
GO

GO
ALTER TABLE [dbo].[Member]
ALTER COLUMN LastName nvarchar(50) null
GO

GO
ALTER TABLE [dbo].[Member]
ALTER COLUMN GenderId int null
GO

GO
ALTER TABLE [dbo].[Member]
ALTER COLUMN MemberCode nvarchar(100) null
GO

GO
ALTER TABLE [dbo].[Member]
ALTER COLUMN DateOfBirthAD nvarchar(10) null
GO

GO
DROP INDEX Member_MemberCode_ui ON [dbo].[Member]
GO

GO
CREATE UNIQUE INDEX Member_MemberCode_ui ON
dbo.[Member](MemberCode) where MemberCode is not null and MemberCode <>''
GO

--GO
--CREATE TABLE dbo.[Agents]
--(
--	AgentId int not null Identity(1,1) Constraint Agents_pk Primary Key,
--	LicenceNumber nvarchar(50) null,
--	MemberId int not null Constraint Agents_MemberId_fk References dbo.[Member](MemberId),
--	ReferalCode nvarchar(100) null,
--	ApprovedDate datetime null,
--	ApprovedBy int null Constraint Agents_Users_fk References dbo.[Users](UserId),
--	[Status] tinyint not null
--);
--GO

--GO
--CREATE UNIQUE INDEX Agents_LicenceNumber_ui ON
--dbo.[Agents](LicenceNumber) where LicenceNumber is not null and LicenceNumber<>''
--GO

--GO
--CREATE UNIQUE INDEX Agents_ReferalCode_ui ON
--dbo.[Agents](ReferalCode) where ReferalCode is not null and ReferalCode<>''
--GO

--GO
--CREATE UNIQUE INDEX Agents_MemberId_ui ON
--dbo.[Agents](MemberId)
--GO

--GO
--INSERT INTO [dbo].[Agents]
--(
--	MemberId,
--	ReferalCode,
--	ApprovedBy,
--	ApprovedDate
--)
--SELECT MemberId,ReferalCode,1,GETDATE()
--FROM [dbo].[Member]
--where IsActive
--GO

GO
DROP VIEW [dbo].[MemberView]
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT			B.MemberId,B.FirstName, B.MiddleName, B.LastName, B.MobileNumber, B.Email, B.DateOfBirthBS, B.DateOfBirthAD, B.GenderId, B.OccupationId, B.OtherOccupationRemarks,
					B.ShareTypeId,B.CitizenshipNumber, B.FormStatus, B.CreatedDate, B.CreatedBy, B.UpdatedDate, B.UpdatedBy, B.ApprovalStatus, B.ApprovedDate, B.ApprovedBy, B.ReferalCode, B.ReferenceId, B.IsActive, B.UserId, 
                         B.ApprovalRemarks, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, A.TemporaryAddress, 
                         A.TemporaryWardNumber, A.TemporaryToleName, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName, TC.Name AS TemporaryCountryName, 
                         (B1.FirstName+(case when isnull(B1.MiddleName,'')='' then '' else ' '+B1.MiddleName end)+isnull(B1.LastName,'')) AS ReferenceFullName,B1.ReferalCode AS ReferenceReferalCode,
						 O.[Name] AS OcuupationName, 
                         UD.Photo, UD.CitizenshipFront, UD.CitizenshipBack, BD.Amount, BD.VoucherImage, G.GenderName, H.ShareTypeName,PF.AppliedKitta
FROM            dbo.Member AS B LEFT OUTER JOIN
                         dbo.Address AS A ON A.MemberId = B.MemberId LEFT OUTER JOIN
                         dbo.Province AS PP ON PP.ProvinceId = A.PermanentProvinceId LEFT OUTER JOIN
                         dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT OUTER JOIN
                         dbo.Province AS TP ON TP.ProvinceId = A.TemporaryProvinceId LEFT OUTER JOIN
                         dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT OUTER JOIN
                         --dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT OUTER JOIN
                         --dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT OUTER JOIN
                         --dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT OUTER JOIN
                         dbo.Country AS TC ON TC.Id = A.TemporaryCountryId LEFT OUTER JOIN
                         dbo.Member AS B1 ON B.ReferenceId = B1.MemberId LEFT OUTER JOIN
                         dbo.Occupation AS O ON O.Id = B.OccupationId LEFT OUTER JOIN
                         --dbo.MemberField AS MF ON MF.Id = B.MemberFieldId LEFT OUTER JOIN
                         dbo.UserDocuments AS UD ON UD.MemberId = B.MemberId LEFT OUTER JOIN
                         dbo.BankDeposit AS BD ON BD.MemberId = B.MemberId AND BD.IsVoucherDeposit = 1 LEFT OUTER JOIN
                         dbo.Gender AS G ON G.GenderId = B.GenderId LEFT OUTER JOIN
                         dbo.ShareTypes AS H ON H.ShareTypeId = B.ShareTypeId LEFT OUTER JOIN
						 dbo.PratigyaPatraFormFillups AS PF ON PF.MemberId=B.MemberId

GO

GO
--CREATE VIEW [dbo].[MemberFilterView]
--AS
--SELECT			B.MemberId,(B1.FirstName+case when isnull(B1.MiddleName,'')!='' then (' '+B1.MiddleName) else '' end+B1.LastName) AS FullName, B.MobileNumber, B.Email, B.DateOfBirthBS, B.DateOfBirthAD, B.GenderId, B.OccupationId, B.OtherOccupationRemarks,
--					B.ShareTypeId,B.CitizenshipNumber, B.FormStatus, B.CreatedDate, B.CreatedBy, B.UpdatedDate, B.UpdatedBy, B.ApprovalStatus, B.ApprovedDate, B.ApprovedBy, B.ReferalCode, B.ReferenceId, B.IsActive, B.UserId, 
--                         B.ApprovalRemarks, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipality, A.PermanentWardNumber, 
--                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, A.TemporaryAddress, 
--                         A.TemporaryWardNumber, A.TemporaryToleName, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
--                         TD.DistrictName AS TemporaryDistrictName, TC.Name AS TemporaryCountryName, 
--                         (B1.FirstName+case when isnull(B1.MiddleName,'')!='' then (' '+B1.MiddleName) else '' end+B1.LastName) AS ReferenceFullName,B1.ReferalCode AS ReferenceReferalCode,
--						 O.[Name] AS OcuupationName, 
--                         UD.Photo, UD.CitizenshipFront, UD.CitizenshipBack, BD.Amount, BD.VoucherImage, G.GenderName, H.ShareTypeName,PF.AppliedKitta
--FROM            dbo.Member AS B LEFT OUTER JOIN
--                         dbo.Address AS A ON A.MemberId = B.MemberId LEFT OUTER JOIN
--                         dbo.Province AS PP ON PP.ProvinceId = A.PermanentProvinceId LEFT OUTER JOIN
--                         dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT OUTER JOIN
--                         dbo.Province AS TP ON TP.ProvinceId = A.TemporaryProvinceId LEFT OUTER JOIN
--                         dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT OUTER JOIN
--                         --dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT OUTER JOIN
--                         --dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT OUTER JOIN
--                         --dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT OUTER JOIN
--                         dbo.Country AS TC ON TC.Id = A.TemporaryCountryId LEFT OUTER JOIN
--                         dbo.Member AS B1 ON B.ReferenceId = B1.MemberId LEFT OUTER JOIN
--                         dbo.Occupation AS O ON O.Id = B.OccupationId LEFT OUTER JOIN
--                         --dbo.MemberField AS MF ON MF.Id = B.MemberFieldId LEFT OUTER JOIN
--                         dbo.UserDocuments AS UD ON UD.MemberId = B.MemberId LEFT OUTER JOIN
--                         dbo.BankDeposit AS BD ON BD.MemberId = B.MemberId AND BD.IsVoucherDeposit = 1 LEFT OUTER JOIN
--                         dbo.Gender AS G ON G.GenderId = B.GenderId LEFT OUTER JOIN
--                         dbo.ShareTypes AS H ON H.ShareTypeId = B.ShareTypeId LEFT OUTER JOIN
--						 dbo.PratigyaPatraFormFillups AS PF ON PF.MemberId=B.MemberId
GO

GO
DROP PROC [dbo].[FilterMember]
GO

GO
CREATE PROC [dbo].[FilterMember]
(
	@ApprovalStatus int,
	@FormStatus int,
	@ReferenceId int
)
AS
BEGIN
	SELECT m.MemberId,(m.FirstName+case when isnull(m.MiddleName,'')!='' then (' '+m.MiddleName) else '' end+m.LastName) AS FullName,
	m.MobileNumber,m.Email,m.CreatedDate,m.ApprovedDate,m.FormStatus,m.ApprovalStatus,m.ReferalCode,
	m.IsActive,p.AppliedKitta,
	(m1.FirstName+case when isnull(m1.MiddleName,'')!='' then (' '+m1.MiddleName) else '' end+m1.LastName) AS ReferenceFullName
	FROM Member m
	LEFT JOIN [dbo].[PratigyaPatraFormFillups] p on p.MemberId=m.MemberId
	LEFT JOIN [dbo].[Member] m1 on m1.MemberId=m.ReferenceId
	WHERE
	(
		(@ApprovalStatus IS NULL OR m.ApprovalStatus=@ApprovalStatus)
		AND
		(@ReferenceId IS NULL OR m.ReferenceId=@ReferenceId)
		AND
		(@FormStatus IS NULL OR m.FormStatus=@FormStatus)
	)
	ORDER BY m.FormStatus DESC,m.CreatedDate DESC
END
GO

GO
CREATE TABLE [dbo].[AgentStatus]
(
	Id int not null Identity(1,1) constraint AgentStatus_pk primary key,
	StatusName nvarchar(200) not null,
	MaxMemberNumber int not null,
	IsActive bit not null,
);
GO

GO
SET IDENTITY_INSERT [dbo].[AgentStatus] ON;
GO
GO
INSERT INTO [dbo].[AgentStatus](Id,StatusName,MaxMemberNumber,IsActive)
select 1,N'Neutral',0,1 union all
select 2,N'Silver',10,1 union all
select 3,N'Gold',50,1 union all
select 4,N'Diamond',500,1
GO
GO
SET IDENTITY_INSERT [dbo].[AgentStatus] OFF;
GO

GO
CREATE TABLE [dbo].[Agent]
(
	AgentId int not null Identity(1,1) constraint Agent_pk primary key,
	AgentFullName nvarchar(200) not null,
	ProvinceId int not null Constraint Agent_ProvinceId_fk References dbo.[Province](ProvinceId),
	DistrictId int not null Constraint Agent_District_fk References dbo.[District](DistrictId),
	MunicipalityName nvarchar(200),
	WardNumber int not null,
	ToleName nvarchar(100) null,
	ContactNumber1 nvarchar(20) not null,
	ContactNumber2 nvarchar(20) null,
	EmailAddress nvarchar(500) null,
	CitizenshipNumber nvarchar(100) null,
	LicenceNumber nvarchar(200) null,
	Qualification nvarchar(200) null,
	Occupation nvarchar(200) null,
	AgentStatusId int not null Constraint Agent_AgentStatusId_fk References dbo.[AgentStatus](Id),
	IsActive bit not null default(1),
	MemberId int null Constraint Agent_MemberId_fk References dbo.[Member](MemberId),
	ReferenceAgentId int null Constraint Agent_ReferenceAgentId_fk References dbo.[Agent](AgentId),
	CreatedBy int null Constraint Agent_CreatedBy_fk References dbo.[Users](UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Agent_UpdatedBy_fk References dbo.[Users](UserId),
	UpdatedDate datetime null,
);
GO

GO
CREATE UNIQUE INDEX Agent_ContactNumber1_ui ON
dbo.[Agent](ContactNumber1) where IsActive<>0
GO

GO
CREATE UNIQUE INDEX Agent_EmailAddress_ui ON
dbo.[Agent](EmailAddress) where IsActive<>0 and EmailAddress is not null and EmailAddress<>''
GO

GO
CREATE or alter VIEW dbo.[AgentView]
AS
select a.*,s.StatusName as AgentStatusName,
		(m.FirstName+ (case when isnull(m.MiddleName,'')= N'' then '' else m.MiddleName end) + ' '+m.LastName) as MemberFullName,
		(case when isnull(a.MemberId,0)=0 then a1.LicenceNumber else m.ReferalCode end)  as ReferenceLicenceNumber ,
		(case when isnull(a.MemberId,0)=0 then a1.AgentFullName else (m.FirstName+ (case when isnull(m.MiddleName,'')= N'' then '' else m.MiddleName end) + ' '+m.LastName) end)  as ReferenceAgentName,
		--a1.AgentFullName as  ReferenceAgentName,
		st.StaffName as CreatedByName,
		p.ProvinceName,d.DistrictName
from			dbo.[Agent] a
inner join		dbo.[AgentStatus] s on s.Id=a.AgentStatusId
left join		dbo.[Member] m on m.MemberId=a.MemberId
left join		dbo.[Agent] a1 on a1.AgentId=a.ReferenceAgentId
left join		dbo.[Staffs] st on st.UserId=a.CreatedBy
left join		dbo.[Province] p on p.ProvinceId=a.ProvinceId
left join		dbo.[District] d on d.DistrictId=a.DistrictId 
GO

GO
ALTER TABLE dbo.[ShareTypes]
ADD RegistrationAmount money not null default(0)
GO

GO
DROP VIEW [dbo].[ShareTypeView]
GO

GO
CREATE VIEW [dbo].[ShareTypeView]
AS
SELECT A.*
,B.FiscalYearName
FROM ShareTypes AS A
JOIN FiscalYear AS B ON B.FiscalYearId=A.FiscalYearId
GO

GO
UPDATE dbo.[Member]
set ReferalCode=N'REF-2021-887'
where MemberId=1
GO