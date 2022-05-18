
GO
CREATE VIEW [dbo].[MenuView]
AS
	SELECT A.*,B.MenuNameEnglish AS ParentMenuNameEnglish,B.MenuNameNepali AS ParentMenuNameNepali 
	FROM Menus AS A
	LEFT JOIN Menus AS B ON B.MenuId=A.ParentMenuId
GO

GO
CREATE OR ALTER PROC [dbo].[Proc_GetMenuAccessPermssionByUserId]
(

	@UserId int,
	@ParentMenuId int,
	@LangId int
)
AS
BEGIN
	IF(@LangId=1)
	BEGIN
		SELECT A.*,B.*,B.MenuNameEnglish AS MenuName FROM MenuAccessPermission AS A
		JOIN MenuView AS B ON B.MenuId=A.MenuId
		WHERE B.ParentMenuId=@ParentMenuId AND
		(A.ReadAccess=1 OR A.AdminAccess=1)

		ORDER BY B.MenuOrder
	END
	ELSE
	BEGIN
		SELECT A.*,B.*,B.MenuNameNepali AS MenuName FROM MenuAccessPermission AS A
		JOIN MenuView AS B ON B.MenuId=A.MenuId
		WHERE B.ParentMenuId=@ParentMenuId AND
		(A.ReadAccess=1 OR A.AdminAccess=1)

		ORDER BY B.MenuOrder
	END
END
GO


