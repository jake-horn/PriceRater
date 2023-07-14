CREATE TABLE [dbo].[UserCategoryProduct] (
    [CategoryProductId] INT IDENTITY (1, 1) NOT NULL,
    [CategoryId]        INT NULL,
    [ProductId]         INT NULL,
    PRIMARY KEY CLUSTERED ([CategoryProductId] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

