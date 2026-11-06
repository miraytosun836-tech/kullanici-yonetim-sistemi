-- MyProject veritabanı kurulum scripti.
-- Admin hesabı burada oluşturulmaz. Program ilk açılışta
-- admin@mcbu.edu.tr hesabını hash'leyerek ekler.

IF DB_ID(N'MyProjectDb') IS NULL
    CREATE DATABASE MyProjectDb;
GO

USE MyProjectDb;
GO

IF OBJECT_ID(N'dbo.Roles', N'U') IS NULL
BEGIN
    CREATE TABLE Roles
    (
        RoleId INT IDENTITY PRIMARY KEY,
        RoleName NVARCHAR(30) NOT NULL UNIQUE
    );
END
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE Users
    (
        UserId INT IDENTITY PRIMARY KEY,
        Email NVARCHAR(150) NOT NULL UNIQUE,
        FirstName NVARCHAR(60) NOT NULL,
        LastName NVARCHAR(60) NOT NULL,
        BirthDate DATE NULL,
        PasswordHash NVARCHAR(500) NOT NULL,
        PasswordSalt NVARCHAR(500) NULL,
        PasswordIterations INT NULL,
        RoleId INT NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        MustChangePassword BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
        CreatedBy INT NULL,
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
    );
END
GO

IF OBJECT_ID(N'dbo.PasswordHistories', N'U') IS NULL
BEGIN
    CREATE TABLE PasswordHistories
    (
        PasswordHistoryId BIGINT IDENTITY PRIMARY KEY,
        UserId INT NOT NULL,
        PasswordHash NVARCHAR(500) NOT NULL,
        PasswordSalt NVARCHAR(500) NULL,
        PasswordIterations INT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
        CONSTRAINT FK_PasswordHistories_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

IF OBJECT_ID(N'dbo.PasswordResetTokens', N'U') IS NULL
BEGIN
    CREATE TABLE PasswordResetTokens
    (
        TokenId BIGINT IDENTITY PRIMARY KEY,
        UserId INT NOT NULL,
        TokenHash CHAR(64) NOT NULL,
        ExpiresAt DATETIME2 NOT NULL,
        UsedAt DATETIME2 NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
        CONSTRAINT FK_ResetTokens_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

IF OBJECT_ID(N'dbo.AuditLogs', N'U') IS NULL
BEGIN
    CREATE TABLE AuditLogs
    (
        AuditLogId BIGINT IDENTITY PRIMARY KEY,
        UserId INT NULL,
        EventType NVARCHAR(50) NOT NULL,
        Description NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = N'Admin')
    INSERT INTO Roles (RoleName) VALUES (N'Admin');

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = N'User')
    INSERT INTO Roles (RoleName) VALUES (N'User');
GO
