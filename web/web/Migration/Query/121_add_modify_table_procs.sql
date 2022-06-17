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

--GO
--DROP VIEW [dbo].[AddressView]
--GO

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

--GO
--DROP VIEW [dbo].[MemberAddressView]
--GO

GO
CREATE VIEW [dbo].[MemberAddressView]
AS
SELECT        A.Id, A.MemberId, A.PermanentDistrictId, A.PermanentMunicipality, A.PermanentWardNumber, 
              A.TemporaryIsOutsideNepal, A.TemporaryCountryId, A.TemporaryProvinceId, A.TemporaryDistrictId, A.TemporaryMunicipality, 
              A.TemporaryWardNumber,A.TemporaryAddress,A.FormerDistrictId,A.FormerMunicipalityName,A.FormerWardNumber,
			  PP.ProvinceId AS PermanentProvinceId,PP.ProvinceName AS PermanentProvinceName, PD.DistrictName AS PermanentDistrictName, 
			  TP.ProvinceName AS TemporaryProvinceName,TD.DistrictName AS TemporaryDistrictName,TC.Name AS TemporaryCountryName, 
              FD.DistrictName AS FormerDistrictName        
						
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

GO
ALTER TABLE dbo.[Member]
ADD MaritalStatusId int null Constraint Member_MaritalStatusId_fk References dbo.[MaritalStatus](Id)
GO

GO
UPDATE [dbo].[Occupation]
SET Status=0 WHERE [Name]=N'Other'
GO

GO
ALTER TABLE [dbo].[UserDocuments]
ADD IsImageString bit not null default(0)
GO


GO
ALTER TABLE [dbo].[Member]
ADD AgentId int null Constraint Member_AgentId_fk References dbo.[Agent](AgentId)
GO

GO
ALTER TABLE dbo.[ShareTypes]
ADD IsPrimary bit not null default(0)
GO

GO
DROP VIEW [dbo].[ShareTypeView]
GO

GO
CREATE VIEW [dbo].[ShareTypeView]
AS
SELECT A.*
,B.FiscalYearName
FROM dbo.ShareTypes AS A
JOIN FiscalYear AS B ON B.FiscalYearId=A.FiscalYearId
GO

GO
CREATE TABLE dbo.[Shareholder]
(
	ShareholderId int not null Identity(1,1) Constraint Shareholder_pk Primary Key,
	MemberId int not null Constraint Shareholder_MemberId_fk References dbo.Member(MemberId),
	ShareTypeId int not null Constraint Shareholder_ShareTypeId References dbo.[ShareTypes](ShareTypeId),
	TotalKitta int not null,
	IsActive int not null,
	ApprovedDate datetime null
);
GO

GO
CREATE UNIQUE INDEX Shareholder_MemberId_ui ON
dbo.Shareholder(MemberId)
GO

GO
ALTER TABLE dbo.Member
ADD ShareholderId int null Constraint Member_ShareholderId_fk References dbo.Shareholder(ShareholderId)
GO

GO
DROP VIEW [dbo].[MemberDetailView]
GO

GO
CREATE VIEW [dbo].[MemberDetailView]
AS
SELECT	B.MemberId,B.FirstName, B.MiddleName, B.LastName, B.MobileNumber, B.Email, B.DateOfBirthBS, B.DateOfBirthAD, B.GenderId, B.OccupationId, B.OtherOccupationRemarks,B.MemberCode,B.ShareholderId,
		B.ShareTypeId,B.CitizenshipNumber, B.FormStatus, B.CreatedDate, B.CreatedBy, B.UpdatedDate, B.UpdatedBy, B.ApprovalStatus, B.ApprovedDate, B.ApprovedBy, B.ReferalCode, B.ReferenceId, B.IsActive, B.UserId,B.ApprovalRemarks, 
		A.PermanentProvinceId,A.PermanentProvinceName, A.PermanentDistrictId,A.PermanentDistrictName, A.PermanentMunicipality, A.PermanentWardNumber, B.MaritalStatusId,
        A.TemporaryIsOutsideNepal, A.TemporaryCountryId,A.TemporaryCountryName,A.TemporaryAddress, A.TemporaryProvinceId, A.TemporaryProvinceName,A.TemporaryDistrictId, A.TemporaryDistrictName,A.TemporaryMunicipality,
        A.TemporaryWardNumber,A.FormerDistrictId,A.FormerDistrictName,A.FormerMunicipalityName,A.FormerWardNumber,
		O.[Name] AS OcuupationName,
		G.GenderName,H.ShareTypeName,H.RegistrationAmount,
		(case when isnull(B.ReferenceId,0)=0 then AG.LicenceNumber else B1.ReferalCode end)  as ReferenceReferalCode ,
		(case when isnull(b.ReferenceId,0)=0 then AG.AgentFullName else (B.FirstName+ (case when isnull(B.MiddleName,'')= N'' then '' else B.MiddleName end) + ' '+B.LastName) end)  as ReferenceFullName,
		BD.Amount,s.TotalKitta,AH.AccountHeadName,s.IsActive as ShareholderIsActive,s.ApprovedDate as ShareholderDate,
		ud.CitizenshipFront,ud.CitizenshipBack,ud.Photo,BD.VoucherImage,ms.MaritalStatusName
