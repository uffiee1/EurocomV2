CREATE PROCEDURE [dbo].[sp_Doctor_AssignPatient]
	@idD nvarchar(50),
	@idP nvarchar(50)
AS
	INSERT INTO PatientDoctorLink(patientId, doctorId)
	VALUES(@idD, @idP);