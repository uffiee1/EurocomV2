CREATE PROCEDURE [dbo].[GetAgreement]
	@username nvarchar(15)
AS
	SELECT Agreement
	FROM [User]
	WHERE Username = @username;