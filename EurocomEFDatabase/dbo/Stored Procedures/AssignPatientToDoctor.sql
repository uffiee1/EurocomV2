CREATE PROCEDURE [dbo].[AssignPatientToDoctor]
	@username nvarchar(30),
	@userId int
AS
	DECLARE @doctorId int
	SET @doctorId = (SELECT aspD.Id
					 FROM AspNetUsers AS aspD
					 INNER JOIN AspNetUserRoles
					 ON aspD.Id = AspNetUserRoles.UserId
					 INNER JOIN AspNetRoles
					 ON AspNetUserRoles.RoleId = AspNetRoles.Id
					 WHERE aspD.UserName = @username
					 AND AspNetRoles.[Name] = 'Doctor')
	DECLARE @patientId int
	SET @patientId = (SELECT aspP.Id
					  FROM AspNetUsers AS aspP
					  INNER JOIN AspNetUserRoles
					  ON aspP.Id = AspNetUserRoles.UserId
					  INNER JOIN AspNetRoles
					  ON AspNetUserRoles.RoleId = AspNetRoles.Id
					  WHERE aspP.Id = @userId
					  AND AspNetRoles.[Name] = 'Patient')
	INSERT INTO PatientDokterKoppel(patientId, doktersId)
	VALUES(@patientId, @doctorId);