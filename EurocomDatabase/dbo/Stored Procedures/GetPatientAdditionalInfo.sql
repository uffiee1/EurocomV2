CREATE PROCEDURE [dbo].[GetPatientAdditionalInfo]
	@userId int
AS
	SELECT uP.Firstname, uP.Lastname, uP.PhoneNumber, uP.Email, Patient.DateOfBirth, Patient.Age
	FROM [User] AS uP
	INNER JOIN Patient
	ON uP.UserId = Patient.UserId
	WHERE uP.UserId = @userId;