CREATE PROCEDURE [dbo].[PatientOverview]
	@username nvarchar(15)
AS
	SELECT uP.Firstname, uP.Lastname, uP.PhoneNumber, uD.Lastname, uD.PhoneNumber
	FROM Doctor
	INNER JOIN [User] AS uD
	ON Doctor.UserId = uD.UserId
	INNER JOIN DoctorPatient
	ON Doctor.DoctorId = DoctorPatient.DoctorId
	INNER JOIN Patient
	ON DoctorPatient.PatientId = Patient.PatientId
	INNER JOIN [User] AS uP
	ON Patient.UserId = uP.UserId
	WHERE uP.Username = @username;