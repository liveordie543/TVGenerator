CREATE TABLE [dbo].[MoviesAndShows] (
    [Name]    VARCHAR (255) NOT NULL,
    [ShowId]  INT           NOT NULL,
    [IsMovie] BIT           NOT NULL,
    [Runtime] INT           NULL,
    [GenreId]   INT            NOT NULL,
    CONSTRAINT [PK_MoviesAndShows] PRIMARY KEY CLUSTERED ([ShowId] ASC), 
    CONSTRAINT [FK_MoviesAndShows_Genres] FOREIGN KEY ([GenreId]) REFERENCES [Genres]([Id])
);

