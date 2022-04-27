
GO
CREATE TABLE [dbo].[OrganizationInfo]
(
	OrganizationInfoId int not null Identity(1,1) Constraint OrganizationInfo_pk Primary Key,
	OrganizationName nvarchar(100) not null,
	AppName nvarchar(20) not null,
	Address nvarchar(200) not null,
	ContactNumber1 nvarchar(20) not null,
	ContactNumber2 nvarchar(20) null,
	TelephoneNumber nvarchar(20) null,
	EmailAddress nvarchar(200) null,
	FaxNumber nvarchar(100) null,
	POBoxNumber nvarchar(200) null,
	Logo image null
);
GO


GO
CREATE TABLE [dbo].[Language]
(
	LanguageId int not null Identity(1,1) Constraint Language_pk Primary Key,
	LanguageName nvarchar(30) not null,
	Code nvarchar(10) not null,
	Status bit null default(1)
);
GO

GO
CREATE UNIQUE INDEX Language_LanguageName_ui ON
Language(LanguageName);
GO


GO
CREATE UNIQUE INDEX Language_Code_ui ON
Language(Code)
GO


GO
CREATE TABLE [dbo].[Menus]
(
	MenuId int not null Identity(1,1) Constraint Menus_pk Primary Key,
	ParentMenuId int null Constraint Menus_fk References Menus(MenuId),
	MenuNameEnglish nvarchar(100) not null,
	MenuNameNepali nvarchar(100) not null,
	CheckMenuName nvarchar(50) not null,
	MenuLink nvarchar(200) not null,
	MenuOrder int not null,
	MenuIcon nvarchar(max) null,
	Status bit null default(1)
);
GO


GO
CREATE UNIQUE INDEX Menus_MenuNameEnglish ON
Menus(MenuNameEnglish,ParentMenuId)
GO

GO
CREATE UNIQUE INDEX Menus_CheckMenuName_ui ON
Menus(CheckMenuName)
GO


GO
CREATE UNIQUE INDEX Menus_MenuLink_ui ON
Menus(MenuLink) WHERE ParentMenuId IS NOT NULL
GO



