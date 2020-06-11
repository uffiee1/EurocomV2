CREATE PROCEDURE [dbo].[sp_PatientMeasurements_InsertValue]
	@Id INT,
	@Date DATE,
	@Measurement INT,
	@MeasurementSucceeded BIT 
AS
	IF NOT EXISTS(SELECT [Id]
		          FROM [dbo].[PatientMeasurements] 
			      WHERE [Id] = @Id)
		BEGIN 
			INSERT INTO [dbo].[PatientMeasurements] ([Id], [Date], [Measurement], [MeasurementSucceeded])
			VALUES (@Id, @Date, @Measurement, @MeasurementSucceeded)
		END 

	ELSE 
		BEGIN
			RAISERROR('The measurement already exist.', 16, 1)
		END 
   
RETURN 0