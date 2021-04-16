CREATE TABLE Artists
(
    ID             int              NOT NULL    IDENTITY,
    Name           nvarchar(max)        NULL,
    Biography      nvarchar(max)        NULL,
    Photo          nvarchar(max)        NULL,
    DateOfBirth    datetime2            NULL,

    CONSTRAINT PK_Artists  PRIMARY KEY (ID)
);
GO


