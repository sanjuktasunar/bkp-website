
GO
ALTER TABLE dbo.[Users]
ADD FullName nvarchar(200) null default('')
GO

GO
CREATE OR ALTER VIEW dbo.[UsersView]
AS
select		u.*,
			ut.UserTypeTitle,r.RoleName,
			us.StatusName
from		[dbo].[Users] u
left join	dbo.[UserType] ut on ut.UserTypeId=u.UserTypeId
left join	dbo.[Role] r on r.RoleId=u.RoleId
left join	dbo.[UserStatus] us on us.StatusId=u.UserStatusId
GO

GO
EXEC sp_rename 'dbo.Member', 'Member_Old';
GO

GO
CREATE TABLE dbo.[Member]
(
	MemberId int not null Identity(1,1) Constraint Member_New_pk Primary Key,
	MemberCode nvarchar(100) null,
	FullName nvarchar(200) null,
	HusbandName nvarchar(200) null,
	FathersName nvarchar(200) null,
	Age int not null,
	CitizenshipNumber nvarchar(100) null,
	FormerAddress nvarchar(300) null,
	PermanentAddress nvarchar(300) null,
	TemporaryDistrictId int null Constraint MemberNew_TempDistrictId_fk References dbo.[District](DistrictId),
	TemporaryMunicipalityName nvarchar(200) null,
	TemporaryWardNumber nvarchar(4) null,
	ContactNumber nvarchar(15) null,
	EmailAddress nvarchar(300) null,
	AppliedShareKitta int null,
	ShareTypeId int null Constraint Member_ShareTypeId_fk References dbo.[ShareTypes](ShareTypeId),
	TotalShareAmount money null,
	TotalSharePaidAmount money null,
	ReferenceId int null Constraint Member_Referal_ReferenceMemberId_fk References dbo.[Member](MemberId),
	AgentId int null Constraint Member_Agent_AgentId_fk References dbo.[Agent](AgentId),
	NomineeName nvarchar(200) null,
	SellerMemberId int null Constraint Member_SellerMemberId_fk References dbo.[Member](MemberId),
	IsApproved int not null,
	RejectRemarks nvarchar(200) null,
	IsShareholder int null,
	ReferalCode nvarchar(100) null,
	CreatedDate datetime null default(getdate()),
	CreatedBy int null Constraint Member_User_CreatedBy_fk References dbo.[Users](UserId),
	UpdatedDate datetime null,
	UpdatedBy int null Constraint Member_User_UpdatedBy_fk References dbo.[Users](UserId),
	ApprovedDate datetime null,
	ApprovedBy int null Constraint Member_User_ApprovedBy_fk References dbo.[Users](UserId),
);
GO

GO
CREATE UNIQUE INDEX Member_New_CitizenshipNumber_ui ON
dbo.[Member](CitizenshipNumber) where CitizenshipNumber <>'' and CitizenshipNumber is not null and IsApproved<>3
GO

GO
CREATE UNIQUE INDEX Member_New_EmailAddress_ui ON
dbo.[Member](EmailAddress) where EmailAddress <>'' and EmailAddress is not null and IsApproved<>3
GO

GO
CREATE UNIQUE INDEX Member_New_ContactNumber_ui ON
dbo.[Member](ContactNumber) where ContactNumber <>'' and ContactNumber is not null and IsApproved<>3
GO

GO
CREATE UNIQUE INDEX Member_New_ReferalCode_ui ON
dbo.[Member](ReferalCode) where ReferalCode <>'' and ReferalCode is not null
GO

GO
CREATE UNIQUE INDEX Member_New_MemberCode_ui ON
dbo.[Member](MemberCode) where MemberCode <>'' and MemberCode is not null
GO


GO
SET IDENTITY_INSERT [dbo].[Member] ON;
GO

