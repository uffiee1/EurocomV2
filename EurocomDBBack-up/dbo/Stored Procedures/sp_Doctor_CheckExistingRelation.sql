CREATE PROCEDURE [dbo].[sp_Doctor_CheckExistingRelation]
	@idD nvarchar(50),
	@idP nvarchar(50)
AS
	SELECT PatientDoctorLink.LinkId
	FROM PatientDoctorLink
	INNER JOIN AspNetUsers AS AspD
	ON PatientDoctorLink.DoctorId = AspD.Id
	INNER JOIN AspNetUsers AS AspP
	ON PatientDoctorLink.PatientId = AspP.Id
	WHERE AspD.Id = @idD
	AND AspP.Id = @idP;