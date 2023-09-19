CREATE TABLE [dbo].[UserProductCategory] (
    [lId]         INT IDENTITY (1, 1) NOT NULL,
    [lCategoryId] INT NOT NULL,
    [lProductId]  INT NOT NULL,
    [lUserId]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([lId] ASC),
    FOREIGN KEY ([lCategoryId]) REFERENCES [dbo].[Category] ([lId]),
    FOREIGN KEY ([lUserId]) REFERENCES [auth].[Users] ([lUserId]),
    CONSTRAINT [FK__UserProdu__lProd__236943A5] FOREIGN KEY ([lProductId]) REFERENCES [dbo].[Product] ([Id])
);



