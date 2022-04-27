
GO
ALTER TABLE [dbo].[Unit]
ADD UnitSymbol nvarchar(20) not null
GO

GO
CREATE UNIQUE INDEX Unit_UnitSymbol_ui ON
[dbo].[Unit](UnitSymbol)
GO


GO
ALTER TABLE [dbo].[Unit]
ADD UnitSymbolNepali nvarchar(100) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitSymbolNepali_ui ON
[dbo].[Unit](UnitSymbolNepali)
GO


GO
ALTER TABLE [dbo].[Unit]
ADD UnitNameNepali nvarchar(400) not null
GO


GO
CREATE UNIQUE INDEX Unit_UnitNameNepali_ui ON
[dbo].[Unit](UnitNameNepali)
GO

GO
ALTER TABLE [dbo].[ProductPrice]
ADD CreatedBy int null Constraint ProductPrice_Users_CreatedBy_fk References Users(UserId)
GO

GO
ALTER TABLE [dbo].[ProductPrice]
ADD UpdatedBy int null Constraint ProductPrice_Users_UpdatedBy_fk References Users(UserId)
GO


GO
CREATE UNIQUE INDEX ProductPrice_Product_Unit_Price_ui ON
[dbo].[ProductPrice](ProductId,UnitId,SellingPrice)
GO



GO
CREATE VIEW [dbo].[ProductPriceView]
AS
SELECT A.*,
B.UnitName,B.UnitNameNepali,B.UnitSymbol,B.UnitSymbolNepali
FROM [dbo].[ProductPrice] AS A
LEFT JOIN [dbo].[Unit] AS B ON B.UnitId=A.UnitId
GO


GO
ALTER TABLE [dbo].[Product]
ADD ProductNameNepali nvarchar(400) not null
GO


GO
CREATE UNIQUE INDEX Product_ProductNameNepali_ui ON
[dbo].[Product](ProductName,ParentProductId)
GO


GO
CREATE VIEW [dbo].[ProductView]
AS
SELECT A.*,
B.ProductName AS ParentProductName,
B.ProductNameNepali AS ParentProductNameNepali
FROM [dbo].[Product] AS A
LEFT JOIN [dbo].[Product] AS B ON B.ProductId=A.ParentProductId
GO



GO
CREATE TABLE [dbo].[ProductImage]
(
	ImageId int not null Identity(1,1) Constraint ProductImage_ImageId_pk Primary Key,
	ProductId int not null Constraint ProductImage_Product_ProductId References Product(ProductId),
	Photo image not null,
	IsActive bit null default(1),
	IsPrimary bit null default(0)
);
GO




