
GO
CREATE TABLE [dbo].[CustomerInfo]
(
	CustomerId int not null Identity(1,1) Constraint CustomerInfo_pk Primary Key,
	CustomerName nvarchar(150) not null,
	Address nvarchar(200) not null,
	ProvinceId int not null Constraint CustomerInfo_Province_ProvinceId_fk References Province(ProvinceId),
	DistrictId int not null Constraint CustomerIndo_District_DistrictId_fk References District(DistrictId),
	ContactNumber nvarchar(15) not null,
	ContactNumber1 nvarchar(15) null default(''),
	EmailAddress nvarchar(150) null default(''),
	PanNumber nvarchar(50) null default(''),
	Status bit null default(1),
	CreatedBy int not null Constraint CustomerInfo_User_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint CustomerInfo_User_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null,
);
GO

GO
CREATE UNIQUE INDEX CustomerInfo_ContactNumber_ui ON
CustomerInfo(ContactNumber);
GO

GO
CREATE UNIQUE INDEX CustomerInfo_ContactNumber1_ui ON
CustomerInfo(ContactNumber1) WHERE ContactNumber1 IS NOT NULL;
GO

GO
CREATE UNIQUE INDEX CustomerInfo_EmailAddress_ui ON
CustomerInfo(EmailAddress) WHERE EmailAddress IS NOT NULL;
GO

GO
CREATE UNIQUE INDEX CustomerInfo_PanNumber_ui ON
CustomerInfo(PanNumber) WHERE PanNumber IS NOT NULL;
GO


GO
CREATE VIEW [dbo].[CustomerInfoView]
AS
SELECT A.*,B.ProvinceName,C.DistrictName 
FROM CustomerInfo AS A
LEFT JOIN Province AS B ON B.ProvinceId=A.ProvinceId
LEFT JOIN District AS C ON C.DistrictId=A.DistrictId
GO

GO
CREATE PROC [dbo].[Sp_GetAllCustomer]
(
	@ProvinceId int,
	@DistrictId int
)
AS
BEGIN
SELECT * FROM [dbo].[CustomerInfoView]
WHERE
(
	((ISNULL(@ProvinceId,'')='') OR ProvinceId=@ProvinceId)
	AND
	((ISNULL(@DistrictId,'')='') OR DistrictId=@DistrictId)
)
END
GO

--drop table [dbo].[CustomerInfo]