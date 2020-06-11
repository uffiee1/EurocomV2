CREATE PROCEDURE [dbo].[CreateStatus]
	@username nvarchar(15),
	@currentDatetime nvarchar(19),
	@inr float
AS
	DECLARE @patientId int, @statusId int
	SET @patientId = (SELECT Patient.PatientId
					  FROM Patient
					  INNER JOIN [User]
					  ON Patient.UserId = [User].UserId
					  WHERE [User].Username = @username)
	INSERT INTO [Status] ([Date], INR)
	VALUES (CONVERT(DATETIME, @currentDatetime, 103), @inr)
	SET @statusId = (SELECT MAX(StatusId)
					 FROM [Status])
	INSERT INTO PatientStatus (PatientId, StatusId)
	VALUES (@patientId, @statusId);