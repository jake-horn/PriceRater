-- =============================================
-- Author:		Jake Horn
-- Create date: 19th July 2023
-- Description:	Checks if a product exists in dbo.Product, returns as a bit value
-- =============================================
CREATE PROCEDURE [dbo].[spCheckIfProductExists]
	-- Add the parameters for the stored procedure here
	@WebAddress INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT CASE WHEN EXISTS 
		(SELECT 1 FROM dbo.Product 
		WHERE WebAddress = @WebAddress)
	THEN CAST (1 AS BIT) 
	ELSE CAST (0 AS BIT) 
	END
END