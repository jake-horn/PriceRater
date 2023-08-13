-- =============================================
-- Author:		Jake Horn
-- Create date: 13th August 2023
-- Description:	Get the categories and products for a particular user
-- =============================================
CREATE PROCEDURE dbo.spGetCategoriesAndProductsForUser 
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM dbo.GetUserCategoriesAndProducts WHERE lUserId = @UserId

END