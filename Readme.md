# Task Manager API

A production-grade RESTful API for managing tasks and projects, built with ASP.NET Core. This project demonstrates a clean N-tier architecture, robust error handling, and unit testing.

## Features

- **N-tier Architecture:** Separation of concerns using Controllers, Services, and DTOs.
- **Secure Authentication:** Password hashing using SHA256 and Salt.
- **Global Exception Handling:** Centralized error handling using `IExceptionHandler`.
- **Unit Testing:** Comprehensive tests using xUnit and In-Memory Database.
- **API Documentation:** Interactive API docs powered by Scalar.

## Tech Stack

- **Backend:** .NET 10 & ASP.NET Core Web API
- **Database:** SQLite (via Entity Framework Core)
- **Testing:** xUnit, Moq, EF Core In-Memory
- **Documentation:** Scalar OpenAPI

## ️ Architecture

- **Controllers:** Handle HTTP requests and responses.
- **Services:** Contain business logic and interact with the database.
- **DTOs:** Ensure secure data transfer and prevent over-posting.
- **Interfaces:** Enable loose coupling and easy unit testing.

##  How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/YOUR_USERNAME/TaskManager.Api.git
   ```
2. Navigate to the project directory:
   ```bash
   cd TaskManager.Api
   ```
3. Restore dependencies and run:
   ```bash
   dotnet restore
   dotnet run
   ```
4. Open Scalar API documentation in your browser:
   ```text
   https://localhost:5001/scalar/v1
   ```

##  Running Tests

To run the unit tests, execute the following command:
```bash
dotnet test
```

## Scalar Reference

