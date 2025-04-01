CREATE DATABASE AgriDB ON PRIMARY 
(
    NAME = AgriDB,
    FILENAME = 'C:\Users\niksh\source\repos\Lab_6\Task_7\AgriDB.mdf',
    SIZE = 10MB,
    MAXSIZE = 50MB,
    FILEGROWTH = 5MB
)
GO

USE AgriDB;
GO

CREATE TABLE AgriculturalEnterprise (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    OwnershipType NVARCHAR(1) NOT NULL,
    LandArea INT NOT NULL,
    EmployeesCount INT NOT NULL
);
GO

INSERT INTO AgriculturalEnterprise (Name, OwnershipType, LandArea, EmployeesCount) 
VALUES 
    (N'Зоря', N'Д', 300, 120),
    (N'Росинка', N'К', 174, 27),
    (N'Петренко', N'П', 56, 6);
GO
