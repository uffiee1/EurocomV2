CREATE TABLE [dbo].[PatientDokterKoppel] (
    [koppel_Id] INT           IDENTITY (1, 1) NOT NULL,
    [patientId] NVARCHAR (50) NULL,
    [doktersId] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([koppel_Id] ASC)
);

