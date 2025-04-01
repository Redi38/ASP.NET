CREATE DATABASE AgriDB2 ON PRIMARY 
(
    NAME = AgriDB2,
    FILENAME = 'C:\Users\niksh\source\repos\Lab_6\Task_9\AgriDB2.mdf',
    SIZE = 10MB,
    MAXSIZE = 50MB,
    FILEGROWTH = 5MB
)
GO

USE AgriDB2;
GO

CREATE TABLE AgriculturalEnterprise (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    OwnershipType NVARCHAR(1) NOT NULL,
    LandArea INT NOT NULL,
    EmployeesCount INT NOT NULL,
    ProductType NVARCHAR(255) NOT NULL
);
GO

INSERT INTO AgriculturalEnterprise (Name, OwnershipType, LandArea, EmployeesCount, ProductType) 
VALUES 
    (N'Зоря', N'Д', 300, 120, N'Зернові'),
    (N'Росинка', N'К', 174, 27, N'Овочі'),
    (N'Петренко', N'П', 56, 6, N'Фрукти');
GO

