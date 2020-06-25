CREATE PROCEDURE [dbo].[sp_BoundaryValues_InsertValue]
	@Id NVARCHAR (450),
	@UpperBoundary DECIMAL (18, 3),
	@TargetValue   DECIMAL (18, 3),
	@LowerBoundary DECIMAL (18, 3)
AS
    BEGIN 
		SET NOCOUNT ON;

		IF NOT EXISTS(SELECT [Id]
		              FROM [dbo].[BoundaryValues] 
					  WHERE [Id] = @Id)
			BEGIN 
				INSERT INTO [dbo].[BoundaryValues]([Id], [UpperBoundary], [TargetValue], [LowerBoundary])
				VALUES (@Id, @UpperBoundary, @TargetValue, @LowerBoundary)

				SET @Id = SCOPE_IDENTITY();
			END
		
		ElSE
			BEGIN
				RAISERROR('The boundary values already exist.', 16, 1)
			END
	END