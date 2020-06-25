CREATE PROCEDURE [dbo].[sp_Comments_Update]
    @Id INT,
	@Comment VARCHAR(MAX),
	@Date DATE, 
	@Important BIT
AS
	BEGIN
		SET NOCOUNT ON;
		IF EXISTS (SELECT [Comment], [Date], [Important]
					   FROM dbo.[Comments]
					   WHERE [Id] = @Id)
			BEGIN
				UPDATE Comments
				SET [Comment] = @Comment, [Date] = @Date, [Important] = @Important
				WHERE [Id] = @Id
			END

		ELSE 
			BEGIN
				RAISERROR('The comment does not exists', 16, 1)
			END
	END