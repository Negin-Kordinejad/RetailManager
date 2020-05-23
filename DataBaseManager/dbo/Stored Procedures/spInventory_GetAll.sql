CREATE PROCEDURE [dbo].[spInventory_GetAll]
AS
BEGIN
	SELECT [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	FROM Inventory
END
