CREATE PROCEDURE [dbo].[DeletePatient]
	@username nvarchar(30)
AS
	BEGIN TRANSACTION;

	DECLARE @patientId int, @userId int
	SET @patientId = (SELECT Patient.PatientId
				      FROM Patient
				      INNER JOIN [User] AS uP
				      ON uP.UserId = Patient.UserId
					  WHERE uP.Username = @username)
	SET @userId = (SELECT Patient.UserId
				   FROM Patient
				   INNER JOIN [User] AS uP
		 		   ON uP.UserId = Patient.UserId
		  		   WHERE uP.Username = @username)
	
	-- Delete DoctorPatient relation(s)
	DELETE
	FROM DoctorPatient
	WHERE PatientId = @patientId;

	-- Delete Status record(s)
	DELETE s
	FROM [Status] AS s
	INNER JOIN PatientStatus
	ON PatientStatus.StatusId = s.StatusId
	INNER JOIN Patient
	ON Patient.PatientId = PatientStatus.PatientId
	INNER JOIN [User] AS uP
	ON uP.UserId = Patient.UserId
	WHERE uP.UserId = @userId;

	-- Delete PatientStatus relation(s)
	DELETE ps
	FROM PatientStatus AS ps
	INNER JOIN Patient
	ON Patient.PatientId = ps.PatientId
	INNER JOIN [User] AS uP
	ON uP.UserId = Patient.UserId
	WHERE uP.UserId = @userId;

	-- Delete User record
	DELETE
	FROM [User]
	WHERE UserId = @userId;

	-- Delete Patient record
	DELETE
	FROM Patient
	WHERE PatientId = @patientId;

	COMMIT;