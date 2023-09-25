-- =============================================
-- Author:		Jake Horn
-- Create date: 21st September 2023
-- Description:	Stored procedure to return either the web scraping id for the web address, or add and return it if not
-- =============================================
CREATE PROCEDURE dbo.spGetOrAddWebScrapingId
	@WebAddress NVARCHAR(250)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @WebScrapingID INT
	SET @WebScrapingId = NULL
	
	SELECT TOP(1) @WebScrapingId = Id 
	FROM dbo.WebScrapingList 
	WHERE WebAddress = @WebAddress

	IF @WebScrapingId IS NULL
		BEGIN
			INSERT INTO dbo.WebScrapingList(WebAddress) VALUES (@WebAddress)
		END

	SELECT Id FROM dbo.WebScrapingList WHERE WebAddress = @WebAddress
    
END