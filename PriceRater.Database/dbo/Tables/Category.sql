CREATE TABLE [dbo].[Category] (
    [lId]           INT            IDENTITY (1, 1) NOT NULL,
    [sCategoryName] NVARCHAR (200) NOT NULL,
    [lUserId]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([lId] ASC),
    FOREIGN KEY ([lUserId]) REFERENCES [auth].[Users] ([lUserId])
);



