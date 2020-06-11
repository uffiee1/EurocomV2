CREATE PROCEDURE [dbo].[sp_BoundaryValues_GetById]
	@Id INT
AS
	BEGIN
		SELECT [UpperBoundary], [TargetValue] ,[Lowerboundary] FROM [dbo].[BoundaryValues]
		WHERE  [Id] = @Id
	END
RETURN 0