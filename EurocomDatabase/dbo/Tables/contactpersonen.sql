CREATE TABLE [dbo].[contactpersonen] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [UserId]           INT           NOT NULL,
    [FirstnameContact] NVARCHAR (30) NOT NULL,
    [LastnameContact]  NVARCHAR (30) NOT NULL,
    [NumberContact]    NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

