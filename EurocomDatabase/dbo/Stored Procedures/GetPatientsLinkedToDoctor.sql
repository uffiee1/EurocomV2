CREATE PROCEDURE [dbo].[GetPatientsLinkedToDoctor]
	@username nvarchar(30)
AS
	SELECT uP.UserId, uP.Firstname, uP.Lastname
	FROM [User] AS uP
	INNER JOIN Patient
	ON uP.UserId = Patient.UserId
	INNER JOIN DoctorPatient
	ON Patient.PatientId = DoctorPatient.PatientId
	INNER JOIN Doctor
	ON DoctorPatient.DoctorId = Doctor.DoctorId
	INNER JOIN [User] AS uD
	ON Doctor.UserId = uD.UserId
	WHERE uD.Username = @username;