CREATE TABLE [dbo].[MoviesAndShows] (
    [Name]    VARCHAR (255) NOT NULL,
    [ShowId]  INT           NOT NULL,
    [IsMovie] BIT           NOT NULL,
    [Runtime] INT           NULL,
    [Genre]   INT           CONSTRAINT [DF_MoviesAndShows_Genre] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MoviesAndShows] PRIMARY KEY CLUSTERED ([ShowId] ASC)
);

