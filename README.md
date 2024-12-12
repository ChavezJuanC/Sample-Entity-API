# Sample Entity API

This repository demonstrates a simple API built with **.NET** and **Entity Framework Core** for managing entities. It showcases how to implement RESTful API endpoints with database integration using Entity Framework.

## Features

- CRUD operations (Create, Read, Update, Delete) for managing entities.
- Entity Framework Core integration with a database.
- RESTful API structure following best practices.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later recommended)
- A database provider (e.g., SQL Server, SQLite)
- (Optional) An API testing tool like Postman or cURL for testing.

## Getting Started

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/ChavezJuanC/Sample-Entity-API.git
   cd Sample-Entity-API
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Configure the database connection string in the `appsettings.json` file. Example for SQL Server:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=SampleEntityDb;Trusted_Connection=True;"
   }
   ```

4. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

### Running the Application

1. Start the application:

   ```bash
   dotnet run
   ```

2. The API will be available at `https://localhost:5001` or `http://localhost:5000`.

## Tools and Libraries

- **ASP.NET Core**: Web framework for building APIs.
- **Entity Framework Core**: ORM for database management.
- **SQL Server** (or other supported databases): Data storage.
