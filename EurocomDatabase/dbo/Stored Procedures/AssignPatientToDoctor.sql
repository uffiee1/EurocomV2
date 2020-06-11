CREATE PROCEDURE [dbo].[AssignPatientToDoctor]
	@username nvarchar(30),
	@userId int
AS
	DECLARE @doctorId int
	SET @doctorId = (SELECT Doctor.DoctorId
				 FROM Doctor
				 INNER JOIN [User] AS uD
				 ON Doctor.UserId = uD.UserId
				 WHERE uD.Username = @username)
	DECLARE @patientId int
	SET @patientId = (SELECT Patient.PatientId
				  FROM Patient
				  INNER JOIN [User] AS uP
				  ON Patient.UserId = uP.UserId
				  WHERE uP.UserId = @userId)
	INSERT INTO DoctorPatient (DoctorId, PatientId)
	VALUES (@doctorId, @patientId);