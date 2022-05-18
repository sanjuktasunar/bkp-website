

GO
DROP TABLE [dbo].[District]
GO


GO
DROP TABLE [dbo].[Province]
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
[dbo].[Province](ProvinceName)
GO
GO
CREATE UNIQUE INDEX Province_ProvinceNameNepali_ui ON
[dbo].[Province](ProvinceNameNepali)
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
[dbo].[District](DistrictName)
GO
GO
CREATE UNIQUE INDEX District_DistrictNameNepali_ui ON
[dbo].[District](DistrictNameNepali)
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
[dbo].[Municipality](MunicipalityName)
GO
GO
CREATE UNIQUE INDEX Municipality_MunicipalityNameNepali_ui ON
[dbo].[Municipality](MunicipalityNameNepali)
GO
