CREATE TABLE [dbo].[DoctorPatient] (
    [DoctorPatientId] INT IDENTITY (1, 1) NOT NULL,
    [DoctorId]        INT NOT NULL,
    [PatientId]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([DoctorPatientId] ASC)
);

