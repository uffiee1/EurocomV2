CREATE PROCEDURE [dbo].[sp_Doctor_GetPatientInfo]
	@idP nvarchar(50)
AS
	SELECT FirstName, LastName, PhoneNumber, Email, DateOfBirth
	FROM AspNetUsers
	WHERE Id = @idP;