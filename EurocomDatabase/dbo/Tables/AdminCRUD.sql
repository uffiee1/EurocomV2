CREATE TABLE [dbo].[AdminCRUD] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Specialty] VARCHAR (50)  NOT NULL,
    [Email]     VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AdminDoktorCRUD] PRIMARY KEY CLUSTERED ([ID] ASC)
);

