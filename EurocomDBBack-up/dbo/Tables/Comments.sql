CREATE TABLE [dbo].[Comments] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Comment]   VARCHAR (MAX) NOT NULL,
    [Date]      DATE          NOT NULL,
    [Important] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

