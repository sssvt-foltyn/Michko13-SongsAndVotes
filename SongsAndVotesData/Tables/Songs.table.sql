CREATE TABLE Songs
(
    ID                 int               NOT NULL IDENTITY,
    Title              nvarchar(max)     NOT NULL,
    Photo              nvarchar(max)         NULL,
    UserUploadedID     int                   NULL,
    AlbumID            int                   NULL,
    AudioFile          nvarchar(max)         NULL,

    CONSTRAINT PK_Songs  PRIMARY KEY (ID),
    CONSTRAINT FK_Songs_Albums_AlbumID         FOREIGN KEY (AlbumID)         REFERENCES Albums (ID) ON DELETE NO ACTION,
    CONSTRAINT FK_Songs_Users_UserUploadedID   FOREIGN KEY (UserUploadedID)  REFERENCES Users (ID) ON DELETE NO ACTION
);
GO


CREATE INDEX IX_Songs_AlbumID ON Songs (AlbumID);
GO

CREATE INDEX IX_Songs_UserUploadedID ON Songs (UserUploadedID);
GO

