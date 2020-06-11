CREATE TABLE [dbo].[Patient] (
    [PatientId]    INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NULL,
    [DateOfBirth]  DATE          NULL,
    [Age]          INT           NULL,
    [SecurityCode] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([PatientId] ASC)
);

