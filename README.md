# Financial Solution

A financial transaction management solution. The repository currently contains a .NET backend API for importing and managing transaction files. A `UI/` folder is planned for the user interface; its framework and setup instructions can be added once the UI project is in place.

## Repository Structure

```text
Financial_Solution/
├── FinancialBackendApi/
│   ├── FinancialBackendApi.slnx
│   └── FinancialBackendApi/
│       ├── Controllers/
│       ├── Data/
│       ├── Models/
│       ├── Services/
│       ├── ExampleFiles/
│       └── README.md
├── UI/                         # Planned: user interface code
├── LICENSE
└── README.md
```

## Components

### Backend API

The ASP.NET Core API accepts financial transaction CSV files, stores file metadata and transaction details, and provides endpoints for retrieving and managing uploaded files.

See the [backend API README](FinancialBackendApi/FinancialBackendApi/README.md) for prerequisites, configuration, run instructions, and endpoint details.

### User Interface

The `FinancialUI/` folder is planned but is not in the repository yet. Once the UI project is added, document its framework, prerequisites, and run commands here or in a dedicated `FinancialUI/README.md`, and link to that documentation from this section.

## Getting Started

### Run the backend API

Prerequisites:

- .NET SDK 10
- A database configured for the API's `DefaultConnection`

From the repository root:

```bash
dotnet restore FinancialBackendApi/FinancialBackendApi.slnx
dotnet run --project FinancialBackendApi/FinancialBackendApi/FinancialBackendApi.csproj
```

In the Development environment, Swagger UI is available at:

```text
https://localhost:<port>/swagger
```

See the [backend API README](FinancialBackendApi/FinancialBackendApi/README.md#configuration) for database configuration details.

## License

This project is licensed under the MIT license. See [LICENSE](LICENSE).
