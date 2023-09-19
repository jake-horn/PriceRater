-- =============================================
-- Author:		Jake Horn
-- Create date: 14/07/2023
-- Description:	Created proc to add products to dbo.PriceHistory when a product is added or updated
-- =============================================
CREATE PROCEDURE [dbo].[spAddNewProductToPriceHistory]
	-- Add the parameters for the stored procedure here
	@Title NVARCHAR(500), 
	@Price DECIMAL(7,2), 
	@ClubcardPrice DECIMAL(7,2),
	@WebAddress NVARCHAR(500), 
	@DateAdded DATETIME2,  
	@RetailerId INT, 
	@WebScrapingId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.PriceHistory
	(
		Title, 
		Price, 
		ClubcardPrice,
		WebAddress, 
		DateAdded,  
		RetailerId, 
		WebScrapingId
	)
	VALUES
	(
		@Title, 
		@Price, 
		@ClubcardPrice, 
		@WebAddress, 
		@DateAdded, 
		@RetailerId, 
		@WebScrapingId
	)
END