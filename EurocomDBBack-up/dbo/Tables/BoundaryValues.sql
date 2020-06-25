CREATE TABLE [dbo].[BoundaryValues] (
    [Id]            NVARCHAR (450)  NOT NULL,
    [Upperboundary] DECIMAL (18, 3) NOT NULL,
    [TargetValue]   DECIMAL (18, 3) NOT NULL,
    [Lowerboundary] DECIMAL (18, 3) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

