CREATE TABLE [dbo].[Product] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (250) NOT NULL,
    [Price]         DECIMAL (7, 2) NOT NULL,
    [WebAddress]    NVARCHAR (500) NOT NULL,
    [DateAdded]     DATETIME2 (7)  NOT NULL,
    [DateUpdated]   DATETIME2 (7)  NULL,
    [RetailerId]    INT            NOT NULL,
    [WebScrapingId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [RetailerId_FK] FOREIGN KEY ([RetailerId]) REFERENCES [dbo].[Retailer] ([Id]),
    CONSTRAINT [WebScrapingId_FK] FOREIGN KEY ([WebScrapingId]) REFERENCES [dbo].[WebScrapingList] ([Id])
);

