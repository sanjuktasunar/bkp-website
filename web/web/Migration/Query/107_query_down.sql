
GO
DROP VIEW [dbo].[MemberView]
GO

GO
DROP TABLE  [dbo].[BankDeposit]
GO


GO
DROP TABLE [dbo].[Address]
GO

GO
DROP TABLE [dbo].[MemberDetails]
GO


GO
DROP TABLE [dbo].[UserDocuments]
GO


GO
CREATE TABLE [dbo].[UserDocuments]
(
	UserDocumentId int not null Identity(1,1) Constraint UserDocuments_pk Primary Key,
	StaffId int null Constraint UserDocuments_Staffs_StaffId References Staffs(StaffId),
	CitizenshipFront nvarchar(max) null,
	CitizenshipBack nvarchar(max) null,
	PanCard nvarchar(max) null,
	EducationalDocument nvarchar(max) null
);
GO
GO
CREATE UNIQUE INDEX UserDocument_StaffId_ui ON
UserDocuments(StaffId) WHERE StaffId is not null
GO

GO
DROP TABLE [dbo].[Member]
GO

GO
DROP TABLE [dbo].[MemberType]
GO

GO
DELETE FROM [dbo].[UserType] WHERE UserTypeTitle=N'Member'
GO

GO
DROP TABLE [dbo].[MunicipalityType]
GO


GO
DROP TABLE [dbo].[AccountHead]
GO


GO
DROP TABLE [dbo].[MemberField]
GO

GO
DROP TABLE [dbo].[Occupation]
GO

GO
DROP TABLE [dbo].[Country]
GO




