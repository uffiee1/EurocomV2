CREATE TABLE [dbo].[Status] (
    [StatusId] INT            IDENTITY (1, 1) NOT NULL,
    [Date]     DATETIME       NULL,
    [INR]      DECIMAL (2, 1) NULL,
    PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

