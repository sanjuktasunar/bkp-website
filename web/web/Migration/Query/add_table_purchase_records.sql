


GO
CREATE TABLE [dbo].[Supplier]
(
	SupplierId int not null Identity(1,1) Constraint Supplier_pk Primary Key,
	SupplierName nvarchar(500) not null,
	Address nvarchar(300),
	ContactNumber1 nvarchar(20) not null,
	ContactNumber2 nvarchar(20) null,
	EmailAddress nvarchar(200) null,
	Website nvarchar(500) null,
	PanNumber nvarchar(50) null,
	Status bit null default(1),
	CreatedBy int null Constraint Supplier_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint Supplier_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO


GO
CREATE UNIQUE INDEX Supplier_SupplierName_ui ON
[dbo].[Supplier](SupplierName)
GO

GO
CREATE UNIQUE INDEX Supplier_ContactNumber1_ui ON
[dbo].[Supplier](ContactNumber1)
GO

GO
CREATE UNIQUE INDEX Supplier_EmailAddress_ui ON
[dbo].[Supplier](EmailAddress) WHERE EmailAddress IS NOT NULL
GO

GO
CREATE UNIQUE INDEX Supplier_Website_ui ON
[dbo].[Supplier](EmailAddress) WHERE Website IS NOT NULL
GO


GO
CREATE UNIQUE INDEX Supplier_PanNumber_ui ON
[dbo].[Supplier](PanNumber) WHERE PanNumber IS NOT NULL
GO

GO
CREATE TABLE [dbo].[FiscalYear]
(
	FiscalYearId int not null Identity(1,1) Constraint FiscalYear_pk Primary Key,
	Name nvarchar(50) not null,
	NepaliName nvarchar(100) null,
	StartDateBS nvarchar(10) not null,
	StartDateAD date not null,
	EndDateBS nvarchar(10) not null,
	EndDateAD date not null,
	IsCurrent bit not null,
	Status bit null default(1),
	CreatedBy int null Constraint FiscalYear_User_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint FiscalYear_User_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO

GO
CREATE UNIQUE INDEX FiscalYear_Name_ui ON
[dbo].[FiscalYear](Name)
GO

GO
CREATE UNIQUE INDEX FiscalYear_NepaliName_ui ON
[dbo].[FiscalYear](NepaliName) WHERE NepaliName IS NOT NULL
GO

GO
CREATE UNIQUE INDEX FiscalYear_StartDateBS_ui ON
[dbo].[FiscalYear](StartDateBS)
GO

GO
CREATE UNIQUE INDEX FiscalYear_StartDateAD_ui ON
[dbo].[FiscalYear](StartDateAD)
GO

GO
CREATE UNIQUE INDEX FiscalYear_EndDateBS_ui ON
[dbo].[FiscalYear](EndDateBS)
GO

GO
CREATE UNIQUE INDEX FiscalYear_EndDateAD_ui ON
[dbo].[FiscalYear](EndDateAD)
GO


GO
CREATE TABLE [dbo].[PurchaseRecord]
(
	PurchaseRecordId int not null Identity(1,1) Constraint PurchaseRecord_pk Primary Key,
	SupplierId int not null Constraint PurchaseRecord_Supplier_SupplierId_fk References Supplier(SupplierId),
	FiscalYearId int not null Constraint PurchaseRecord_FiscalYear_FiscalYearId_fk References FiscalYear(FiscalYearId),
	InvoiceNumber bigint not null,
	PurchaseDateAD datetime not null,
	PurchaseDateBS nvarchar(10) not null,
	BillNumber nvarchar(30) not null,
	ApprovalStatus int not null,
	ApprovedDate datetime not null,
	ApprovalRemarks nvarchar(500) null default(''),
	BillImageString nvarchar(max) null,
	CreatedBy int null Constraint PurchaseRecord_User_CreatedBy_fk References Users(UserId),
	CreatedDate datetime not null default GETDATE(),
	UpdatedBy int null Constraint PurchaseRecord_User_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
);
GO
GO
CREATE UNIQUE INDEX PurchaseRecord_InvoiceNumber_ui ON
[dbo].[PurchaseRecord](InvoiceNumber)
GO


GO
CREATE TABLE [dbo].[PurchaseRecordDetail]
(
	Id int not null Identity(1,1) Constraint PurchaseRecordDetail_pk Primary Key,
	PurchaseRecordId int not null Constraint PurchaseRecordDetail_PurchaseRecord_PurchaseRecordId_fk References PurchaseRecord(PurchaseRecordId),
	ProductId int not null Constraint PurchaseRecordDetail_Product_ProductId_fk References Product(ProductId),
	UnitId int not null  Constraint PurchaseRecordDetail_Unit_UnitId_fk References Unit(UnitId),
	Quantity decimal(18,4) not null
);
GO

GO
CREATE TABLE [dbo].[Stock]
(
	Id int not null Identity(1,1) Constraint Stock_pk Primary Key,
	ProductId int not null Constraint Stock_Product_ProductId_fk References Product(ProductId),
	UnitId int not null  Constraint Stock_Unit_UnitId_fk References Unit(UnitId),
	Quantity decimal(18,4) not null,
	TotalPurchaseQuantity decimal(18,4) not null,
	TotalSellQuantity decimal(18,4) not null,
);
GO
