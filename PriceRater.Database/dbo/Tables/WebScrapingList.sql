CREATE TABLE [dbo].[WebScrapingList] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [WebAddress] NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

