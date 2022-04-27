

GO
CREATE OR ALTER VIEW [dbo].[MenuAccessPermissionView]
AS
SELECT A.*,B.MenuNameEnglish,B.MenuNameNepali,B.CheckMenuName,B.MenuIcon,
B.MenuLink,B.MenuOrder,B.ParentMenuId,B.ParentMenuNameEnglish,B.ParentMenuNameNepali,
C.UserId,C.UserName,C.UserTypeId,C.UserStatusId
FROM MenuView AS B 
LEFT JOIN  MenuAccessPermission AS A ON A.MenuId=B.MenuId
LEFT JOIN StaffsView AS C ON C.StaffId=A.StaffId
GO


GO
ALTER TABLE [dbo].[Users]
DROP COLUMN RoleId
GO

GO
ALTER TABLE [dbo].[Member]
DROP COLUMN IsActive
GO


GO
DROP VIEW [dbo].[AddressView]
GO


GO
ALTER TABLE [dbo].[Member]
DROP COLUMN IsActive
GO



