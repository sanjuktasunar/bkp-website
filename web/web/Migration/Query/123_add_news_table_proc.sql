GO
create table dbo.[News]
(
	NewsId int not null Identity(1,1) Constraint News_pk Primary Key,
	LanguageId int not null Constraint News_LanguageId_fk References dbo.[Language](LanguageId),
	Title nvarchar(120) not null,
	[Description] nvarchar(max) not null,
	FrontImage nvarchar(200) null,
	IsPublish bit not null,
	IsDeleted bit not null,
	CreatedBy int null Constraint News_CreatedBy_fk References dbo.[Users](UserId),
	CreatedDate datetime null,
	UpdatedBy int null Constraint News_UpdatedBy_fk References dbo.[Users](UserId),
	UpdatedDate datetime null
);