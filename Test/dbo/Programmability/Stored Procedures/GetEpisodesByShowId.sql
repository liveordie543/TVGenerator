CREATE PROCEDURE [dbo].[GetEpisodesByShowId]
	@ShowId int
AS
	SELECT [Id], [Name], [ShowId], [Season], [EpisodeNumber], [EpisodeId]
	FROM [dbo].[Episodes]
	WHERE [ShowId] = @ShowId

RETURN 0
