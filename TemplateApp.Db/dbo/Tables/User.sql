CREATE TABLE [dbo].[User] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Email]           VARCHAR (512) NOT NULL,
    [FirstName]       NVARCHAR (50) NOT NULL,
    [LastName]        NVARCHAR (50) NOT NULL,
    [PasswordHash]    VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);



