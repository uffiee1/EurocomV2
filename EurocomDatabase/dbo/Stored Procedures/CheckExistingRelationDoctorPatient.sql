CREATE PROCEDURE [dbo].[CheckExistingRelationDoctorPatient]
	@username nvarchar(15),
	@userId int
AS
	SELECT DoctorPatient.DoctorPatientId, DoctorPatient.DoctorId, DoctorPatient.PatientId
	FROM DoctorPatient
	INNER JOIN Doctor
	ON DoctorPatient.DoctorId = Doctor.DoctorId
	INNER JOIN [User] AS uD
	ON Doctor.UserId = uD.UserId
	INNER JOIN Patient
	ON DoctorPatient.PatientId = Patient.PatientId
	INNER JOIN [User] AS uP
	ON Patient.UserId = uP.UserId
	WHERE uD.Username = @username
	AND uP.UserId = @userId;