FROM            dbo.Member AS B LEFT OUTER JOIN
                dbo.[MemberAddressView] AS A ON A.MemberId = B.MemberId LEFT OUTER JOIN
                dbo.Member AS B1 ON B.ReferenceId = B1.MemberId LEFT OUTER JOIN
                dbo.Occupation AS O ON O.Id = B.OccupationId LEFT OUTER JOIN
				dbo.Gender AS G ON G.GenderId = B.GenderId LEFT OUTER JOIN
                dbo.ShareTypes AS H ON H.ShareTypeId = B.ShareTypeId LEFT OUTER JOIN
				dbo.Agent AS AG on AG.AgentId=B.AgentId LEFT OUTER JOIN
				dbo.[Shareholder] s on s.ShareholderId=B.ShareholderId LEFT OUTER JOIN
				dbo.BankDeposit BD on BD.MemberId=B.MemberId LEFT OUTER JOIN
				dbo.[AccountHead] AH on AH.AccountHeadId=BD.AccountHeadId LEFT OUTER JOIN
				dbo.[UserDocuments] ud on ud.MemberId=B.MemberId LEFT OUTER JOIN
				dbo.[MaritalStatus] ms on ms.Id=B.MaritalStatusId
GO

GO
DROP PROC [dbo].[FilterMember]
GO

GO
CREATE PROC [dbo].[FilterMember]
(
	@ApprovalStatus int,
	@FormStatus int,
	@ReferenceId int,
	@AgentId int,
	@ShareTypeId int
)
AS
BEGIN
	/*
		exec [dbo].[FilterMember] 2,2,1,0,0
	*/

	SELECT m.MemberId,(m.FirstName+case when isnull(m.MiddleName,'')!='' then (' '+m.MiddleName) else '' end+' '+m.LastName) AS FullName,
	m.MobileNumber,m.Email,m.CreatedDate,m.ApprovedDate,m.FormStatus,m.ApprovalStatus,m.ReferalCode,m.ApprovalRemarks,
	m.IsActive,m.CitizenshipNumber,m.ShareholderId,m.ShareTypeId,
	(case when isnull(m.ReferenceId,0)=0 then a.AgentFullName else (m.FirstName+ (case when isnull(m.MiddleName,'')= N'' then '' else ' '+m.MiddleName end) + ' '+m.LastName) end)  as ReferenceFullName
	,st.ShareTypeName,p.AppliedKitta
	FROM Member m
	left join [dbo].[PratigyaPatraFormFillups] p on p.MemberId=m.MemberId
	left join dbo.ShareTypes st on st.ShareTypeId=m.ShareTypeId
	left join [dbo].[Member] m1 on m1.MemberId=m.ReferenceId
	left join [dbo].[Agent] a on a.AgentId=m.AgentId
	WHERE
	(
		(isnull(@ApprovalStatus,0)=0 OR m.ApprovalStatus=@ApprovalStatus)
		AND
		(isnull(@ReferenceId,0)=0 OR m.ReferenceId=@ReferenceId)
		AND
		(isnull(@AgentId,0)=0 OR m.AgentId=@AgentId)
		AND
		(isnull(@FormStatus,0)=0 OR m.FormStatus=@FormStatus)
		AND
		isnull(m.ShareholderId,0)=0
		AND
		(isnull(@ShareTypeId,0)=0 OR m.ShareTypeId=@ShareTypeId)
	)
	ORDER BY m.FormStatus DESC,m.CreatedDate DESC
END
GO

GO
DROP PROC [dbo].[Sp_GetReferenceMembers]
GO

