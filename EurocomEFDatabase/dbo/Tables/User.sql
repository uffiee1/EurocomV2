CREATE TABLE [dbo].[User] (
    [UserId]      INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (MAX) NULL,
    [Lastname]    NVARCHAR (MAX) NULL,
    [Username]    NVARCHAR (MAX) NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [Password]    NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    [Agreement]   BIT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