GO
CREATE TABLE [dbo].[UserType]
(
	UserTypeId int not null Identity(1,1)  Constraint UserType_pk Primary Key,
	UserTypeTitle nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX UserType_UserTypeTitle_ui ON
UserType(UserTypeTitle)
GO


GO
CREATE TABLE [dbo].[Gender]
(
	GenderId int not null Identity(1,1)  Constraint Gender_pk Primary Key,
	GenderName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Gender_GenderName_ui ON
Gender(GenderName)
GO


GO
CREATE TABLE [dbo].[Role]
(
	RoleId int not null Identity(1,1)  Constraint Role_pk Primary Key,
	RoleName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Role_RoleName_ui ON
Role(RoleName)
GO


GO
CREATE TABLE [dbo].[Designation]
(
	DesignationId int not null Identity(1,1)  Constraint Designation_pk Primary Key,
	DesignationName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Designation_DesignationName_ui ON
Designation(DesignationName)
GO


GO
CREATE TABLE [dbo].[Department]
(
	DepartmentId int not null Identity(1,1)  Constraint Department_pk Primary Key,
	DepartmentName nvarchar(50) not null,
	Status bit null default(1),
	CreatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Department_DepartmentName_ui ON
Department(DepartmentName)
GO


GO
CREATE TABLE [dbo].[PhotoStorages]
(
	PhotoStorageId int not null Identity(1,1) Constraint PhotoStorages_pk Primary Key,
	Photo image null,
	PhotoLocation nvarchar(max) null
);
GO

GO
CREATE TABLE [dbo].[UserStatus]
(
	StatusId int not null Identity(1,1) constraint UserStatus_pk Primary Key,
	StatusName nvarchar(50) not null,
	UserTypeId int null Constraint UserStatus_UserType_UserTypeId References UserType(UserTypeId),
	Status bit null default(1)
);
GO

GO
CREATE UNIQUE INDEX UserStatus_StatusName_ui ON
UserStatus(StatusName,UserTypeId)
GO

GO
CREATE TABLE [dbo].[Users]
(
	UserId int not null Identity constraint Users_pk Primary Key,
	UserTypeId int not null Constraint Users_UserType_UserTypeId_fk References UserType(UserTypeId),
	PhotoStorageId int not null Constraint Users_PhotoStorages_PhotoStorageId_fk References PhotoStorages(PhotoStorageId),
	UserName nvarchar(50) not null,
	Password nvarchar(max) not null,
	EmailAddress nvarchar(200) not null,
	ContactNumber nvarchar(20) not null,
	UserStatusId int not null Constraint Users_UserStatus_UserStatusId_fk References UserStatus(StatusId),
	CreatedBy int null Constraint Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Users_UserName_ui ON
Users(UserName)
GO
GO
CREATE UNIQUE INDEX Users_EmailAddress_ui ON
Users(EmailAddress)
GO
GO
CREATE UNIQUE INDEX Users_ContactNumber_ui ON
Users(ContactNumber)
GO


GO
CREATE TABLE [dbo].[Staffs]
(
	StaffId int not null Identity(1,1) Constraint Staffs_pk Primary Key,
	UserId int not null Constraint Staffs_Users_UserId_fk References Users(UserId),
	RoleId int not null Constraint Staffs_Role_RoleId_fk References Role(RoleId),
	DesignationId int not null Constraint Staffs_Designation_DesignationId_fk References Designation(DesignationId),
	DepartmentId int null Constraint Staffs_Department_DepartmentId_fk References Department(DepartmentId),
	StaffName nvarchar(200) not null,
	GenderId int not null Constraint Staffs_Gender_GenderId_fk References Staffs(StaffId),
	TemporaryAddress nvarchar(200) not null,
	PermanentAddress nvarchar(200) not null,
	CitizenshipNumber nvarchar(150) null,
	PanNumber nvarchar(150) null,
	BasicSalary float not null,
);
GO
GO
CREATE UNIQUE INDEX Staffs_CitizenshipNumber_ui ON
Staffs(CitizenshipNumber) WHERE CitizenshipNumber IS NOT NULL
GO

GO
CREATE UNIQUE INDEX Staffs_PanNumber_ui ON
Staffs(PanNumber) WHERE PanNumber IS NOT NULL
GO

GO
CREATE TABLE [dbo].[UserDocuments]
(
	UserDocumentId int not null Identity(1,1) Constraint UserDocuments_pk Primary Key,
	StaffId int not null Constraint UserDocuments_Staffs_StaffId References Staffs(StaffId),
	CitizenshipFront nvarchar(500) null,
	CitizenshipBack nvarchar(500) null,
	PanCard nvarchar(500) null,
	EducationalDocument nvarchar(max) null
);
GO


GO
CREATE TABLE [dbo].[BankAccount]
(
	BankAccountId int not null Identity(1,1) Constraint BankAccount_pk Primary Key,
	StaffId int not null Constraint BankAccount_Staffs_StaffId References Staffs(StaffId),
	AccountNumber nvarchar(200) null,
	BankName nvarchar(500) null
);
GO

GO
CREATE TABLE [dbo].[Unit]
(
	UnitId int not null Identity(1,1) Constraint Unit_pk Primary Key,
	UnitName nvarchar(200) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_Unit_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Users_Unit_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Unit_UnitName_ui ON
Unit(UnitName)
GO

GO
CREATE TABLE [dbo].[Product]
(
	ProductId int not null Identity(1,1) Constraint Product_pk Primary Key,
	ParentProductId int null Constraint Product_ParentProductId_fk References Product(ProductId),
	ProductName nvarchar(200) not null,
	ProductCode nvarchar(50) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Users_Product_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Users_Product_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
CREATE UNIQUE INDEX Product_ProductName_ui ON
Product(ProductName,ParentProductId)
GO
GO
CREATE UNIQUE INDEX Product_ProductCode_ui ON
Product(ProductCode)
GO

GO
CREATE TABLE [dbo].[ProductPrice]
(
	ProductPriceId int not null Identity(1,1) Constraint ProductPrice_pk Primary Key,
	ProductId int not null Constraint ProductPrice_Product_ProductId_fk References Product(ProductId),
	UnitId int not null Constraint ProductPrice_Unit_UnitId_fk References Unit(UnitId),
	SellingPrice float not null,
	Status bit null default(1),
	UpdatedDate datetime not null
);
GO


GO
CREATE TABLE [dbo].[MenuAccessPermission]
(
	MenuAccessPermissionId int not null Identity(1,1) Constraint MenuAccessPermission_pk Primary Key,
	MenuId int not null Constraint MenuAccessPermission_Menus_MenuId_fk References Menus(MenuId),
	StaffId int not null Constraint MenuAccessPermission_Staffs_StaffId_fk References Staffs(StaffId),
	ReadAccess bit null Default(0),
	WriteAccess bit null Default(0),
	ModifyAccess bit null Default(0),
	DeleteAccess bit null Default(0),
	AdminAccess bit null Default(0),
);
GO
GO
CREATE UNIQUE INDEX MenuAccessPermission_MenuId_StaffId ON
MenuAccessPermission(MenuId,StaffId)
GO

GO
CREATE TABLE [dbo].[Province]
(
	ProvinceId int not null Identity(1,1) Constraint Province_pk Primary Key,
	ProvinceName nvarchar(100) not null,
	ProvinceNameNepali nvarchar(150) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Province_Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Province_Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Province_ProvinceName_ui ON 
Province(ProvinceName)
GO
GO
CREATE UNIQUE INDEX Province_ProvinceNameNepali_ui ON
Province(ProvinceNameNepali)
GO


GO
CREATE TABLE [dbo].[District]
(
	DistrictId int not null Identity(1,1) Constraint District_pk Primary Key,
	ProvinceId int not null Constraint District_Province_ProvinceId_fk References Province(ProvinceId),
	DistrictName nvarchar(100) not null,
	DistrictNameNepali nvarchar(150) not null,
	Status bit null default(1),
	CreatedBy int null Constraint District_Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint District_Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX District_DistrictName_ui ON
District(DistrictName)
GO
GO
CREATE UNIQUE INDEX District_DistrictNameNepali_ui ON
District(DistrictNameNepali)
GO

GO
CREATE TABLE [dbo].[Municipality]
(
	MunicipalityId int not null Identity(1,1) Constraint Municipality_pk Primary Key,
	DistrictId int not null Constraint Municipality_District_DistrictId_fk References District(DistrictId),
	MunicipalityName nvarchar(200) not null,
	MunicipalityNameNepali nvarchar(150) not null,
	Status bit null default(1),
	CreatedBy int null Constraint Municipality_Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint Municipality_Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX Municipality_MunicipalityName_ui ON
Municipality(MunicipalityName)
GO
GO
CREATE UNIQUE INDEX Municipality_MunicipalityNameNepali_ui ON
Municipality(MunicipalityNameNepali)
GO

