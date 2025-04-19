
ALTER   PROCEDURE [dbo].[spLogin]
@Flag char = null,
@UserId int = null,
@UserName nvarchar(100) =null,
@Password nvarchar(max) =null,
@Email nvarchar(200) = null
AS
BEGIN
	If @Flag='G'
	BEGIN
		select UserId,UserName,Password,Email,u.RoleId,r.RoleName from tblusers u with(nolock) 
		join Auth.tblRoles r with(nolock) on r.RoleId = u.RoleId
		where UPPER(Username)=UPPER(@UserName);
	END
END


