CREATE TABLE User
(
    ID              int              NOT NULL    IDENTITY,
    IsAdmin         bit              NOT NULL,
    Name            nvarchar(max)        NULL,
    Biography       nvarchar(max)        NULL,
    Photo           nvarchar(max)        NULL,
    DateOfBirth     datetime2            NULL,

    CONSTRAINT PK_User  PRIMARY KEY (ID)
);
GO


