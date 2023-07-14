CREATE TABLE [dbo].[PriceHistory] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (250) NOT NULL,
    [Price]         DECIMAL (7, 2) NOT NULL,
    [WebAddress]    NVARCHAR (500) NOT NULL,
    [DateAdded]     DATETIME2 (7)  NOT NULL,
    [RetailerId]    INT            NOT NULL,
    [WebScrapingId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([RetailerId]) REFERENCES [dbo].[Retailer] ([Id]),
    FOREIGN KEY ([WebScrapingId]) REFERENCES [dbo].[WebScrapingList] ([Id])
);

