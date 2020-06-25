CREATE PROCEDURE [dbo].[sp_Doctor_GetMeasurements]
	@idP nvarchar(50)
AS
	SELECT pm.[Date], pm.Measurement
	FROM PatientMeasurements AS pm
	INNER JOIN PatientMeasurementsLink AS pml
	ON pm.Id = pml.MeasurementId
	INNER JOIN AspNetUsers AS AspP
	ON pml.PatientId = AspP.Id
	WHERE AspP.Id = @idP;