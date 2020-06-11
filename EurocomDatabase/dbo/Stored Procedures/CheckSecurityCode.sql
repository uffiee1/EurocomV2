CREATE PROCEDURE [dbo].[CheckSecurityCode]
	@securityCode nvarchar(20)
AS
	SELECT Patient.UserId, Patient.SecurityCode
	FROM Patient
	INNER JOIN [User] AS uP
	ON Patient.UserId = uP.UserId
	WHERE Patient.SecurityCode = @securityCode;