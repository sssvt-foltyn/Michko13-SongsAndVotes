CREATE TABLE Albums
(
    ID                int               NOT NULL    IDENTITY,
    ArtistByID        int                   NULL,
    Name              nvarchar(max)         NULL,
    Photo             nvarchar(max)         NULL,
    UserByID          int                   NULL,

    CONSTRAINT PK_Albums  PRIMARY KEY (ID),
    CONSTRAINT FK_Albums_Artists_ArtistByID  FOREIGN KEY (ArtistByID) REFERENCES Artists (ID) ON DELETE NO ACTION,
    CONSTRAINT FK_Albums_Users_UserByID  FOREIGN KEY (UserByID) REFERENCES Users (ID) ON DELETE NO ACTION
);
GO


CREATE INDEX IX_Albums_ArtistByID ON Albums (ArtistByID);
GO

CREATE INDEX IX_Albums_UserByID ON Albums (UserByID);
GO

