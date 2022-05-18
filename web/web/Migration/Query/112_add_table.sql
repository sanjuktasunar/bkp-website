
GO
CREATE TABLE [dbo].[FiscalYear]
(
	FiscalYearId int not null Identity(1,1) Constraint FiscalYear_pk Primary Key,
	FiscalYearName nvarchar(200) not null,
	StartDateAD datetime not null,
	StartDateBS nvarchar(10) not null,
	EndDateAD datetime not null,
	EndDateBS nvarchar(10) not null,
	Status bit null default(1),
	IsCurrent bit not null default(0)
);
GO
GO
CREATE UNIQUE INDEX FiscalYear_FiscalYearName_ui ON
FiscalYear(FiscalYearName)
GO


GO
CREATE TABLE [dbo].[ShareTypes]
(
	ShareTypeId int not null Identity(1,1) Constraint ShareTypes_pk Primary Key,
	ShareTypeName nvarchar(200) not null,
	FiscalYearId int not null Constraint ShareTypes_FiscalYear_fk References FiscalYear(FiscalYearId),
	NumberOfIssuedShares bigint not null,
	MaxSharePerPerson int null,
	MinSharePerPerson int null,
	Status bit null default(1)
);
GO
CREATE UNIQUE INDEX ShareTypes_ShareTypeName_ui ON
ShareTypes(ShareTypeName)
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
ALTER PROC [dbo].[FilterMember]
(
	@ApprovalStatus int,
	@FormStatus int,
	@ReferenceId int
)
AS
BEGIN
	SELECT * 
	FROM MemberView
	WHERE
	(
		(@ApprovalStatus IS NULL OR ApprovalStatus=@ApprovalStatus)
		AND
		(@ReferenceId IS NULL OR ReferenceId=@ReferenceId)
		AND
		(@FormStatus IS NULL OR FormStatus=@FormStatus)
	)
	ORDER BY FormStatus DESC,CreatedDate DESC
END
GO



