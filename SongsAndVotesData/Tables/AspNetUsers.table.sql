CREATE TABLE AspNetUsers
(
    Id                          nvarchar(450)      NOT NULL,
    UserName                    nvarchar(256)          NULL,
    NormalizedUserName          nvarchar(256)          NULL,
    Email                       nvarchar(256)          NULL,
    NormalizedEmail             nvarchar(256)          NULL,
    EmailConfirmed              bit                NOT NULL,
    PasswordHash                nvarchar(max)          NULL,
    SecurityStamp               nvarchar(max)          NULL,
    ConcurrencyStamp            nvarchar(max)          NULL,
    PhoneNumber                 nvarchar(max)          NULL,
    PhoneNumberConfirmed        bit                NOT NULL,
    TwoFactorEnabled            bit                NOT NULL,
    LockoutEnd                  datetimeoffset         NULL,
    LockoutEnabled              bit                NOT NULL,
    AccessFailedCount           int                NOT NULL,

    CONSTRAINT PK_AspNetUsers  PRIMARY KEY (Id)
);
GO


CREATE INDEX EmailIndex ON AspNetUsers (NormalizedEmail);
GO

CREATE UNIQUE INDEX UserNameIndex ON AspNetUsers (NormalizedUserName) WHERE NormalizedUserName IS NOT NULL;
GO

