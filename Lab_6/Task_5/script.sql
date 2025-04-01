CREATE DATABASE agri;
GO

USE agri;
GO

CREATE TABLE AgriculturalEnterprise (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    OwnershipType NVARCHAR(1) CHECK (OwnershipType IN ('Д', 'П', 'К')) NOT NULL,
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