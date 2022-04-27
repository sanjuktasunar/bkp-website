GO
ALTER TABLE [dbo].[Address]
ADD FormerDistrictId int null Constraint Address_FormerDistrictId_fk References dbo.[District](DistrictId)
GO

GO
ALTER TABLE [dbo].[Address]
ADD FormerMunicipalityName nvarchar(200) null
GO

GO
ALTER TABLE [dbo].[Address]
ADD FormerWardNumber nvarchar(4) null
GO

GO
DROP VIEW [dbo].[AddressView]
GO

GO
CREATE VIEW [dbo].[AddressView]
AS
SELECT        A.Id, A.MemberId, A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, 
                         A.TemporaryWardNumber, A.TemporaryToleName, A.TemporaryAddress, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName,
						 --PM.Name AS PermanentMunicipalityName, TM.Name AS TemporaryMunicipalityName,
						 PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName, 
                         --M.FirstName, M.MiddleName, M.LastName, G.GenderName, M.ReferalCode, M.MobileNumber, M.Email, M.GenderId, M.MemberCode, M.IsActive, M.UserId, M.ApprovalStatus, M.CreatedDate, M.ApprovedDate,M.ReferenceId
						 FD.DistrictName AS FormerDistrictName,
			A.FormerDistrictId,A.FormerMunicipalityName,A.FormerWardNumber

FROM        dbo.[Address] AS A LEFT JOIN
            dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT JOIN
			dbo.Province AS PP ON PP.ProvinceId = PD.ProvinceId LEFT JOIN
            dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT JOIN
			dbo.Province AS TP ON TP.ProvinceId = TD.ProvinceId LEFT JOIN

			dbo.District AS FD ON FD.DistrictId = A.FormerDistrictId LEFT JOIN

            --dbo.MunicipalityType AS PM ON PM.Id = A.PermanentMunicipalityTypeId LEFT JOIN
            --dbo.MunicipalityType AS TM ON TM.Id = A.TemporaryMunicipalityTypeId LEFT JOIN
            dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT JOIN
            dbo.Country AS TC ON TC.Id = A.TemporaryCountryId
   --         dbo.Member AS M ON M.MemberId = A.MemberId LEFT JOIN
			--dbo.Gender AS G ON G.GenderId=M.GenderId
GO


GO
CREATE VIEW [dbo].[MemberAddressView]
AS
SELECT        A.Id, A.MemberId, A.PermanentIsOutsideNepal, A.PermanentCountryId, A.PermanentProvinceId, A.PermanentDistrictId, A.PermanentMunicipalityTypeId, A.PermanentMunicipality, A.PermanentWardNumber, 
                         A.PermanentToleName, A.PermanentAddress, A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipalityTypeId, A.TemporaryMunicipality, 
                         A.TemporaryWardNumber, A.TemporaryToleName, A.TemporaryAddress, PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, TP.ProvinceName AS TemporaryProvinceName, 
                         TD.DistrictName AS TemporaryDistrictName,
						 PC.Name AS PermanentCountryName, TC.Name AS TemporaryCountryName, 
						 FD.DistrictName AS FormerDistrictName,
			A.FormerDistrictId,A.FormerMunicipalityName,A.FormerWardNumber

FROM        dbo.[Address] AS A LEFT JOIN
            dbo.District AS PD ON PD.DistrictId = A.PermanentDistrictId LEFT JOIN
			dbo.Province AS PP ON PP.ProvinceId = PD.ProvinceId LEFT JOIN
            dbo.District AS TD ON TD.DistrictId = A.TemporaryDistrictId LEFT JOIN
			dbo.Province AS TP ON TP.ProvinceId = TD.ProvinceId LEFT JOIN
			dbo.District AS FD ON FD.DistrictId = A.FormerDistrictId LEFT JOIN
            dbo.Country AS PC ON PC.Id = A.PermanentCountryId LEFT JOIN
            dbo.Country AS TC ON TC.Id = A.TemporaryCountryId
GO

GO
CREATE TABLE [dbo].[MaritalStatus]
(
	Id int not null Identity(1,1) Constraint MaritalStatus_pk Primary Key,
	MaritalStatusName nvarchar(100) not null,
	OrderId int not null,
	IsActive bit not null
);
GO

GO
SET IDENTITY_INSERT [dbo].[MaritalStatus] ON
GO

GO
INSERT INTO dbo.[MaritalStatus](Id,MaritalStatusName,OrderId,IsActive)
SELECT 1,N'Single',1,1 UNION ALL
SELECT 2,N'Married',2,1 UNION ALL
SELECT 3,N'Other',3,1
GO

GO
SET IDENTITY_INSERT [dbo].[MaritalStatus] OFF
GO
