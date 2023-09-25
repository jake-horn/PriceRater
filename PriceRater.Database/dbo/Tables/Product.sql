CREATE TABLE [dbo].[Product] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (250) NOT NULL,
    [Price]         DECIMAL (7, 2) NOT NULL,
    [ClubcardPrice] DECIMAL (7, 2) NULL,
    [WebAddress]    NVARCHAR (500) NOT NULL,
    [DateAdded]     DATETIME2 (7)  NOT NULL,
    [DateUpdated]   DATETIME2 (7)  NULL,
    [RetailerId]    INT            NOT NULL,
    CONSTRAINT [PK__Product__3214EC0764F62EB7] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [RetailerId_FK] FOREIGN KEY ([RetailerId]) REFERENCES [dbo].[Retailer] ([Id]),
    CONSTRAINT [Unique_Web_Address] UNIQUE NONCLUSTERED ([WebAddress] ASC)
);





