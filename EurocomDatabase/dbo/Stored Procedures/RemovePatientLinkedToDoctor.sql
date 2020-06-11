CREATE PROCEDURE [dbo].[RemovePatientLinkedToDoctor]
	@username nvarchar(30),
	@userId int
AS
	DELETE dp
	FROM DoctorPatient AS dp
	INNER JOIN Doctor
	ON dp.DoctorId = Doctor.DoctorId
	INNER JOIN [User] AS uD
	ON Doctor.UserId = uD.UserId
	INNER JOIN Patient
	ON dp.PatientId = Patient.PatientId
	INNER JOIN [User] AS uP
	ON Patient.UserId = uP.UserId
	WHERE uD.Username = @username
	AND uP.UserId = @userId;