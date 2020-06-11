CREATE PROCEDURE [dbo].[UpdatePhonenumberPatient]
	@phonenumber int,
	@username nvarchar(30)
AS
	UPDATE [User]
	SET PhoneNumber = @phonenumber
	WHERE Username = @username;