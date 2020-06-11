CREATE TABLE [dbo].[User_AspUser] (
    [UserId]    INT         IDENTITY (1, 1) NOT NULL,
    [AspUserId] NCHAR (450) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

