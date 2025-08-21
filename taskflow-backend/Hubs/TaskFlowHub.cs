using Microsoft.AspNetCore.SignalR;
using TaskFlow.DTOs;
using System.Threading.Tasks;

namespace TaskFlow.Hubs
{
    /// <summary>
    /// SignalR Hub for real-time communication in the TaskFlow application.
    /// This hub enables clients to receive real-time updates on various entities
    /// like cards, comments, and user assignments.
    /// </summary>
    public class TaskFlowHub : Hub<ITaskFlowClient>
    {
        /// <summary>
        /// Allows a client to join a specific board group.
        /// Clients in the same board group will receive updates related to that board.
        /// </summary>
        /// <param name="boardId">The ID of the board to join.</param>
        public async Task JoinBoardGroup(int boardId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Board-{boardId}");
            // Optionally, send a message to the joining client or group
        }

        /// <summary>
        /// Allows a client to leave a specific board group.
        /// </summary>
        /// <param name="boardId">The ID of the board to leave.</param>
        public async Task LeaveBoardGroup(int boardId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Board-{boardId}");
            // Optionally, send a message to the leaving client or group
        }

        // Placeholder methods for broadcasting updates.
        // These will be called by services after data persistence.

        /// <summary>
        /// Broadcasts a card movement update to all clients in the relevant board group.
        /// </summary>
        /// <param name="cardId">The ID of the moved card.</param>
        /// <param name="newListId">The ID of the new list the card belongs to.</param>
        /// <param name="oldListId">The ID of the old list the card belonged to.</param>
        public async Task SendCardMoved(int cardId, int newListId, int oldListId)
        {
            // Determine the board ID from the cardId or other context
            // For now, assuming a direct broadcast to all clients for simplicity,
            // but in a real scenario, this would be targeted to a specific board group.
            await Clients.All.CardMoved(cardId, newListId, oldListId);
        }

        /// <summary>
        /// Broadcasts a new comment update to all clients in the relevant board group.
        /// </summary>
        /// <param name="commentDto">The DTO of the newly added comment.</param>
        public async Task SendCommentAdded(CommentDto commentDto)
        {
            // Assuming commentDto contains CardId, which can be used to determine BoardId
            // For now, broadcasting to all, but should be targeted to a board group.
            await Clients.All.CommentAdded(commentDto);
        }

        /// <summary>
        /// Broadcasts a user assignment update to all clients in the relevant board group.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <param name="userDto">The DTO of the user assigned.</param>
        public async Task SendUserAssignedToCard(int cardId, UserDto userDto)
        {
            // Target specific board group
            await Clients.All.UserAssignedToCard(cardId, userDto);
        }

        /// <summary>
        /// Broadcasts a user unassignment update to all clients in the relevant board group.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <param name="userId">The ID of the user unassigned.</param>
        public async Task SendUserUnassignedFromCard(int cardId, string userId)
        {
            // Target specific board group
            await Clients.All.UserUnassignedFromCard(cardId, userId);
        }

        /// <summary>
        /// Broadcasts a card update to all clients in the relevant board group.
        /// </summary>
        /// <param name="cardDto">The DTO of the updated card.</param>
        public async Task SendCardUpdated(CardDto cardDto)
        {
            // Target specific board group
            await Clients.All.CardUpdated(cardDto);
        }

        /// <summary>
        /// Broadcasts a list update to all clients in the relevant board group.
        /// </summary>
        /// <param name="listDto">The DTO of the updated list.</param>
        public async Task SendListUpdated(ListDto listDto)
        {
            // Target specific board group
            await Clients.All.ListUpdated(listDto);
        }

        /// <summary>
        /// Broadcasts a board update to all clients in the relevant workspace group.
        /// </summary>
        /// <param name="boardDto">The DTO of the updated board.</param>
        public async Task SendBoardUpdated(BoardDto boardDto)
        {
            // Target specific workspace group
            await Clients.All.BoardUpdated(boardDto);
        }

        /// <summary>
        /// Broadcasts a workspace update to all clients in the relevant workspace group.
        /// </summary>
        /// <param name="workspaceDto">The DTO of the updated workspace.</param>
        public async Task SendWorkspaceUpdated(WorkspaceDto workspaceDto)
        {
            // Target specific workspace group
            await Clients.All.WorkspaceUpdated(workspaceDto);
        }

        /// <summary>
        /// Broadcasts a card deletion update to all clients in the relevant board group.
        /// </summary>
        /// <param name="cardId">The ID of the deleted card.</param>
        /// <param name="listId">The ID of the list the card belonged to.</param>
        public async Task SendCardDeleted(int cardId, int listId)
        {
            // Target specific board group
            await Clients.All.CardDeleted(cardId, listId);
        }

        /// <summary>
        /// Broadcasts a list deletion update to all clients in the relevant board group.
        /// </summary>
        /// <param name="listId">The ID of the deleted list.</param>
        /// <param name="boardId">The ID of the board the list belonged to.</param>
        public async Task SendListDeleted(int listId, int boardId)
        {
            // Target specific board group
            await Clients.All.ListDeleted(listId, boardId);
        }

        /// <summary>
        /// Broadcasts a board deletion update to all clients in the relevant workspace group.
        /// </summary>
        /// <param name="boardId">The ID of the deleted board.</param>
        /// <param name="workspaceId">The ID of the workspace the board belonged to.</param>
        public async Task SendBoardDeleted(int boardId, int workspaceId)
        {
            // Target specific workspace group
            await Clients.All.BoardDeleted(boardId, workspaceId);
        }
    }
}