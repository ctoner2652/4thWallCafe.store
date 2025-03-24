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


Before running the application, make sure your `appsettings.json` files are properly configured for both the API and MVC projects.

--- 
### Both Projects 

```json
"ConnectionStrings": {
  "DefaultConnection": "your-connection-string-here"
}
```


### 🔐 API - JWT Setup

The API requires secure JWT token authentication for protected endpoints. Add the following section to your `appsettings.json` file in the **API** project:

```json
"Jwt": {
  "Key": "here-is-my-44-character-long-key-for-my-secure-guy",
  "Issuer": "4ThWallCafe",
  "Audience": "Applications",
  "Expiration": 3000
}
```

- **Key**: Your secret signing key (must be long and secure; 44+ characters is good).
- **Issuer**: Your custom issuer name (e.g. `4ThWallCafe`).
- **Audience**: Who the token is intended for (e.g. `Applications`).
- **Expiration**: Token lifetime in minutes (e.g. `30` = 30 minutes).

---

### 🖥️ MVC - API Credentials & Base URL

In the **MVC** project, the app uses a dedicated API account to authenticate and fetch data from the API. Add this to your `appsettings.json`:

```json
"API": {
  "BaseAddress": "API base address here",
  "MVCAPIUserName": "api username here",
  "MVCAPIPassword": "ap password here!"
}
```

- **BaseAddress**: URL pointing to the deployed API.
- **MVCAPIUserName / MVCAPIPassword**: Credentials for the API user (must be a valid API-only account set up in the Identity DB).

---

✅ Make sure the API user account has been seeded into the Identity system or registered through the Auth flow!



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

- Home Screen ![image](https://github.com/user-attachments/assets/fe7bccda-f4f0-4f7b-9b79-0beaba45b6ba)

- Menu Screen ![image](https://github.com/user-attachments/assets/55a594be-e727-4806-aa4f-2991ff336f67)

- Order Screen ![image](https://github.com/user-attachments/assets/fd29deea-8a47-4c0a-a644-3537176b6635)


- Check Out Screen![image](https://github.com/user-attachments/assets/bafce69c-9287-43a4-b058-47c02703e8f3)


- Order Confirmation Screen ![image](https://github.com/user-attachments/assets/77dd5c63-3e10-49ea-b1ee-54088b760ef6)

- 

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