GO
INSERT INTO dbo.[Member]
(
	MemberId,
	FullName,
	MemberCode,
	Age,
	CitizenshipNumber,
	TemporaryDistrictId,
	TemporaryMunicipalityName,
	TemporaryWardNumber,
	FormerAddress,
	PermanentAddress,
	ContactNumber,
	EmailAddress,
	AppliedShareKitta,
	ShareTypeId,
	ReferenceId,
	AgentId,
	IsApproved,
	IsShareholder,
	ReferalCode,
	CreatedDate,
	CreatedBy,
	UpdatedDate,
	UpdatedBy,
	ApprovedDate,
	ApprovedBy,
	RejectRemarks
)
select 
m.MemberId,
m.FirstName+case when isnull(m.MiddleName,'')='' then '' else ' '+m.MiddleName end+' '+m.LastName,
m.MemberCode,
case when isnull(m.DateOfBirthAD,'')='' then 0 else datediff(year,convert(date, m.DateOfBirthAD), getdate()) end,
m.CitizenshipNumber,
ad.TemporaryDistrictId,
ad.TemporaryMunicipality,
ad.TemporaryWardNumber,
ad.FormerMunicipalityName+'-'+ad.FormerWardNumber+','+fd.DistrictName,
ad.PermanentMunicipality+'-'+ad.PermanentWardNumber+','+pd.DistrictName,
m.MobileNumber,
m.Email,
sh.TotalKitta,
sh.ShareTypeId,
m.ReferenceId,
m.AgentId,
m.ApprovalStatus,
case when m.ShareholderId>0 then 1 else 0 end,
m.ReferalCode,
m.CreatedDate,
m.CreatedBy,
m.UpdatedDate,
m.UpdatedBy,
m.ApprovedDate,
m.ApprovedBy,
m.ApprovalRemarks
from
			dbo.[Member_Old] m
left join	dbo.[Shareholder] sh on sh.MemberId=m.MemberId
left join	dbo.[Address] ad on ad.MemberId=m.MemberId 
left join	dbo.District td on td.DistrictId=ad.TemporaryDistrictId
left join	dbo.District fd on fd.DistrictId=ad.FormerDistrictId
left join	dbo.District pd on pd.DistrictId=ad.PermanentDistrictId
left join	dbo.Country tc on tc.Id=ad.TemporaryCountryId
GO

GO
SET IDENTITY_INSERT [dbo].[Member] OFF;
GO

GO
ALTER TABLE [dbo].[Shareholder] DROP CONSTRAINT [Shareholder_MemberId_fk]
GO

GO
ALTER TABLE [dbo].[Shareholder]  WITH CHECK ADD  CONSTRAINT [Shareholder_MemberId_fk] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
GO
ALTER TABLE [dbo].[Shareholder] CHECK CONSTRAINT [Shareholder_MemberId_fk]
GO

--GO
--EXEC sp_rename 'dbo.Member.RefernceId', 'ReferenceId', 'COLUMN';
--GO


GO
ALTER TABLE [dbo].[Agent] DROP CONSTRAINT [Agent_ProvinceId_fk]
GO

GO
ALTER TABLE dbo.[Agent]
DROP COLUMN ProvinceId
GO

GO
ALTER VIEW [dbo].[AgentView]
AS
select a.*,s.StatusName as AgentStatusName,
		m.FullName as MemberFullName,
		(case when isnull(a.MemberId,0)=0 then a1.LicenceNumber else m.ReferalCode end)  as ReferenceLicenceNumber ,
		(case when isnull(a.MemberId,0)=0 then a1.AgentFullName else (m.FullName) end)  as ReferenceAgentName,
		(case when isnull(a.MemberId,0)=0 then a1.ContactNumber1 else m.ContactNumber end)  as ReferencePhoneNumber ,
		st.StaffName as CreatedByName,
		p.ProvinceName,d.DistrictName
from			dbo.[Agent] a
inner join		dbo.[AgentStatus] s on s.Id=a.AgentStatusId
left join		dbo.[Member] m on m.MemberId=a.MemberId
left join		dbo.[Agent] a1 on a1.AgentId=a.ReferenceAgentId
left join		dbo.[Staffs] st on st.UserId=a.CreatedBy
left join		dbo.[District] d on d.DistrictId=a.DistrictId 
left join		dbo.[Province] p on p.ProvinceId=d.ProvinceId
GO

GO
ALTER PROC [dbo].[Sp_GetReferenceMembers]
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
SELECT b.MemberId,b.FullName as ReferenceName
FROM Member as b
join #temp as a on a.ReferenceID=b.MemberId
select MemberId AS [Key],ReferenceName AS [Value] from #refMem
END
GO

GO
CREATE OR ALTER VIEW dbo.[ShareholderView]
AS
select		m.FullName,m.MemberId,s.TotalKitta,s.ShareholderId,s.ShareTypeId,
			st.ShareTypeName,s.IsActive,m.ContactNumber
