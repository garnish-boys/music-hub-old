CREATE PROCEDURE [dbo].[spSample_Insert]
	@Name nvarchar(MAX),
	@Description nvarchar(MAX),
	@FilePath nvarchar(MAX)
AS
	insert into dbo.[Samples] ([Name], [Description], [FilePath])
	values (@Name, @Description, @FilePath)
RETURN inserted.Id
