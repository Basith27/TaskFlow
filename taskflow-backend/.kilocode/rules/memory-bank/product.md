# Product – TaskFlow Backend

## Why this project exists
The TaskFlow backend exists to provide a robust and scalable foundation for a real-time collaborative task management application. It aims to enable multiple users to work together seamlessly on boards and cards, with instant updates reflecting changes across all connected clients.

## Problems it solves
- **Real-time Synchronization:** Addresses the challenge of keeping multiple client applications synchronized with the latest state of boards and cards without manual refreshes.
- **Data Persistence:** Provides a reliable mechanism for storing and retrieving project data (workspaces, boards, lists, cards, comments, user assignments).
- **User Management:** Handles secure user authentication and authorization, ensuring that only authorized users can access and modify data.
- **Scalability:** Designed to support a growing number of users and concurrent collaborative sessions.

## How it should work
The backend should expose a RESTful API for standard, stateless operations like user authentication, initial board data fetching, and creating new boards. It will utilize an ASP.NET Core SignalR Hub for handling all real-time, stateful communication, broadcasting events like `CardMoved`, `NewComment`, or `UserAssigned` to all connected clients on a specific board.

## User experience goals
- **Instant Feedback:** Users should see changes made by themselves or others reflected immediately in their UI.
- **Seamless Collaboration:** The system should feel responsive and allow multiple users to interact with the same data concurrently without conflicts.
- **Secure Access:** Users should feel confident that their data is secure and accessible only to authorized individuals.
- **Reliable Performance:** The backend should handle a high volume of requests and real-time updates efficiently, ensuring a smooth user experience.