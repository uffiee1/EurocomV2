CREATE TABLE [dbo].[PatientDoctorLink] (
    [LinkId]    INT           IDENTITY (1, 1) NOT NULL,
    [PatientId] NVARCHAR (50) NULL,
    [DoctorId]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([LinkId] ASC)
);

