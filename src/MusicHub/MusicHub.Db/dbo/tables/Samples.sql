CREATE TABLE [dbo].[Samples]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [FilePath] NVARCHAR(MAX) NOT NULL
)
