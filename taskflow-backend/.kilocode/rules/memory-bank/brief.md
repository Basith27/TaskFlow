# Project Brief – TaskFlow Backend

TaskFlow’s backend is built with ASP.NET Core Web API and SignalR to provide a real-time collaborative environment. It manages user authentication, workspace and board data, card state, and user assignments.

The backend is responsible for both RESTful operations (login, create board, fetch data) and real-time broadcasting (via SignalR) to keep clients synchronized.

### Goals:
- Handle secure authentication and authorization
- Persist and serve project data (boards, cards, comments, etc.)
- Broadcast real-time updates to clients via SignalR
- Maintain consistency and reliability during collaborative edits

### Technologies:
- ASP.NET Core Web API
- SignalR for real-time communication
- Entity Framework Core for ORM
- PostgreSQL as the primary database
- Identity system for secure user access

The backend is modular and adheres to clean architecture principles, separating business logic into services and using DTOs for safe data transport.
