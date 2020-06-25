CREATE PROCEDURE [dbo].[sp_Comments_Insert]
    @Id INT,
	@Comment VARCHAR(MAX),
	@Date DATE, 
	@Important BIT
AS
	BEGIN
		SET NOCOUNT ON;
		IF NOT EXISTS (SELECT [Comment], [Date], [Important]
					   FROM dbo.[Comments]
					   WHERE [Id] = @Id)
			BEGIN
				INSERT INTO [dbo].[Comments]([Comment], [Date], [Important]) 
				VALUES(@Comment, @Date, @Important)
			END

		ELSE 
			BEGIN
				RAISERROR('The comment already exists', 16, 1)
			END
	END