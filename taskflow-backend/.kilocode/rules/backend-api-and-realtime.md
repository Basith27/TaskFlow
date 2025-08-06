# API and SignalR Guidelines

## REST API (ASP.NET Core)
- Use `[ApiController]` and attribute routing.
- Validate all incoming DTOs with `DataAnnotations`.
- Return standard HTTP status codes with consistent response models.

## SignalR
- Use strongly typed Hubs and clients.
- Handle connection lifecycle events (onConnected, onDisconnected).
- Broadcast updates only after DB commit.

## Example
```csharp
public async Task MoveCardAsync(int cardId, string listName) {
    await _cardService.UpdateListAsync(cardId, listName);
    await _hubContext.Clients.All.SendAsync("CardMoved", cardId, listName);
}
