
GO
UPDATE Member SET ReferenceId=1 WHERE MemberId=1
GO


GO
CREATE OR ALTER PROC [dbo].[FilterMember]
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
END
GO