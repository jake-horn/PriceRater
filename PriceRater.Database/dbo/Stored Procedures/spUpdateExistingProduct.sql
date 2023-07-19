-- =============================================
-- Author:		Jake Horn
-- Create date: 19th July 2023
-- Description:	Updates an existing product, and adds the entry to dbo.PriceHistory
-- =============================================
CREATE PROCEDURE dbo.spUpdateExistingProduct 
	@Title NVARCHAR(500), 
	@Price DECIMAL(7,2), 
	@WebAddress NVARCHAR(500), 
	@DateAdded DATETIME2, 
	@DateUpdated DATETIME2, 
	@RetailerId INT, 
	@WebScrapingId INT
AS

BEGIN
	SET NOCOUNT ON;

	-- Update the current product entry
	UPDATE dbo.Product
	SET
		Title = @Title,
		Price = @Price,
		DateUpdated = @DateUpdated
	WHERE WebScrapingId = @WebScrapingId

	-- Add updated product into the dbo.PriceHistory
	EXEC dbo.spAddNewProductToPriceHistory @Title, @Price, @WebAddress, @DateAdded, @DateUpdated, @RetailerId, @WebScrapingId
		
END