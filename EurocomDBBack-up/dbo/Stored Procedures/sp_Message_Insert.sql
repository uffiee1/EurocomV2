CREATE PROCEDURE [dbo].[sp_Message_Insert]
	@Message NVARCHAR(125)
AS
	SET NOCOUNT ON;
	INSERT INTO [dbo].[Messsage]([Message])
	VALUES(@Message);