CREATE PROCEDURE [dbo].[UpdateAgreement]
	@username nvarchar(15),
	@agreement bit
AS
	UPDATE [User]
	SET Agreement = @agreement
	WHERE Username = @username;