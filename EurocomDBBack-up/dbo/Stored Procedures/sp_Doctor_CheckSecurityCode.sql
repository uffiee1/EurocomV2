CREATE PROCEDURE [dbo].[sp_Doctor_CheckSecurityCode]
	@securityCode nvarchar(50)
AS
	SELECT AspP.Id
	FROM AspNetUsers AS AspP
	INNER JOIN AspNetUserRoles
	ON aspP.Id = AspNetUserRoles.UserId
	INNER JOIN AspNetRoles
	ON AspNetUserRoles.RoleId = AspNetRoles.Id
	WHERE AspNetRoles.[Name] = 'Patient'
	AND AspP.SecurityStamp = @securityCode;