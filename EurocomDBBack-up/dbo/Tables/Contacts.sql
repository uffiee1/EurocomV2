CREATE TABLE [dbo].[Contacts] (
    [ContactId]   INT           IDENTITY (1, 1) NOT NULL,
    [id]          NVARCHAR (50) NULL,
    [FirstName]   NVARCHAR (50) NULL,
    [LastName]    NVARCHAR (50) NULL,
    [PhoneNumber] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC)
);

