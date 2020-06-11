CREATE PROCEDURE [dbo].[sp_BoundaryValues_GetAll]
AS
	BEGIN
		SELECT [UpperBoundary], [TargetValue] ,[Lowerboundary] FROM [dbo].[BoundaryValues]
	END
RETURN 0