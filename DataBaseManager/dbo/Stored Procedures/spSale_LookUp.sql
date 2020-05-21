CREATE PROCEDURE [dbo].[spSale_LookUp]
	@CashierId NVARCHAR(128) , 
	@SaleDate DATETIME2 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id
	FROM Sale
	WHERE CashierId=@CashierId AND SaleDate=@SaleDate
END