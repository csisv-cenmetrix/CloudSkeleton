
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(250) NOT NULL,
    [AddressNo] nvarchar(10) NULL,
    [Street] nvarchar(200) NULL,
    [City] nvarchar(50) NOT NULL,
    [JobRole] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Todos] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(150) NOT NULL,
    [Description] nvarchar(300) NULL,
    [Created] datetime2 NOT NULL,
    [Due] datetime2 NOT NULL,
    [Status] int NOT NULL,
    [AuthorId] int NOT NULL,
    CONSTRAINT [PK_Todos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Todos_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AddressNo', N'City', N'FullName', N'JobRole', N'Street') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] ON;
INSERT INTO [Authors] ([Id], [AddressNo], [City], [FullName], [JobRole], [Street])
VALUES (1, N'45', N'Belgium', N'Trevor	Duncan', N'Developer', N'Street 1'),
(2, N'35', N'Brazil', N'Carol	McLean', N'Systems Engineer', N'Street 2'),
(3, N'25', N'Netherlands', N'Alexander	Pullman', N'Developer', N'Street 3'),
(4, N'15', N'Ukraine', N'Adrian	Parr', N'QA', N'Street 4');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AddressNo', N'City', N'FullName', N'JobRole', N'Street') AND [object_id] = OBJECT_ID(N'[Authors]'))
    SET IDENTITY_INSERT [Authors] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] ON;
INSERT INTO [Todos] ([Id], [AuthorId], [Created], [Description], [Due], [Status], [Title])
VALUES (1, 1, '2021-08-23T12:34:55.2250630+05:30', N'Get some text books for school', '2021-08-28T12:34:55.2263719+05:30', 0, N'Get books for school - DB');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] ON;
INSERT INTO [Todos] ([Id], [AuthorId], [Created], [Description], [Due], [Status], [Title])
VALUES (2, 1, '2021-08-23T12:34:55.2264578+05:30', N'Go to supermarket and by some stuff', '2021-08-28T12:34:55.2264583+05:30', 0, N'Need some grocceries');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] ON;
INSERT INTO [Todos] ([Id], [AuthorId], [Created], [Description], [Due], [Status], [Title])
VALUES (3, 2, '2021-08-23T12:34:55.2264589+05:30', N'Buy new camera', '2021-08-28T12:34:55.2264590+05:30', 0, N'Purchase Camera');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AuthorId', N'Created', N'Description', N'Due', N'Status', N'Title') AND [object_id] = OBJECT_ID(N'[Todos]'))
    SET IDENTITY_INSERT [Todos] OFF;
GO

CREATE INDEX [IX_Todos_AuthorId] ON [Todos] ([AuthorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210823070455_InitialCreate', N'5.0.6');
GO

COMMIT;
GO
