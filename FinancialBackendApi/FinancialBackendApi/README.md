# Financial Backend API

This ASP.NET Core API imports and manages CSV files containing financial transactions. It persists file metadata and transaction details, and provides endpoints for listing, retrieving, updating, and deleting uploaded files.

For an overview of the full repository and its planned UI, see the [solution README](../../README.md).

## Technologies

- ASP.NET Core 10 / .NET SDK 10
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Features

- Upload and validate transaction CSV files
- Store file metadata and transaction rows
- List files with pagination
- Retrieve a file and its transactions
- Update file status and delete uploaded files
- Explore the API with Swagger in Development

## Run Locally

Prerequisites:

- .NET SDK 10
- A database available through the `DefaultConnection` connection string

From the `FinancialBackendApi/` directory (the directory containing `FinancialBackendApi.slnx`):

```bash
dotnet restore FinancialBackendApi.slnx
dotnet run --project FinancialBackendApi/FinancialBackendApi.csproj
```

Configure `ConnectionStrings:DefaultConnection` in the API configuration or through the standard ASP.NET Core configuration providers before starting the API. The application fails at startup if the connection string is missing.

In the Development environment, open Swagger UI at:

```text
https://localhost:<port>/swagger
```

## API Endpoints

### Files

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/file?page=1&pageSize=10` | List uploaded files with pagination |
| `GET` | `/api/file/{id}` | Get a file and its associated transactions |
| `GET` | `/api/file/{id}/details` | Get transaction details for a file |
| `POST` | `/api/file` | Upload a CSV file as multipart form data |
| `PUT` | `/api/file/{id}` | Update a file's status |
| `DELETE` | `/api/file/{id}` | Delete a file and its related records |

### Financial

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/financial/types` | Get sample financial transaction types |
| `GET` | `/api/financial/{id}` | Get a sample financial entry by ID |

## CSV Format

Uploaded files use this header and column order:

```csv
Date,Amount,Description,Type,Category,Account
2026-09-01,-1450.00,Apartment rent,Expense,Housing,Checking
2026-09-02,-78.42,Weekly groceries,Expense,Groceries,Checking
```

Sample files are available in `ExampleFiles/`:

- `bank_transactions.csv`
- `credit_card_transactions.csv`

## Data Model

The API uses Entity Framework Core with a `FinancialDbContext`:

- `FileHeader` stores file metadata.
- `FileDetail` stores transaction rows.
- One `FileHeader` can have many `FileDetail` records.

## License

This project is licensed under the MIT license. See the repository's [LICENSE](../../LICENSE).
