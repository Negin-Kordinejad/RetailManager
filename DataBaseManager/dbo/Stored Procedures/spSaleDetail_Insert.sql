CREATE PROCEDURE [dbo].[spSaleDetail_Insert]
	@SaleId INT ,
	@ProductId INT,
	@Quantity INT,
	@PurchasPrice MONEY,
	@TAX MONEY
AS
BEGIN
SET NOCOUNT ON;
INSERT INTO SaleDetail (SaleId,ProductId,Quantity,PurchasPrice,Tax)
  VALUES(@SaleId,@ProductId,@Quantity,@PurchasPrice,@TAX)
END

