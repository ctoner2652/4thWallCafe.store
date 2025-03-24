# ☕ 4Th Wall Café

Welcome to the 4Th Wall Café repository! This is a full-stack café management system built with .NET 8 featuring a robust Web API backend and an MVC frontend. The system supports everything from order handling and cart management to role-based user access and dynamic API user authentication. It's built to be modular, extensible, and ideal for both learning and small business operations.

---

## Features

- **Web API + MVC Frontend**: Clean separation between API logic and UI interaction.
- **JWT Authentication**: Secure API endpoints for authorized users only.
- **Scoped Dependency Injection**: Proper service lifetime handling to avoid memory leaks and stale data.
- **Custom Role Management**: Support for manager, accountant, and API-only user roles.
- **Token Generator**: Built-in admin interface to generate and manage API access tokens.
- **Cart & Checkout Flow**: Session-based cart with live total calculations.
- **Reporting Dashboard**: View orders, revenue, and sales insights.
- **Clean Error Handling**: Custom error page with global fallback via middleware.
- **Unit Tested**: Services are tested using NUnit and mock repositories.

---

## Technologies Used

- **C# / .NET 8**
- **ASP.NET Core (MVC + Web API)**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Bootstrap 5**
- **NUnit**

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/4ThWallCafe.git
cd 4ThWallCafe
```

### 2. Setup the Database

You will need a SQL Server instance. A `.bak` file of the database can be provided to restore from.

Update your `appsettings.json` file in both the API and MVC projects:

```json
"ConnectionStrings": {
  "DefaultConnection": "your-connection-string-here"
}
```

### 3. Run the API Project

Navigate to the API project folder:

```bash
cd 4ThWallCafe.API
dotnet run
```

This will start the API (usually on `https://localhost:5001`).

### 4. Run the MVC Frontend

Navigate to the MVC project folder:

```bash
cd 4ThWallCafe.MVC
dotnet run
```

---

## Screenshots

> (Add these as you go)
- Dashboard with summary boxes
- API token manager
- Reporting page
- Error page fallback
- Cart & Checkout flow

---

## Architecture Overview

- **MVC Project** → Interacts with API to handle all user-facing functionality.
- **API Project** → Handles all core business logic, token auth, and data access.
- **Application Layer** → Contains services (business logic).
- **Data Layer** → Contains EF Core DbContext and repository interfaces/implementations.
- **Core Entities** → Shared domain models used by both API and MVC.

---

## Example Services

Each service is registered as `Scoped`:

```csharp
builder.Services.AddScoped<ICafeOrderService, CafeOrderService>();
builder.Services.AddScoped<ICafeOrderRepository, CafeOrderRepository>();
```

All services are injected using constructor injection and have their own `ILogger<T>` for logging.

---

## Author

Developed with 💻 by Colby (aka @ctoner2652)

---

## License

MIT License — free to use and modify.
