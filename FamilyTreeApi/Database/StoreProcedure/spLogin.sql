CREATE   PROCEDURE [dbo].[spLogin]
@Flag char = null,
@UserId int = null,
@UserName nvarchar(100) =null,
@Password nvarchar(max) =null,
@Email nvarchar(200) = null
AS
BEGIN
	If @Flag='G'
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM tblUsers WHERE UserName = @UserName)
		BEGIN
			;THROW 51003, 'Invalid username', 0;
		END
		ELSE IF NOT EXISTS(SELECT 1 FROM tblUsers WHERE  Password = @Password)
		BEGIN
			;THROW 51004, 'Invalid password', 0;
		END
		ELSE
		BEGIN
			SELECT 1 as Success;
		END
	END
END
GO