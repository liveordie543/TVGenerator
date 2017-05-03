CREATE PROCEDURE [dbo].[GetGenres]
AS
	SELECT [Id], [Name] 
	FROM Genres

RETURN 0
