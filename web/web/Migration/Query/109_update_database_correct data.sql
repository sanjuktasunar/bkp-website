
GO
UPDATE [dbo].[MemberField] SET Name=N'Business Management and Grow',NepaliName=N'Business Management and Grow'
WHERE TRIM(Name)=N'Capital Increase'
GO

GO
UPDATE [dbo].[MemberField] SET Name=N'Administration And Accounting',NepaliName=N'Administration And Accounting'
WHERE TRIM(Name)=N'Marketing'
GO

GO
INSERT INTO [dbo].[MemberField](Name,NepaliName,Status)
SELECT N'Capital Increase',N'Capital Increase',1 UNION ALL
SELECT N'Project Increase',N'Project Increae',1
GO

GO
UPDATE [dbo].[Occupation] SET Name=N'Nurse',NepaliName=N'Nurse' WHERE LOWER(Name)=N'other'
GO

GO
INSERT INTO [dbo].[Occupation](Name,NepaliName,Status)
SELECT N'Housewife',N'Housewife',1 UNION ALL
SELECT N'Government Job',N'Government Job',1 UNION ALL
SELECT N'Teacher',N'Teacher',1 UNION ALL
SELECT N'Other',N'Other',1
GO


GO
INSERT INTO [dbo].[Country](Name,NepaliName,Status,IsOutsideNepal)
SELECT N'Germany',N'Germany',1,1 UNION ALL
SELECT N'Australia',N'Australia',1,1 
GO


GO
UPDATE [dbo].[Member] SET IsActive=1 WHERE MemberId=1
GO
