CREATE TYPE GenreIdTable AS TABLE
(
    Id int NOT NULL
);
GO

CREATE PROCEDURE [dbo].[GetMoviesAndShows]
	@IncludeMovies bit,
	@IncludeShows bit,
	@GenreIds GenreIdTable READONLY
AS
	DECLARE @MoviesAndShowsTemp TABLE ([Name] [varchar](255) NOT NULL, [ShowId] [int] NOT NULL, [IsMovie] [bit] NOT NULL,
		[Runtime] [int] NULL, [Genre] [int] NOT NULL)

	IF @IncludeMovies = 1
	BEGIN
		INSERT INTO @MoviesAndShowsTemp SELECT [Name], [ShowId], [IsMovie], [Runtime], [Genre] FROM MoviesAndShows
		WHERE [IsMovie] = 1 AND [Genre] IN (SELECT [Id] FROM @GenreIds g)
	END

	IF @IncludeShows = 1
	BEGIN
		INSERT INTO @MoviesAndShowsTemp SELECT [Name], [ShowId], [IsMovie], [Runtime], [Genre] FROM MoviesAndShows
		WHERE [IsMovie] = 0 AND [Genre] IN (SELECT [Id] FROM @GenreIds g)
	END

	SELECT [Name], [ShowId], [IsMovie], [Runtime], [Genre] 
	FROM @MoviesAndShowsTemp temp

RETURN 0