GO
CREATE PROC [dbo].[Sp_GetReferenceMembers]
AS
BEGIN
create table #temp
(
	Id int Identity(1,1),
	ReferenceID int not null
)
INSERT INTO #temp(ReferenceID)
SELECT DISTINCT ReferenceId FROM dbo.Member WHERE ReferenceId IS NOT NULL
CREATE TABLE #refMem
(
	ID int Identity(1,1),
	MemberId int,
	ReferenceName nvarchar(400) 
)
insert into #refMem
(
	MemberId,ReferenceName
)
SELECT b.MemberId,TRIM(b.FirstName)+case when b.MiddleName is null then '' else ' '+TRIM(b.MiddleName) end+' '+TRIM(b.LastName)+'('+b.MobileNumber+')'
	as ReferenceName
FROM Member as b
join #temp as a on a.ReferenceID=b.MemberId
select MemberId AS [Key],ReferenceName AS [Value] from #refMem
END
GO

GO
CREATE PROC [dbo].[Sp_GetReferenceAgents]
AS
BEGIN
create table #temp
(
	Id int Identity(1,1),
	AgentID int not null
)
INSERT INTO #temp(AgentID)
SELECT DISTINCT AgentId FROM dbo.Member WHERE AgentId IS NOT NULL

CREATE TABLE #refAgent
(
	ID int Identity(1,1),
	AgentId int,
	AgentName nvarchar(400) 
)
insert into #refAgent(AgentId,AgentName)
SELECT a.AgentId,b.AgentFullName
FROM #temp as a
join dbo.[Agent] as b on a.AgentID=b.AgentId

select AgentId AS [Key],AgentName AS [Value] from #refAgent
END
GO

GO
declare @ShareTypeId int
select @ShareTypeId=(select top 1 ShareTypeId from dbo.ShareTypes 
				where lower(ShareTypeName)=N'right share' or lower(ShareTypeName)=N'rightshare')

UPDATE dbo.[Member]
SET ShareTypeId=@ShareTypeId
WHERE ApprovalStatus=2
GO

GO
CREATE PROC [dbo].[FilterShareholder]
(
	@ReferenceId int,
	@AgentId int,
	@ShareTypeId int,
	@SearchQuery nvarchar(50),
	@Code nvarchar(50)
)
AS
BEGIN
	/*
		exec [dbo].[FilterShareholder] 0,0,0,'',''
	*/

	SELECT s.ShareholderId,s.TotalKitta,s.ApprovedDate,s.IsActive,
	m.MemberId,(m.FirstName+case when isnull(m.MiddleName,'')!='' then (' '+m.MiddleName) else '' end+' '+m.LastName) AS FullName,
	m.MobileNumber,m.Email,m.CreatedDate,m.ReferalCode,m.MemberCode,
	m.CitizenshipNumber,
	(case when isnull(m.ReferenceId,0)=0 then a.AgentFullName else (m1.FirstName+ (case when isnull(m1.MiddleName,'')= N'' then '' else ' '+m1.MiddleName end) + ' '+m1.LastName) end)  as ReferenceFullName
	,st.ShareTypeName
	FROM dbo.[Shareholder] s
	inner join dbo.[Member] m on s.ShareholderId=m.ShareholderId
	inner join dbo.[ShareTypes] st on st.ShareTypeId=s.ShareTypeId
	left join [dbo].[Member] m1 on m1.MemberId=m.ReferenceId
	left join [dbo].[Agent] a on a.AgentId=m.AgentId
	
	WHERE
	(
		(isnull(@ReferenceId,0)=0 OR m.ReferenceId=@ReferenceId)
		AND
		(isnull(@AgentId,0)=0 OR m.AgentId=@AgentId)
		AND
		(isnull(@ShareTypeId,0)=0 OR m.ShareTypeId=@ShareTypeId)
		AND
		(
			((isnull(@SearchQuery,'')='') OR
			 (m.FirstName like N'%'+@SearchQuery+'%') OR
			 (m.MiddleName like N'%'+@SearchQuery+'%') OR
			 (m.LastName like N'%'+@SearchQuery+'%') OR
			 (m.Email like N'%'+@SearchQuery+'%') OR
			 (m.MobileNumber like N'%'+@SearchQuery+'%') OR
			 (m.CitizenshipNumber like N'%'+@SearchQuery+'%')
			)
		)
		AND
		(
			((isnull(@Code,'')='') OR
			 (m.MemberCode like N'%'+@Code+'%') OR
			 (m.ReferalCode like N'%'+@Code+'%')
			)
		)
	)
	ORDER BY s.ApprovedDate desc
END
GO

GO
UPDATE dbo.Member SET
FirstName=N'Sanjukta',MiddleName=N'',LastName=N'Sunar',CreatedDate=N'2021-03-20',
MobileNumber=N'9857089889',Email=N'info@bkpnepal.com'
WHERE MemberId=1
GO