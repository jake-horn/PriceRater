-- =============================================
-- Author:		Jake Horn
-- Create date: 30/05/2023
-- Description:	Adds product to dbo.Product table
-- =============================================
CREATE PROCEDURE [dbo].[spAddProduct] 
	-- Add the parameters for the stored procedure here
	@Title NVARCHAR(500), 
	@Price DECIMAL(7,2), 
	@ClubcardPrice DECIMAL(7,2) = NULL, 
	@WebAddress NVARCHAR(500), 
	@DateAdded DATETIME2, 
	@DateUpdated DATETIME2, 
	@RetailerId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.Product
	(
		Title, 
		Price, 
		ClubcardPrice, 
		WebAddress, 
		DateAdded, 
		DateUpdated, 
		RetailerId
	)
	VALUES
	(
		@Title, 
		@Price, 
		@ClubcardPrice, 
		@WebAddress, 
		@DateAdded, 
		@DateUpdated, 
		@RetailerId
	)

	EXEC dbo.spAddNewProductToPriceHistory
		@Title = @Title, 
		@Price = @Price, 
		@ClubcardPrice = @ClubcardPrice, 
		@WebAddress = @WebAddress, 
		@DateAdded = @DateAdded, 
		@RetailerId = @RetailerId

END
