# Financial Backend API

This project is a backend service for processing and managing financial transaction files. It provides a REST API for uploading CSV files, storing file metadata, retrieving records, updating statuses, and exposing simple financial transaction data.

## Technologies

- ASP.NET Core 10
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- C#

## Project Purpose

The application is intended to help manage financial file imports from sources such as bank statements and credit card CSV exports. A user can upload a file, validate the contents, save the records to a database, and then query the saved file data or update its status through API requests.

## Main Features

- CSV upload and validation
- File metadata persistence
- File detail retrieval
- Status updates for uploaded files
- Pagination for file listings
- Example transaction and category endpoints
- Swagger-based API documentation in development

## Database Model

The data model includes two main entities:

- `FileHeader`: stores file metadata
- `FileDetail`: stores each transaction row associated with a file

The relationship is defined as a one-to-many link from a file header to its transaction details.

## Expected CSV Format

```csv
Date,Amount,Description,Type,Category,Account
2026-09-01,-1450.00,Apartment rent,Expense,Housing,Checking
2026-09-02,-78.42,Weekly groceries,Expense,Groceries,Checking
```

The application reads each line as a transaction entry and maps the values to:

- Date
- Amount
- Description
- Type
- Category
- Account

## Endpoint Summary

### Files

- `GET /api/file`
- `GET /api/file/{id}`
- `GET /api/file/{id}/details`
- `POST /api/file`
- `PUT /api/file/{id}`
- `DELETE /api/file/{id}`

### Financial

- `GET /api/financial/types`
- `GET /api/financial/{id}`

## Configuration

The application uses SQL Server and expects the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Financials;Trusted_Connection=True;TrustServerCertificate=True;Timeout=30;"
}
```

## Running the API

```bash
dotnet restore
dotnet build
dotnet run
```

Then browse the Swagger UI at:

```text
https://localhost:<port>/swagger
```

## Example Data

Example CSV files are included under the project:

- `ExampleFiles/bank_transactions.csv`
- `ExampleFiles/credit_card_transactions.csv`

These examples can be used to test the upload and parsing flow.

## Notes

This backend is focused on data ingestion and persistence for financial file processing. It is a practical example of a .NET API that combines EF Core, file upload handling, and RESTful service design.
