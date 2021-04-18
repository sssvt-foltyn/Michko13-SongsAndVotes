CREATE TABLE Playlists
(
    ID                int               NOT NULL    IDENTITY,
    ArtistByID        int                   NULL,
    Name              nvarchar(max)         NULL,
    Photo             nvarchar(max)         NULL,
    UserByID          int                   NULL,
    Discriminator     nvarchar(max)     NOT NULL    DEFAULT N'',

    CONSTRAINT PK_Playlists  PRIMARY KEY (ID),
    CONSTRAINT FK_Playlists_Artists_ArtistByID     FOREIGN KEY (ArtistByID)  REFERENCES Artists (ID) ON DELETE NO ACTION,
    CONSTRAINT FK_Playlists_Users_UserByID         FOREIGN KEY (UserByID)    REFERENCES Users (ID) ON DELETE NO ACTION
);
GO


CREATE INDEX IX_Playlists_ArtistByID ON Playlists (ArtistByID);
GO

CREATE INDEX IX_Playlists_UserByID ON Playlists (UserByID);
GO

