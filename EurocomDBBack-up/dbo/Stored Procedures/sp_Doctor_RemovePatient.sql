CREATE PROCEDURE [dbo].[sp_Doctor_RemovePatient]
	@idD nvarchar(50),
	@idP nvarchar(50)
AS
	DELETE pdl
	FROM PatientDoctorLink AS pdl
	INNER JOIN AspNetUsers AS AspD
	ON pdl.DoctorId = AspD.Id
	INNER JOIN AspNetUsers AS AspP
	ON pdl.PatientId = AspP.Id
	WHERE AspD.Id = @idD
	AND AspP.Id = @idP;