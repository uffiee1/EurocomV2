CREATE PROCEDURE [dbo].[GetPatientNames]
AS
	SELECT uP.UserId, uP.Firstname, uP.Lastname
	FROM [User] AS uP
	INNER JOIN Patient
	ON Patient.UserId = uP.UserId;