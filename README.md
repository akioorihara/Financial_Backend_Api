# Financial Backend API

A .NET 10 backend service for importing, storing, and querying financial transaction files. The project is designed to support CSV-based financial data ingestion and expose a small REST API for file management, transaction metadata, and sample financial categories.

## Overview

This solution contains a backend API built with ASP.NET Core and Entity Framework Core. It allows users to upload CSV files containing financial transactions, persist them in a SQL Server database, and retrieve file-level or transaction-level records through API endpoints.

The application is focused on the backend service layer and provides the following capabilities:

- Import CSV files with financial transaction records
- Persist file metadata and transaction details in a relational database
- Query uploaded files with pagination
- Retrieve file details by ID
- Update a file's processing status
- Delete existing uploaded file records
- Expose a simple financial transaction category API
- Document endpoints using Swagger/OpenAPI in development

## Tech Stack

- ASP.NET Core 10
- C#
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- .NET SDK 10

## Solution Structure

```text
Financial_Backend_Api/
├── README.md
├── LICENSE
└── FinancialBackendApi/
    └── FinancialBackendApi/
        ├── Controllers/
        │   ├── FileController.cs
        │   ├── FinancialController.cs
        │   └── WeatherForecastController.cs
        ├── Data/
        │   └── FinancialDbContext.cs
        ├── Models/
        │   ├── FinancialTransactions.cs
        │   ├── DTOs/
        │   └── Entities/
        ├── Services/
        │   ├── FileService.cs
        │   └── IFileService.cs
        ├── ExampleFiles/
        │   ├── bank_transactions.csv
        │   └── credit_card_transactions.csv
        ├── Migrations/
        ├── appsettings.json
        ├── appsettings.Development.json
        ├── Program.cs
        ├── FinancialBackendApi.csproj
        └── FinancialBackendApi.http
```

## Core Features

### File Upload and Parsing
The API accepts uploaded CSV files and validates them before storing them in the database.

The expected CSV format is:

```csv
Date,Amount,Description,Type,Category,Account
2026-09-01,-1450.00,Apartment rent,Expense,Housing,Checking
2026-09-02,-78.42,Weekly groceries,Expense,Groceries,Checking
```

Each row is mapped into a `FileDetail` record containing:

- Date
- Amount
- Description
- Type
- Category
- Account

### File Metadata Storage
Each uploaded CSV is associated with a `FileHeader` entry that stores:

- ID
- File name
- Created timestamp
- Last updated timestamp
- Current status

### File Status Lifecycle
Current file processing status values are managed through the API and can be updated after uploading.

### File Retrieval
The API supports:

- List files with pagination
- Get a single file alongside its transaction details
- Get raw file details for a given file record
- Delete a file record when no longer needed

## API Endpoints

### File API

- `GET /api/file?page=1&pageSize=10`  
  Returns a paginated list of uploaded files.

- `GET /api/file/{id}`  
  Returns the file header and all associated transactions.

- `GET /api/file/{id}/details`  
  Returns the file details for a specific uploaded file.

- `POST /api/file`  
  Uploads a CSV file as multipart form data.

- `PUT /api/file/{id}`  
  Updates the file header status.

- `DELETE /api/file/{id}`  
  Removes a file and its related records.

### Financial Sample API

- `GET /api/financial/types`  
  Returns sample financial transaction types such as Debit and Credit.

- `GET /api/financial/{id}`  
  Returns a sample financial entry by ID.

## Database Model

The application uses EF Core with a `FinancialDbContext` that includes:

- `FileHeaders`
- `FileDetails`

The relationship is one-to-many:

- One `FileHeader` can contain many `FileDetail` rows.

## Configuration

The application expects a SQL Server connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Financials;Trusted_Connection=True;TrustServerCertificate=True;Timeout=30;"
}
```

Before running the app, make sure SQL Server is available and the database connection is valid.

## Running the Project

From the repository root:

```bash
dotnet restore
cd FinancialBackendApi/FinancialBackendApi
dotnet build
dotnet run
```

Then open Swagger in the browser when running in development:

```text
https://localhost:<port>/swagger
```

## Example Files

Two sample CSV files are included in the project for testing and demonstration:

- `FinancialBackendApi/ExampleFiles/bank_transactions.csv`
- `FinancialBackendApi/ExampleFiles/credit_card_transactions.csv`

These files provide realistic examples of banking and credit card data that match the expected import schema.

## Notes

- The project is primarily focused on the backend API and data persistence layer.
- It currently uses SQL Server as the default relational database provider.
- Swagger is enabled for local development, making it easier to test endpoint behavior interactively.

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for details.


