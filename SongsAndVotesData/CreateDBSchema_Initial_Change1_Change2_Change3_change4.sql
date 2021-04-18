IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

CREATE TABLE [Artists] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Biography] nvarchar(max) NULL,
    [Photo] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NULL,
    CONSTRAINT [PK_Artists] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Users] (
    [ID] int NOT NULL IDENTITY,
    [IsAdmin] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Biography] nvarchar(max) NULL,
    [Photo] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Albums] (
    [ID] int NOT NULL IDENTITY,
    [ArtistByID] int NULL,
    [Name] nvarchar(max) NULL,
    [Photo] nvarchar(max) NULL,
    [UserByID] int NULL,
    CONSTRAINT [PK_Albums] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Albums_Artists_ArtistByID] FOREIGN KEY ([ArtistByID]) REFERENCES [Artists] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Albums_Users_UserByID] FOREIGN KEY ([UserByID]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Songs] (
    [ID] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Photo] nvarchar(max) NULL,
    [UserUploadedID] int NULL,
    [ArtistID] int NULL,
    [AlbumID] int NULL,
    CONSTRAINT [PK_Songs] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Songs_Albums_AlbumID] FOREIGN KEY ([AlbumID]) REFERENCES [Albums] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Songs_Artists_ArtistID] FOREIGN KEY ([ArtistID]) REFERENCES [Artists] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Songs_Users_UserUploadedID] FOREIGN KEY ([UserUploadedID]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ArtistsSongs] (
    [ID] int NOT NULL IDENTITY,
    [ArtistID] int NOT NULL,
    [SongsID] int NOT NULL,
    [Order] int NOT NULL,
    CONSTRAINT [PK_ArtistsSongs] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ArtistsSongs_Artists_ArtistID] FOREIGN KEY ([ArtistID]) REFERENCES [Artists] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArtistsSongs_Songs_SongsID] FOREIGN KEY ([SongsID]) REFERENCES [Songs] ([ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Albums_ArtistByID] ON [Albums] ([ArtistByID]);
GO

CREATE INDEX [IX_Albums_UserByID] ON [Albums] ([UserByID]);
GO

CREATE INDEX [IX_ArtistsSongs_ArtistID] ON [ArtistsSongs] ([ArtistID]);
GO

CREATE INDEX [IX_ArtistsSongs_SongsID] ON [ArtistsSongs] ([SongsID]);
GO

CREATE INDEX [IX_Songs_AlbumID] ON [Songs] ([AlbumID]);
GO

CREATE INDEX [IX_Songs_ArtistID] ON [Songs] ([ArtistID]);
GO

CREATE INDEX [IX_Songs_UserUploadedID] ON [Songs] ([UserUploadedID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210203175342_Initial', N'5.0.5');
GO

ALTER TABLE [Songs] ADD [AudioFile] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210207135641_Change1', N'5.0.5');
GO

ALTER TABLE [Songs] DROP CONSTRAINT [FK_Songs_Artists_ArtistID];
GO

DROP INDEX [IX_Songs_ArtistID] ON [Songs];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Songs]') AND [c].[name] = N'ArtistID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Songs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Songs] DROP COLUMN [ArtistID];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210207143909_Change2', N'5.0.5');
GO

ALTER TABLE [Albums] DROP CONSTRAINT [FK_Albums_Artists_ArtistByID];
GO

ALTER TABLE [Albums] DROP CONSTRAINT [FK_Albums_Users_UserByID];
GO

ALTER TABLE [Songs] DROP CONSTRAINT [FK_Songs_Albums_AlbumID];
GO

ALTER TABLE [Albums] DROP CONSTRAINT [PK_Albums];
GO

EXEC sp_rename N'[Albums]', N'Playlists';
GO

EXEC sp_rename N'[Songs].[AlbumID]', N'PlaylistID', N'COLUMN';
GO

EXEC sp_rename N'[Songs].[IX_Songs_AlbumID]', N'IX_Songs_PlaylistID', N'INDEX';
GO

EXEC sp_rename N'[Playlists].[IX_Albums_UserByID]', N'IX_Playlists_UserByID', N'INDEX';
GO

EXEC sp_rename N'[Playlists].[IX_Albums_ArtistByID]', N'IX_Playlists_ArtistByID', N'INDEX';
GO

ALTER TABLE [Songs] ADD [ArtistID] int NULL;
GO

ALTER TABLE [Playlists] ADD [Discriminator] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Playlists] ADD CONSTRAINT [PK_Playlists] PRIMARY KEY ([ID]);
GO

CREATE INDEX [IX_Songs_ArtistID] ON [Songs] ([ArtistID]);
GO

ALTER TABLE [Playlists] ADD CONSTRAINT [FK_Playlists_Artists_ArtistByID] FOREIGN KEY ([ArtistByID]) REFERENCES [Artists] ([ID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Playlists] ADD CONSTRAINT [FK_Playlists_Users_UserByID] FOREIGN KEY ([UserByID]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Songs] ADD CONSTRAINT [FK_Songs_Artists_ArtistID] FOREIGN KEY ([ArtistID]) REFERENCES [Artists] ([ID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Songs] ADD CONSTRAINT [FK_Songs_Playlists_PlaylistID] FOREIGN KEY ([PlaylistID]) REFERENCES [Playlists] ([ID]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210207144828_Change3', N'5.0.5');
GO

ALTER TABLE [Songs] DROP CONSTRAINT [FK_Songs_Users_UserUploadedID];
GO

EXEC sp_rename N'[Songs].[UserUploadedID]', N'UserID', N'COLUMN';
GO

EXEC sp_rename N'[Songs].[IX_Songs_UserUploadedID]', N'IX_Songs_UserID', N'INDEX';
GO

ALTER TABLE [Songs] ADD CONSTRAINT [FK_Songs_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([ID]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210208075525_change4', N'5.0.5');
GO

