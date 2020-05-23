﻿CREATE PROCEDURE [dbo].[spInventory_Insert]
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice MONEY,
	@PurchaseDate DATETIME2
AS
BEGIN
SET NOCOUNT ON;
  INSERT INTO Inventory(ProductId,Quantity,PurchasePrice,PurchaseDate)
                VALUES(@ProductId,@Quantity,@PurchasePrice,@PurchaseDate)
END
