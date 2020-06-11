CREATE PROCEDURE [dbo].[GetUser]
	@username nvarchar(15),
	@password nvarchar(25)
AS
	SELECT Username
	FROM [User]
	WHERE Username = @username
	AND [Password] = @password;