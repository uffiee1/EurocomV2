CREATE TABLE [dbo].[User] (
    [UserId]      INT           IDENTITY (1, 1) NOT NULL,
    [Firstname]   NVARCHAR (15) NOT NULL,
    [Lastname]    NVARCHAR (15) NOT NULL,
    [Username]    NVARCHAR (30) NOT NULL,
    [Password]    NVARCHAR (25) NOT NULL,
    [PhoneNumber] NVARCHAR (10) NOT NULL,
    [Agreement]   BIT           NOT NULL,
    [Email]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

