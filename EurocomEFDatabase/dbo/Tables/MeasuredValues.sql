CREATE TABLE [dbo].[MeasuredValues] (
    [Id]            NVARCHAR (450) NOT NULL,
    [DeviceId]      NVARCHAR (50)  NULL,
    [MeasurementId] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([MeasurementId]) REFERENCES [dbo].[PatientMeasurements] ([Id])
);

