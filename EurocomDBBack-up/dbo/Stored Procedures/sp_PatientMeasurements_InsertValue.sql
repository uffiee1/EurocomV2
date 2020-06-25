CREATE PROCEDURE [dbo].[sp_PatientMeasurements_InsertValue]
	@Date DATE,
	@Measurement INT,
	@MeasurementSucceeded BIT 
AS
		BEGIN 
			INSERT INTO [dbo].[PatientMeasurements] ([Date], [Measurement], [MeasurementSucceeded])
			VALUES (@Date, @Measurement, @MeasurementSucceeded)
		END 

RETURN 0