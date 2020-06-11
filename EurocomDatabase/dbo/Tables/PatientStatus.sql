CREATE TABLE [dbo].[PatientStatus] (
    [PatientStatusId] INT IDENTITY (1, 1) NOT NULL,
    [PatientId]       INT NOT NULL,
    [StatusId]        INT NOT NULL,
    PRIMARY KEY CLUSTERED ([PatientStatusId] ASC)
);

