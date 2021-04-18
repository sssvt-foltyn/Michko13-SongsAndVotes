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
VALUES (N'20210203175342_Initial', N'5.0.3');
GO

ALTER TABLE [Songs] ADD [AudioFile] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210207135641_Change1', N'5.0.3');
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
VALUES (N'20210207143909_Change2', N'5.0.3');
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
VALUES (N'20210207144828_Change3', N'5.0.3');
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
VALUES (N'20210208075525_change4', N'5.0.3');
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210217172450_IdentityTables', N'5.0.3');
GO

ALTER TABLE [Playlists] DROP CONSTRAINT [FK_Playlists_Users_UserByID];
GO

ALTER TABLE [Songs] DROP CONSTRAINT [FK_Songs_Users_UserID];
GO

ALTER TABLE [Users] DROP CONSTRAINT [PK_Users];
GO

EXEC sp_rename N'[Users]', N'User';
GO

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([ID]);
GO

ALTER TABLE [Playlists] ADD CONSTRAINT [FK_Playlists_User_UserByID] FOREIGN KEY ([UserByID]) REFERENCES [User] ([ID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Songs] ADD CONSTRAINT [FK_Songs_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([ID]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210217172706_Identitytables2', N'5.0.3');
GO

