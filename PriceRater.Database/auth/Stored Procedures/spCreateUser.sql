CREATE PROCEDURE [auth].[spCreateUser] 
	@Name NVARCHAR(150), 
	@Email NVARCHAR(200), 
	@Password NVARCHAR(200),
	@UserId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO auth.Users (sName, sEmail, sPassword)
    VALUES (@Name, @Email, @Password);

    -- Capture the newly inserted ID in the @ID OUTPUT parameter
    SET @UserId = SCOPE_IDENTITY();

    -- Return the inserted user object using SELECT
    SELECT lUserId AS 'UserId', sName AS 'Name', sEmail AS 'Email', sPassword AS 'Password' FROM auth.Users WHERE lUserId = @UserId;
END