-- =============================================
-- Author:		Jake Horn
-- Create date: 9th August 2023
-- Description:	Gets all products from dbo.Product, used primarily for testing frontend
-- =============================================
CREATE PROCEDURE dbo.GetAllProducts

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		*
	FROM dbo.Product

END