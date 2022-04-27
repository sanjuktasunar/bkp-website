
GO
SET IDENTITY_INSERT [dbo].[MemberType] ON
GO
GO
INSERT INTO [dbo].[MemberType](Id,Name,NepaliName,Status)
SELECT 1,N'Candidate',N'उम्मेदवार',1 UNION ALL
SELECT 2,N'Member',N'Member',1
GO
GO
SET IDENTITY_INSERT [dbo].[MemberType] OFF
GO

GO
INSERT INTO [dbo].[MunicipalityType](Name,NepaliName,Status)
SELECT N'Rular Municipality',N'गाउँपालिका',1 UNION ALL
SELECT N'Municipality',N'नगरपालिका',1 UNION ALL
SELECT N'Sub-metropolis',N'उपमहानगरपालिका',1 UNION ALL
SELECT N'Metropolitan',N'महानगरपालिका',1 
GO


GO
SET IDENTITY_INSERT [dbo].[UserType] ON
GO

GO
INSERT INTO [dbo].[UserType](UserTypeId,UserTypeTitle,Status)
SELECT 3,N'Member',1 
GO

GO
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO


GO
SET IDENTITY_INSERT [dbo].[Occupation] ON
GO

GO
INSERT INTO [dbo].[Occupation](Id,Name,NepaliName,Status)
SELECT 1,N'Doctor',N'Doctor',1 UNION ALL
SELECT 2,N'Engineer',N'Engineer',1 UNION ALL
SELECT 3,N'Businessman',N'Businessman',1 UNION ALL
SELECT 4,N'Other',N'Other',1
GO
GO
SET IDENTITY_INSERT [dbo].[Occupation] OFF
GO

GO
INSERT INTO [dbo].[MemberField](Name,NepaliName,Status)
SELECT N'Capital Increase',N'Capital Increase',1 UNION ALL
SELECT N'Marketing',N'Marketing',1
GO


GO
SET IDENTITY_INSERT [dbo].[Country] ON
GO

GO
INSERT INTO [dbo].[Country](Id,Name,NepaliName,Status,IsOutsideNepal)
SELECT 1,N'Nepal',N'Nepal',1,0 UNION ALL
SELECT 2,N'India',N'India',1,1 UNION ALL
SELECT 3,N'China',N'China',1,1 UNION All
SELECT 4,N'USA',N'USA',1,1
GO

GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO



GO
SET IDENTITY_INSERT [dbo].[Member] ON
GO
GO
INSERT INTO [dbo].[Member](MemberId,MemberCode,FirstName,LastName,Email,MobileNumber,
ReferalCode,FormStatus,ApprovalStatus,GenderId,DateOfBirthBS,DateOfBirthAD,OccupationId,MemberFieldId,
CitizenshipNumber,CreatedBy)
VALUES(1,N'BKP-2021-78',N'Bishwokarma Trading and Promotors Pvt.ltd',N'',N'bishwokarmatrading73@gmail.com',N'9857039526',
N'REF-617450-887',2,2,1,N'2078-03-03',N'2021-6-17',1,1,N'1234/9999',1)
GO

GO
SET IDENTITY_INSERT [dbo].[Member] OFF
GO