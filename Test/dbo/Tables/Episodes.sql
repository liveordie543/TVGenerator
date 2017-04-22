CREATE TABLE [dbo].[Episodes] (
    [Id] INT NOT NULL IDENTITY, 
    [Name]      VARCHAR (255) NULL,
    [ShowId]    INT           NOT NULL,
    [Season]    INT           NULL,
    [EpisodeNumber]   INT           NULL,
    [EpisodeId] INT           NOT NULL,
    CONSTRAINT [FK_Episodes_MoviesAndShows] FOREIGN KEY ([ShowId]) REFERENCES [dbo].[MoviesAndShows] ([ShowId]), 
    CONSTRAINT [PK_Episodes] PRIMARY KEY ([Id])
);

