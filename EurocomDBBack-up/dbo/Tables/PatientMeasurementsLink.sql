CREATE TABLE [dbo].[PatientMeasurementsLink] (
    [LinkId]        INT           IDENTITY (1, 1) NOT NULL,
    [PatientId]     NVARCHAR (50) NULL,
    [MeasurementId] INT           NULL,
    PRIMARY KEY CLUSTERED ([LinkId] ASC)
);

