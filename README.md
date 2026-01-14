# Clean Architecture Advanced

## Overview
CleanArchitecture.Advanced is a solid, robust and structured .NET application implementing Clean Architecture principles. It provides a scalable and maintainable solution, structured with an API and a client application, focused on separating concerns into distinct layers and keeping each component well decoupled from the whole architecture.

---

## Features
- Follows Clean Architecture principles
- Modular structure with clear separation of concerns
- Entity Framework for database management
- RESTful API implementation
- Client application using WPF and MVVM pattern
- Includes caching for optimized performance
- Supports validation and error handling
- Dependency injection for service management

---

## Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────────────┐
│           Presentation Layer                │
│  (API Controllers / WPF Views & ViewModels) │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│           Application Layer                 │
│   (Services, Interfaces, Validation)        │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│          Infrastructure Layer               │
│  (Repositories, Data Access, Connectors)    │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│             Domain Layer                    │
│        (Entities, Domain Logic)             │
└─────────────────────────────────────────────┘
```

### Dependency Flow

- **Presentation** depends on **Application**
- **Application** depends on **Domain**
- **Infrastructure** implements **Application** interfaces
- **Domain** has no dependencies (pure business logic)

---

## Technologies Used

### Backend (API)

| Technology | Purpose |
|------------|---------|
| **.NET 8** | Runtime framework |
| **ASP.NET Core** | Web API framework |
| **Entity Framework Core** | ORM for data access |
| **SQL Server** | Database |
| **FluentValidation** | Input validation |
| **AutoMapper** | Object mapping |
| **Newtonsoft.Json** | JSON serialization |
| **Swagger/OpenAPI** | API documentation |

### Frontend (Client)

| Technology | Purpose |
|------------|---------|
| **WPF (.NET 8)** | Desktop UI framework |
| **MVVM Pattern** | UI architecture |
| **HttpClient** | HTTP communication |
| **Dependency Injection** | IoC container |

### Design Patterns

- **Repository Pattern**: Data access abstraction
- **Unit of Work**: Transaction management
- **Dependency Injection**: Inversion of Control
- **CQRS-like Queries**: Separation of read operations
- **Factory Pattern**: Object creation
- **Specification Pattern**: Complex query building
- **Middleware Pattern**: Cross-cutting concerns

---

## Project Structure

### **CleanArchitecture.Advanced.Api** (ASP.NET Core Web API)

```
├── Domain/
│   └── Entities/              # Domain entities (Library, Book, Author, etc.)
├── Application/
│   ├── Interfaces/            # Service and repository contracts
│   ├── Services/              # Business logic implementations
│   ├── Validation/            # FluentValidation validators
│   └── Mappings/              # AutoMapper profiles
├── Infrastructure/
│   ├── Context/               # EF Core DbContext
│   ├── Repositories/          # Data access implementations
│   ├── Caching/               # Memory cache implementations
│   ├── Data/                  # Database seeding
│   └── Middleware/            # Exception handling middleware
└── Presentation/
    └── Controllers/           # API endpoints
```

### **CleanArchitecture.Advanced.Client** (WPF Application)

```
├── Domain/
│   └── Models/                # Client-side domain models
├── Application/
│   ├── Interfaces/            # Service and connector contracts
│   ├── Services/              # UI business logic
│   ├── Validation/            # Client-side validation
│   └── Mappings/              # AutoMapper profiles
├── Infrastructure/
│   └── Connectors/            # HTTP API connectors
└── Presentation/
    ├── Views/                 # XAML views
    ├── ViewModels/            # MVVM ViewModels
    └── Interfaces/            # ViewModel contracts
```

### **CleanArchitecture.Advanced.Common** (Shared Library)

```
├── Application/
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Requests/              # API request models
│   └── Responses/             # API response wrappers
├── Enums/                     # Shared enumerations
├── Exceptions/                # Custom exceptions
├── Extensions/                # Extension methods
└── Constants/                 # Application constants
```

---

## Configuration

### API Configuration (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=true;"
  },
  "AllowedOrigins": [
    "https://yourdomain.com",
    "https://www.yourdomain.com"
  ],
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Client Configuration

Update `CommonConstants.cs` in the Common project:

```csharp
public const string WebServiceEndpoint = "https://localhost:7001/";
```

---

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

---

## Installation
### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/andreavallati/CleanArchitecture.Advanced.git
   cd CleanArchitecture.Advanced
   ```

2. **Configure the database connection**
   
   Edit `appsettings.json` in the API project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=true;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd CleanArchitecture.Advanced.Api
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run --project CleanArchitecture.Advanced.Api
   ```
   
   API will be available at: `https://localhost:7001`

5. **Run the WPF Client**
   ```bash
   dotnet run --project CleanArchitecture.Advanced.Client
   ```

---

<div align="center">

**Happy Coding!**

</div>
