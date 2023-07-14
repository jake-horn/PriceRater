-- =============================================
-- Author:		Jake Horn
-- Create date: 29/05/2023
-- Description:	Gets all the web addresses required for web scraping
-- =============================================
CREATE PROCEDURE dbo.spGetAllWebScrapingAddresses 

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		Id, 
		WebAddress
	FROM dbo.WebScrapingList

END

