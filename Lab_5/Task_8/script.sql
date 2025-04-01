    -- Створення бази даних  
    CREATE DATABASE RegistrationDB;  

    -- Використання бази даних  
    USE RegistrationDB;  

    -- Створення таблиці Users  
    CREATE TABLE Users (  
        Id INT IDENTITY(1,1) PRIMARY KEY,  
        Address NVARCHAR(255) NOT NULL,  
        Password NVARCHAR(255) NOT NULL,  
        Login NVARCHAR(100) NOT NULL UNIQUE,  
        Email NVARCHAR(150) NOT NULL UNIQUE  
    );  

