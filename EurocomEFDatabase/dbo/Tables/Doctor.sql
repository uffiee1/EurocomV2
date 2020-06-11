CREATE TABLE [dbo].[Doctor] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NULL,
    [Lastname]  NVARCHAR (MAX) NULL,
    [Specialty] NVARCHAR (MAX) NULL,
    [Email]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED ([ID] ASC)
);

