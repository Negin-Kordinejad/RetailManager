CREATE PROCEDURE [dbo].[spUserLookUp]
	@id nvarchar(128)
AS
BEGIN
SET NOCOUNT ON;
	SELECT ID,FirstName,LastName,EmailAddress,CreateDate
	FROM [dbo].[User]
	WHERE id=@id
END