-- =============================================
-- Author:		Jake Horn
-- Create date: 26th July 2023
-- Description:	Returns a user by id
-- =============================================
CREATE PROCEDURE auth.spGetUserById
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		lUserId AS 'UserId', 
		sName AS 'Name', 
		sEmail AS 'Email',
		sPassword AS 'Password'
	FROM auth.Users
	WHERE lUserId = @Id

END