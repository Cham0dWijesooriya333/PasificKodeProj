# PasificKodeA
Simple ASP.NET Web API for managing Departments and Employees using raw ADO.NET with SQL Server.
Tech stack
•	.NET 10
•	C#
•	SQL Server (uses plain SQL via System.Data.SqlClient)
•	No ORM (Dapper/EF) used

# Overview

This repository exposes CRUD endpoints for Departments and Employees. Departments cannot be deleted while Employees reference them; the API returns 409 Conflict with a clear message in that case.
# Prerequisites
•	.NET 10 SDK
•	SQL Server instance (localdb, SQL Server Express)
•	A connection string named DefaultConnection in appsettings.*.json
Getting started
1.	Clone the repo git clone <repo-url>
2.	Configure the database connection
•	Open appsettings.Development.json (or appsettings.json)
•	Set ConnectionStrings:DefaultConnection to your SQL Server connection string.
Example:
# "ConnectionStrings": {
#    "DefaultConnection": "Server=(localdb)\\ExpressSql;Database=CompanyDB;Trusted_Connection=True;"
#   }

# 4.	Create the database schema (example) Execute the following SQL against your SQL Server to create tables the app expects:
CREATE DATABASE CompanyDB;
GO
USE CompanyDB;
GO

CREATE TABLE Departments (
  DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
  DepartmentCode NVARCHAR(20) NOT NULL,
  DepartmentName NVARCHAR(100) NOT NULL
);

CREATE TABLE Employees (
  EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
  FirstName NVARCHAR(50) NOT NULL,
  LastName NVARCHAR(50) NOT NULL,
  Email NVARCHAR(256) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Salary DECIMAL(18,2) NOT NULL,
  DepartmentId INT NOT NULL,
  CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
   );
   
   Note: Age is not stored — compute it in the frontend from DateOfBirth.

# Tested API endpoints (via Swagger UI) >>

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

# The following endpoints have been validated in Swagger UI:
•	Departments
•	GET /api/departments
•	GET /api/departments/{id}
•	POST /api/departments
•	PUT /api/departments/{id}
•	DELETE /api/departments/{id} — returns 409 Conflict if employees reference the department
•	Employees
•	GET /api/employees
•	GET /api/employees/{id}
•	POST /api/employees
•	PUT /api/employees/{id}
•	DELETE /api/employees/{id}
Use Swagger UI to exercise these endpoints and view request/response examples.
# Notes & recommendations
•	Age should be displayed/calculated on the client from DateOfBirth to avoid inconsistencies.
For production, consider:
•	Using explicit SqlParameter types instead of AddWithValue for predictable types/performance.
•	Introducing migrations or a schema versioning approach.
•	Adding integration tests (Postman collection / automated tests).
•	If business logic allows, consider DB-level ON DELETE CASCADE or app-driven reassignments — but be deliberate about data-loss implications.
Contributing
•	Open issues or PRs. Keep changes focused and include tests where applicable.
