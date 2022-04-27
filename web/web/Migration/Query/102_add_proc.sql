
GO
DROP VIEW [dbo].[MenuAccessPermissionView]
GO

GO
CREATE VIEW [dbo].[MenuAccessPermissionView]
AS
SELECT A.*,B.MenuNameEnglish,B.MenuNameNepali,B.CheckMenuName,B.MenuIcon,
B.MenuLink,B.MenuOrder,B.ParentMenuId,B.ParentMenuNameEnglish,B.ParentMenuNameNepali,
C.UserId,C.UserName,C.UserTypeId,C.UserStatusId
FROM MenuView AS B 
LEFT JOIN  MenuAccessPermission AS A ON A.MenuId=B.MenuId 
LEFT JOIN StaffsView AS C ON C.StaffId=A.StaffId
WHERE B.ParentMenuId IS NOT NULL
GO


GO
CREATE OR ALTER PROC [dbo].[MenuAccessPermissionForStaff]
(
	@StaffId int
)
AS
BEGIN
	SELECT A.*,
	B.ReadAccess,B.WriteAccess,B.AdminAccess,B.ModifyAccess,B.DeleteAccess,
	B.StaffId,B.MenuAccessPermissionId
	FROM MenuView AS A
	LEFT JOIN MenuAccessPermissionView AS B 
	ON B.MenuId=A.MenuId AND B.StaffId=@StaffId
	WHERE A.ParentMenuId IS NOT NULL

END
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
		SELECT A.*,B.*,B.MenuNameEnglish AS MenuName 
		FROM MenuAccessPermission AS A
		JOIN MenuView AS B ON B.MenuId=A.MenuId
		JOIN StaffsView AS C ON C.StaffId=A.StaffId
		WHERE B.ParentMenuId=@ParentMenuId AND
		(A.ReadAccess=1 OR A.AdminAccess=1) 
		AND C.UserId=@UserId
		ORDER BY B.MenuOrder
	END
	ELSE
	BEGIN
		SELECT A.*,B.*,B.MenuNameNepali AS MenuName 
		FROM MenuAccessPermission AS A
		JOIN MenuView AS B ON B.MenuId=A.MenuId
		JOIN StaffsView AS C ON C.StaffId=A.StaffId
		WHERE B.ParentMenuId=@ParentMenuId AND
		(A.ReadAccess=1 OR A.AdminAccess=1) 
		AND C.UserId=@UserId
		ORDER BY B.MenuOrder
	END
END
GO
