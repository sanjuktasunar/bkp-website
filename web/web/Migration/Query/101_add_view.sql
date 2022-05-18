

GO
CREATE VIEW [dbo].[StaffsView]
AS
SELECT A.StaffId,A.UserId,A.RoleId,A.DesignationId,A.DepartmentId,A.StaffName,
A.GenderId,A.TemporaryAddress,A.PermanentAddress,A.CitizenshipNumber,A.PanNumber,
A.BasicSalary,
B.UserName,B.UserTypeId,B.PhotoStorageId,B.ContactNumber,
B.EmailAddress,B.UserStatusId,
US.StatusName,
C.Photo,C.PhotoLocation,
R.RoleName,R.Status AS RoleStatus,D.DesignationName,D.Status AS DesignationStatus,
DP.DepartmentName,DP.Status AS DepartmentStatus,
G.GenderName,G.Status AS GenderStatus
FROM Staffs AS A
JOIN Users AS B ON B.UserId=A.UserId
JOIN UserStatus AS US ON US.StatusId=B.UserStatusId
JOIN PhotoStorages AS C ON C.PhotoStorageId=B.PhotoStorageId
LEFT JOIN Role AS R ON R.RoleId=A.RoleId
LEFT JOIN Designation AS D ON D.DesignationId=A.DesignationId
LEFT JOIN Department AS DP ON DP.DepartmentId=A.DepartmentId
LEFT JOIN Gender AS G ON G.GenderId=A.GenderId
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
GO



