# 📚 Library Management System

## Author: **Mohammad Alfayoume**

## Time Estimation: One Day

## Actual Time: Two Days

## AI Tools
* ChatGPT 4o

## ChatGPT history 
- [Link](https://chatgpt.com/share/68cf0cb3-28e4-8012-889b-c3c1b3b3ada2)

## Prompts
```
You are an expert .NET Core architect. Generate a full project setup for a .NET Core Web API application using Onion Architecture, Domain-Driven Design (DDD), Repository Pattern, EF Core, and ADO.NET with Stored Procedures.. The solution must include the following layers:

API Layer – ASP.NET Core Web API project (controllers, exception handling, DI setup).

Application Layer.

Infrastructure Layer.

Domain Layer.

Requirements:

Use Clean Code principles and SOLID design.

Configure Dependency Injection to wire up layers correctly.

Add an example feature (e.g., "Product" entity with CRUD endpoints).

Apply best practices for error handling, logging, and validation (FluentValidation).

Provide folder structure and explain the purpose of each layer.

Use .NET 8 and EF Core latest stable version.
```

## Challenges
* Set up application skeleton code

This project uses SQL Server as the database. Below is the schema and stored procedure script.

## Database Setup
* Create a new Database using SSMS with the name LMS
* Add new query and run the script below
## Database Script

```sql
USE [LMS]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 9/20/2025 10:52:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
    [Id] [uniqueidentifier] NOT NULL,
      NOT NULL,
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    [CreatedAt] [datetime] NOT NULL,
    [ModifiedAt] [datetime] NULL,
    [IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Category](
    [Id] [uniqueidentifier] NOT NULL,
      NOT NULL,
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    [CreatedAt] [datetime] NOT NULL,
    [ModifiedAt] [datetime] NULL,
    [IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BookCategory](
    [BookId] [uniqueidentifier] NOT NULL,
    [CategoryId] [uniqueidentifier] NOT NULL,
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [ModifiedBy] [uniqueidentifier] NULL,
    [CreatedAt] [datetime] NOT NULL,
    [ModifiedAt] [datetime] NULL,
    [IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_BookCategory] PRIMARY KEY CLUSTERED ([BookId] ASC, [CategoryId] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BookCategory]  
ADD CONSTRAINT [FK_BookCategory_Book_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([Id])

ALTER TABLE [dbo].[BookCategory]  
ADD CONSTRAINT [FK_BookCategory_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO

/****** Stored Procedure ******/
CREATE PROCEDURE [dbo].[GetAllBooksWithCategories]
AS
BEGIN
    SELECT 
        b.Id AS BookId,
        b.Name AS BookName,
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM Book b
    LEFT JOIN BookCategory bc ON b.Id = bc.BookId
    LEFT JOIN Category c ON bc.CategoryId = c.Id
    WHERE ISNULL(b.IsDeleted, 0) = 0
    ORDER BY b.CreatedAt DESC
END
GO
```

## Resources
- [onion architecture](https://youtu.be/KqWNtCpjUi8?si=N66kYupT8uN1_D_z)
- [fluentvalidation](https://youtu.be/vaDDB7BpEgQ?si=kr2QSIBMIPl0tDeD)
