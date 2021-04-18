CREATE TABLE ArtistsSongs
(
    ID            int         NOT NULL    IDENTITY,
    ArtistID      int         NOT NULL,
    SongsID       int         NOT NULL,
    [Order]       int         NOT NULL,

    CONSTRAINT PK_ArtistsSongs  PRIMARY KEY (ID),
    CONSTRAINT FK_ArtistsSongs_Artists_ArtistID      FOREIGN KEY (ArtistID)  REFERENCES Artists (ID) ON DELETE CASCADE,
    CONSTRAINT FK_ArtistsSongs_Songs_SongsID         FOREIGN KEY (SongsID)   REFERENCES Songs (ID) ON DELETE CASCADE
);
GO


CREATE INDEX IX_ArtistsSongs_ArtistID ON ArtistsSongs (ArtistID);
GO

CREATE INDEX IX_ArtistsSongs_SongsID ON ArtistsSongs (SongsID);
GO

