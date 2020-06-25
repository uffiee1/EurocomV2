CREATE TABLE [dbo].[PatientMeasurements] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [Date]                 DATE            NULL,
    [MeasurementSucceeded] BIT             NOT NULL,
    [Measurement]          DECIMAL (18, 3) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

