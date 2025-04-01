# Clean Architecture Advanced

## Overview
CleanArchitecture.Advanced is a solid, robust and structured .NET application implementing Clean Architecture principles. It provides a scalable and maintainable solution, structured with an API and a client application, focused on separating concerns into distinct layers and keeping each component well decoupled from the whole architecture.

## Features
- Follows Clean Architecture principles
- Modular structure with clear separation of concerns
- Entity Framework for database management
- RESTful API implementation
- Client application using WPF and MVVM pattern
- Includes caching for optimized performance
- Supports validation and error handling
- Dependency injection for service management

## Technologies Used
- .NET Core
- Entity Framework Core
- ASP.NET Core Web API
- WPF with MVVM Pattern for Client
- AutoMapper
- Newtonsoft.Json for Serialization / Deserialization
- FluentValidation for validation handling

## Project Structure
```
CleanArchitecture.Advanced/
├── CleanArchitecture.Advanced.Api/          # API Layer
│   ├── Application/                         # Business logic, mappings and validation
│   ├── Domain/                              # Core domain entities
│   ├── Infrastructure/                      # Data access and repositories
│   ├── Presentation/                        # Controllers and endpoints
├── CleanArchitecture.Advanced.Client/       # Client application (if applicable)
│   ├── Application/                         # Business logic, mappings and validation
│   ├── Domain/                              # Client models
│   ├── Infrastructure/                      # API communication layer
│   ├── Presentation/                        # WPF Views and ViewModels
├── CleanArchitecture.Advanced.Common/       # Shared utilities and models
│   ├── Application/                         # DTOs and POCO models
│   ├── Constants/                           # Constants
│   ├── Enums/                               # Enumerations
│   ├── Exceptions/                          # Custom exceptions
│   ├── Extensions/                          # Extensions methods
```

## API Endpoints
| Method   | Endpoint                  | Description                                     |
|----------|---------------------------|-------------------------------------------------|
| GET      | /libraries                | Get all libraries                               |
| GET      | /libraries/{id}           | Get library by ID                               |
| GET      | /libraries/first          | Get first library                               |
| GET      | /libraries/select         | Get selection of libraries based on Expression  |
| POST     | /libraries                | Add a library                                   |
| POST     | /libraries/search         | Search libraries based on filter Request        |
| POST     | /libraries/get-where      | Get subset of libraries based on Expression     |
| POST     | /libraries/first          | Get first library based on Expression           |
| PUT      | /libraries                | Update a library                                |
| DELETE   | /libraries/{id}           | Delete a library                                |

## Installation
### Prerequisites
- .NET SDK 6.0 or later
- SQL Server (if using database authentication)

### Steps
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd CleanArchitecture.Advanced
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```
3. Modify `appsettings.json` to set the correct connection string for SQL Server:
   ```sh
   "DefaultConnection": "Server=(localdb)\\YourInstance;Database=YourDatabase;Trusted_Connection=True;"
   ```

## Usage
1. Run the API project:
   ```sh
   dotnet run --project CleanArchitecture.Advanced.Api
   ```
2. Run the client application in Visual Studio.
