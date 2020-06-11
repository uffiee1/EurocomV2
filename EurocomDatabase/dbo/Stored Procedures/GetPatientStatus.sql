CREATE PROCEDURE [dbo].[GetPatientStatus]
	@username nvarchar(30),
	@userId int
AS
	SELECT [Status].[Date], [Status].INR
	FROM [Status]
	INNER JOIN PatientStatus
	ON [Status].StatusId = PatientStatus.StatusId
	INNER JOIN Patient
	ON PatientStatus.PatientId = Patient.PatientId
	INNER JOIN [User] AS uP
	ON Patient.UserId = uP.UserId
	INNER JOIN DoctorPatient
	ON Patient.PatientId = DoctorPatient.PatientId
	INNER JOIN Doctor
	ON DoctorPatient.DoctorId = Doctor.DoctorId
	INNER JOIN [User] AS uD
	ON Doctor.UserId = uD.UserId
	WHERE uD.Username = @username
	AND uP.UserId = @userId;