from		dbo.Shareholder s
inner join	dbo.Member m on m.MemberId=s.MemberId
inner join	dbo.ShareTypes st on st.ShareTypeId=s.ShareTypeId
GO

GO
DROP VIEW dbo.MemberView
GO

GO
DROP VIEW dbo.MemberDetailView
GO

GO
CREATE OR ALTER VIEW dbo.[MemberView]
AS
select		m.*,
			d.DistrictName as TemporaryDistrictName,st.ShareTypeName,st.PricePerKitta as SharePricePerKitta,
			sm.FullName as SellerFullName,sm.ContactNumber as SellerPhoneNumber,
			(case when m.ReferenceId is not null then rm.FullName else ag.AgentFullName end) as ReferenceFullName,
			(case when m.ReferenceId is not null then rm.ContactNumber else ag.ContactNumber1 end) as ReferencePhoneNumber,
			(case when m.ReferenceId is not null then rm.ReferalCode else ag.LicenceNumber end) as ReferenceLicenceNumber,
			u.FullName as ApprovedByFullName,u1.FullName as CreatedByFullName
from		dbo.Member m
left join	dbo.District d on d.DistrictId=m.TemporaryDistrictId
left join	dbo.ShareTypes st on st.ShareTypeId=m.ShareTypeId
left join	dbo.Member sm on sm.MemberId=m.SellerMemberId
left join	dbo.Member rm on rm.MemberId=m.ReferenceId
left join	dbo.Agent ag on ag.AgentId=m.AgentId
left join	dbo.Users u on u.UserId=m.ApprovedBy
left join	dbo.Users u1 on u1.UserId=m.CreatedBy
GO

GO
ALTER PROC [dbo].[FilterMember]
(
	@ApprovalStatus int,
	@ReferenceId int,
	@AgentId int,
	@ShareTypeId int,
	@SearchQuery nvarchar(100),
	@SellerMemberId int
)
AS
BEGIN
	/*
		exec [dbo].[FilterMember] 1,0,0,0
	*/

	SELECT m.MemberId,m.FullName,
	m.ContactNumber,m.EmailAddress,m.CreatedDate,m.ApprovedDate,m.IsApproved,m.ReferalCode,m.RejectRemarks,
	m.CitizenshipNumber,m.IsShareholder,m.ShareTypeId,
	m.ReferenceFullName,m.ReferencePhoneNumber,m.ShareTypeName,m.AppliedShareKitta,
	m.SellerFullName,m.SellerPhoneNumber,m.TotalSharePaidAmount,(m.AppliedShareKitta*m.SharePricePerKitta) as TotalShareAmount
	FROM dbo.[MemberView] m
	WHERE
	(
		(isnull(@ApprovalStatus,0)=0 OR m.IsApproved=@ApprovalStatus)
		AND
		(isnull(@ReferenceId,0)=0 OR m.ReferenceId=@ReferenceId)
		AND
		(isnull(@AgentId,0)=0 OR m.AgentId=@AgentId)
		AND
		isnull(m.IsShareholder,0)=0
		AND
		(isnull(@ShareTypeId,0)=0 OR m.ShareTypeId=@ShareTypeId)
		AND
		(   isnull(@SearchQuery,'')='' OR 
			(m.ContactNumber=@SearchQuery) OR
			(m.CitizenshipNumber=@SearchQuery) OR
			(m.ReferalCode=@SearchQuery)
		)
		AND
		(isnull(@SellerMemberId,0)=0 or m.SellerMemberId=@SellerMemberId)
	)
	ORDER BY m.CreatedDate DESC
END
GO

GO
CREATE TABLE dbo.[MemberPaymentLog]
(
	PaymentId int not null Identity(1,1) Constraint MemberPaymentDetails_pk Primary Key,
	MemberId int not null Constraint MemberPaymentDetails_Member_MemberId_fk References dbo.[Member](MemberId),
	Amount money not null,
	CreatedBy int not null Constraint MemberPaymentDetails_CreatedBy_fk References dbo.[Users](UserId),
	CreatedDate datetime not null,
	IsDeleted bit not null default(0),
	DeletedDate datetime null,
	DeletedBy int null Constraint MemberPaymentDetails_DeletedBy_fk References dbo.[Users](UserId)
);
GO

GO
UPDATE dbo.[Users] set FullName=N'System Admin'
where UserId=1
GO