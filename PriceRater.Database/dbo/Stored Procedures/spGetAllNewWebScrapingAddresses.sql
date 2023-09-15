-- =============================================
-- Author:		Jake Horn
-- Create date: 15/07/2023
-- Description:	Proc returns only the values of WebScrapingList that do not currently exist in dbo.Product
-- =============================================
CREATE PROCEDURE dbo.spGetAllNewWebScrapingAddresses

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT
	    wsl.Id, 
	    wsl.WebAddress
	FROM dbo.WebScrapingList AS wsl
	LEFT JOIN dbo.Product AS p ON wsl.Id = p.WebScrapingId
	WHERE p.WebScrapingId IS NULL

END