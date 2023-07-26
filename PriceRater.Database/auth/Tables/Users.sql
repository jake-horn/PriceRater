CREATE TABLE [auth].[Users] (
    [lUserId]   INT            IDENTITY (1, 1) NOT NULL,
    [sName]     NVARCHAR (150) NULL,
    [sEmail]    NVARCHAR (200) NULL,
    [sPassword] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([lUserId] ASC),
    UNIQUE NONCLUSTERED ([sEmail] ASC)
);

