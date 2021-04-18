CREATE TABLE Songs
(
    ID                 int               NOT NULL IDENTITY,
    Title              nvarchar(max)     NOT NULL,
    Photo              nvarchar(max)         NULL,
    UserID             int                   NULL,
    PlaylistID         int                   NULL,
    AudioFile          nvarchar(max)         NULL,
    ArtistID           int                   NULL,

    CONSTRAINT PK_Songs  PRIMARY KEY (ID),
    CONSTRAINT FK_Songs_Artists_ArtistID       FOREIGN KEY (ArtistID)         REFERENCES Artists (ID)    ON DELETE NO ACTION,
    CONSTRAINT FK_Songs_Playlists_PlaylistID   FOREIGN KEY (PlaylistID)       REFERENCES Playlists (ID)  ON DELETE NO ACTION,
    CONSTRAINT FK_Songs_User_UserID            FOREIGN KEY (UserID)           REFERENCES User (ID)       ON DELETE NO ACTION
);
GO


CREATE INDEX IX_Songs_PlaylistID ON Songs (PlaylistID);
GO

CREATE INDEX IX_Songs_UserID ON Songs (UserID);
GO

CREATE INDEX IX_Songs_ArtistID ON Songs (ArtistID);
GO

