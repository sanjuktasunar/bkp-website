
GO
CREATE TABLE [dbo].[Country]
(
	Id int not null Identity(1,1) Constraint Country_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1),
	IsOutsideNepal bit null,
);
GO
CREATE UNIQUE INDEX Country_Name_ui ON
[dbo].[Country](Name)
GO

GO
CREATE UNIQUE INDEX Country_NepaliName_ui ON
[dbo].[Country](NepaliName)
GO



GO
CREATE TABLE [dbo].[Occupation]
(
	Id int not null Identity(1,1) Constraint Occupation_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX Occupation_Name_ui ON
[dbo].[Occupation](Name)
GO

GO
CREATE UNIQUE INDEX Occupation_NepaliName_ui ON
[dbo].[Occupation](NepaliName)
GO


GO
CREATE TABLE [dbo].[MemberField]
(
	Id int not null Identity(1,1) Constraint MemberField_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX MemberField_Name_ui ON
[dbo].[MemberField](Name)
GO

GO
CREATE UNIQUE INDEX MemberField_NepaliName_ui ON
[dbo].[MemberField](NepaliName)
GO


GO
CREATE TABLE [dbo].[AccountHead]
(
	AccountHeadId int not null Identity(1,1) Constraint AccountHead_pk Primary Key,
	AccountHeadName nvarchar(350) not null,
	AccountHolderName nvarchar(150) not null,
	AccountNumber nvarchar(100) not null,
	Address nvarchar(150) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX AccountHead_AccountNumber_ui ON
[dbo].[AccountHead](AccountNumber)
GO

GO
CREATE TABLE [dbo].[MunicipalityType]
(
	Id int not null Identity(1,1) Constraint MunicipalityType_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX MunicipalityType_Name_ui ON
[dbo].[MunicipalityType](Name)
GO

GO
CREATE UNIQUE INDEX MunicipalityType_NepaliName_ui ON
[dbo].[MunicipalityType](NepaliName)
GO


GO
CREATE TABLE [dbo].[MemberType]
(
	Id int not null Identity(1,1) Constraint MemberType_pk Primary Key,
	Name nvarchar(100) not null,
	NepaliName nvarchar(200) not null,
	Status bit null default(1)
);
GO
GO
CREATE UNIQUE INDEX MemberType_Name_ui ON
[dbo].[MemberType](Name)
GO

GO
CREATE UNIQUE INDEX MemberType_NepaliName_ui ON
[dbo].[MemberType](NepaliName)
GO


GO
CREATE TABLE [dbo].[Member]
(
	MemberId int not null Identity(1,1) Constraint Members_pk Primary Key,
	MemberCode nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	MiddleName nvarchar(50) null,
	LastName nvarchar(50) not null,
	MobileNumber nvarchar(20) null,
	Email nvarchar(200) null,
	DateOfBirthBS nvarchar(10) null,
	DateOfBirthAD date null,
	GenderId int null Constraint Member_Gender_GenderId_fk References Gender(GenderId),
	OccupationId int null Constraint Member_Occupation_OccupationId_fk References Occupation(Id),
	OtherOccupationRemarks nvarchar(100) null,
	MemberFieldId int null Constraint Member_MemberField_MemberFieldId_fk References MemberField(Id) ,
	CitizenshipNumber nvarchar(100) null,
	IsMemberFilled bit null default(1),
	FormStatus int not null,
	CreatedDate datetime null,
	CreatedBy int null Constraint Member_User_CreatedByUserId_fk References Users(UserId),
	UpdatedDate datetime null,
	UpdatedBy int null Constraint Member_User_UpdateByUserId_fk References Users(UserId),
	ApprovalStatus int not null,
	ApprovedDate datetime null,
	ApprovedBy int null Constraint Member_User_ApproveByUserId_fk References Users(UserId),
	ReferalCode nvarchar(50) null default(''),
	ReferenceId int null Constraint Member_ReferenceId_fk References Member(MemberId) 
);
GO

GO
CREATE UNIQUE INDEX Member_MobileNumber_ui ON
[dbo].[Member](MobileNumber) WHERE MobileNumber IS NOT NULL
GO
GO
CREATE UNIQUE INDEX Member_Email_ui ON
[dbo].[Member](Email) WHERE Email IS NOT NULL
GO
GO
CREATE UNIQUE INDEX Member_CitizenshipNumber_ui ON
[dbo].[Member](CitizenshipNumber) WHERE CitizenshipNumber IS NOT NULL
GO
GO
CREATE UNIQUE INDEX Member_MemberCode_ui ON
[dbo].[Member](MemberCode) 
GO

GO
CREATE UNIQUE INDEX Member_ReferalCode_ui ON
[dbo].[Member](ReferalCode) WHERE ReferalCode IS NOT NULL
GO


GO
DROP TABLE [dbo].[UserDocuments]
GO

GO
CREATE TABLE [dbo].[UserDocuments]
(
	UserDocumentId int not null Identity(1,1) Constraint UserDocuments_pk Primary Key,
	StaffId int null Constraint UserDocuments_Staffs_StaffId References Staffs(StaffId),
	MemberId int null Constraint UserDocument_Member_MemberId_fk References Member(MemberId),
	Photo nvarchar(max) null,
	CitizenshipFront nvarchar(max) null,
	CitizenshipBack nvarchar(max) null,
	PanCard nvarchar(max) null,
	EducationalDocument nvarchar(max) null
);
GO

GO
CREATE UNIQUE INDEX UserDocument_MemberId_ui ON
[dbo].[UserDocuments](MemberId) WHERE MemberId is not null
GO
GO
CREATE UNIQUE INDEX UserDocument_StaffId_ui ON
[dbo].[UserDocuments](StaffId) WHERE StaffId is not null
GO



GO
CREATE TABLE [dbo].[MemberDetails]
(
	Id int not null Identity(1,1) Constraint MemberDetails_pk Primary Key,
	MemberId int not null Constraint MemberDetails_Members_MemberId References Member(MemberId),
	MemberTypeId int not null Constraint MemberDetails_Member_MemberId_fk References MemberType(Id),
	IsPrimary bit not null,
	CreatedDate datetime not null,
	CreatedBy int null Constraint MemberDetails_Users_CreatedBy_fk References Users(UserId),
);
GO


GO
CREATE TABLE [dbo].[Address]
(
	Id int not null Identity(1,1) Constraint Address_pk Primary Key,
	MemberId int null Constraint Address_Member_MemberId_fk References Member(MemberId),
	PermanentIsOutsideNepal bit null default(0),
	PermanentCountryId int null  Constraint Address_Country_PermanentCountryId_fk References Country(Id),
	PermanentProvinceId int null Constraint Address_Province_PermanentProvinceId References Province(ProvinceId),
	PermanentDistrictId int null Constraint Address_District_PermanentDistrictId References District(DistrictId),
	PermanentMunicipalityTypeId int null Constraint Address_MunicipalityType_PermanentMunicipalityTypeId References MunicipalityType(Id),
	PermanentMunicipality nvarchar(100) null,
	PermanentWardNumber nvarchar(100) null,
	PermanentToleName nvarchar(100) null,
	PermanentAddress nvarchar(500) null,
	TemporaryIsOutsideNepal bit null default(0),
	TemporaryCountryId int null  Constraint Address_Country_TemporaryCountryId_fk References Country(Id),
	TemporaryProvinceId int null Constraint Address_Province_TemporaryProvinceId References Province(ProvinceId),
	TemporaryDistrictId int null Constraint Address_District_TemporaryDistrictId References District(DistrictId),
	TemporaryMunicipalityTypeId int null Constraint Address_MunicipalityType_TemporaryMunicipalityTypeId References MunicipalityType(Id),
	TemporaryMunicipality nvarchar(100) null,
	TemporaryWardNumber nvarchar(100) null,
	TemporaryToleName nvarchar(100) null,
	TemporaryAddress nvarchar(500) null
);
GO

GO
CREATE UNIQUE INDEX Address_MemberId_ui ON
Address(MemberId) WHERE MemberId IS NOT NULL
GO


GO
CREATE TABLE [dbo].[BankDeposit]
(
	Id int not null Identity(1,1) Constraint BankDeposit_pk Primary Key,
	MemberId int null Constraint BankDeposit_Member_MemberId_fk References Member(MemberId),
	IsOther bit null default(0),
	IsVoucherDeposit bit null default(0),
	Name nvarchar(100) null,
	PhoneNumber nvarchar(10) null,
	Address nvarchar(150) null,
	Amount decimal(18,2) not null,
	AccountHeadId int null Constraint BankDeposit_AccountHead_AccountHeadId References AccountHead(AccountHeadId),
	DepositDate datetime null,
	VoucherImage nvarchar(max) null,
	IsApproved bit null default(0),
	ApprovedDate datetime null,
);
GO
GO
CREATE UNIQUE INDEX BankDeposit_MemberId_ui ON
BankDeposit(MemberId) WHERE MemberId IS NOT NULL
GO

GO
CREATE VIEW [dbo].[MemberView]
AS
SELECT A.*,B.FirstName AS RefernceFirstName,B.MiddleName AS ReferenceMiddleName,B.LastName AS ReferenceLastName,
B.ReferalCode AS ReferenceReferalCode
FROM Member AS A
LEFT JOIN Member AS B ON A.ReferenceId=B.MemberId
GO
