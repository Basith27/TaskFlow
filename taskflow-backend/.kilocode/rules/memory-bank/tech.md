# Tech – TaskFlow Backend

## Technologies Used
- **Backend Framework:** ASP.NET Core 8.0 Web API
- **Real-time Communication:** ASP.NET Core SignalR
- **Object-Relational Mapper (ORM):** Entity Framework Core
- **Database:** MySQL
- **API Documentation:** Swashbuckle.AspNetCore (Swagger/OpenAPI)
- **Authentication/Authorization:** ASP.NET Core Identity

## Development Setup
- **SDK:** .NET 8.0 SDK
- **IDE:** Visual Studio Code (or Visual Studio)
- **Package Manager:** NuGet

## Technical Constraints
- Adherence to .NET 8.0 framework.
- Real-time updates must be efficient and scalable for collaborative environments.

## Dependencies
- `Microsoft.AspNetCore.OpenApi`
- `Swashbuckle.AspNetCore`
- `Microsoft.EntityFrameworkCore`
- `Pomelo.EntityFrameworkCore.MySql`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.EntityFrameworkCore.Design`

## Tool Usage Patterns
- **Entity Framework Core Migrations:** For database schema management.
- **Swagger UI:** For testing API endpoints during development.
- **SignalR Client Libraries:** Frontend will use `@microsoft/signalr` for real-time communication.