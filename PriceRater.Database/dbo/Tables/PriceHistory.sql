CREATE TABLE [dbo].[PriceHistory] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (250) NOT NULL,
    [Price]         DECIMAL (7, 2) NOT NULL,
    [ClubcardPrice] DECIMAL (7, 2) NULL,
    [WebAddress]    NVARCHAR (500) NOT NULL,
    [DateAdded]     DATETIME2 (7)  NOT NULL,
    [RetailerId]    INT            NOT NULL,
    [WebScrapingId] INT            NOT NULL,
    CONSTRAINT [PK__PriceHis__3214EC071D0FFDFD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__PriceHist__Retai__6FE99F9F] FOREIGN KEY ([RetailerId]) REFERENCES [dbo].[Retailer] ([Id]),
    CONSTRAINT [FK__PriceHist__WebSc__70DDC3D8] FOREIGN KEY ([WebScrapingId]) REFERENCES [dbo].[WebScrapingList] ([Id])
);



