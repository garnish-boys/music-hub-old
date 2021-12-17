CREATE PROCEDURE [dbo].[spSample_Get]
	@Id int not null
AS
BEGIN
	SELECT * 
	FROM [dbo].[Samples]
	WHERE Id = @Id;
END

