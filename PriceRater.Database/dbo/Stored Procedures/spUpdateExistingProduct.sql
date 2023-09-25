-- =============================================
-- Author:		Jake Horn
-- Create date: 19th July 2023
-- Description:	Updates an existing product, and adds the entry to dbo.PriceHistory
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateExistingProduct] 
	@Title NVARCHAR(500), 
	@Price DECIMAL(7,2), 
	@ClubcardPrice DECIMAL(7,2) = NULL, 
	@WebAddress NVARCHAR(500), 
	@DateAdded DATETIME2, 
	@DateUpdated DATETIME2, 
	@RetailerId INT
AS

BEGIN
	SET NOCOUNT ON;

	-- Update the current product entry
	UPDATE dbo.Product
	SET
		Title = @Title,
		Price = @Price,
		ClubcardPrice = @ClubcardPrice, 
		DateUpdated = @DateUpdated
	WHERE WebAddress = @WebAddress

	-- Add updated product into the dbo.PriceHistory
	EXEC dbo.spAddNewProductToPriceHistory @Title, @Price, @ClubcardPrice, @WebAddress, @DateAdded, @RetailerId
		
END