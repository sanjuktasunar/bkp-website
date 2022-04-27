
GO
CREATE TABLE [dbo].[PratigyaPatraFormFillups]
(
	Id int not null Identity(1,1) Constraint PratigyaPatraFormFillups_pk Primary Key,
	MemberId int not null Constraint PPFUPS_Member_MemberId_fk References Member(MemberId),
	AppliedKitta int not null,
	IsFillup bit null default(0),
);
GO

GO
CREATE UNIQUE INDEX PPUPS_MEMBERID_UI ON
[dbo].[PratigyaPatraFormFillups](MemberId)
GO


GO
CREATE PROC [dbo].[Sp_MemberPratigyaPatra]
AS
BEGIN
SELECT * FROM [dbo].[MemberView] AS A
LEFT JOIN [dbo].[PratigyaPatraFormFillups] AS B ON B.MemberId=A.MemberId
WHERE 
	ReferalCode IS NOT NULL
	AND UserId IS NOT NULL
	AND IsActive=1

	ORDER BY B.IsFillup
END
GO

