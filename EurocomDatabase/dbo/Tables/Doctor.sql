CREATE TABLE [dbo].[Doctor] (
    [DoctorId] INT           IDENTITY (1, 1) NOT NULL,
    [UserId]   INT           NOT NULL,
    [Type]     NVARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([DoctorId] ASC)
);

