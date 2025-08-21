using TaskFlow.DTOs;

namespace TaskFlow.Hubs
{
    /// <summary>
    /// Defines the methods that a SignalR client can call on the TaskFlow Hub.
    /// This interface enables strongly typed SignalR hubs.
    /// </summary>
    public interface ITaskFlowClient
    {
        /// <summary>
        /// Notifies clients that a card has been moved.
        /// </summary>
        /// <param name="cardId">The ID of the moved card.</param>
        /// <param name="newListId">The ID of the new list the card belongs to.</param>
        /// <param name="oldListId">The ID of the old list the card belonged to.</param>
        Task CardMoved(int cardId, int newListId, int oldListId);

        /// <summary>
        /// Notifies clients that a new comment has been added to a card.
        /// </summary>
        /// <param name="commentDto">The DTO of the newly added comment.</param>
        Task CommentAdded(CommentDto commentDto);

        /// <summary>
        /// Notifies clients that a user has been assigned to a card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <param name="userDto">The DTO of the user assigned.</param>
        Task UserAssignedToCard(int cardId, UserDto userDto);

        /// <summary>
        /// Notifies clients that a user has been unassigned from a card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <param name="userId">The ID of the user unassigned.</param>
        Task UserUnassignedFromCard(int cardId, string userId);

        /// <summary>
        /// Notifies clients that a card has been updated.
        /// </summary>
        /// <param name="cardDto">The DTO of the updated card.</param>
        Task CardUpdated(CardDto cardDto);

        /// <summary>
        /// Notifies clients that a list has been updated.
        /// </summary>
        /// <param name="listDto">The DTO of the updated list.</param>
        Task ListUpdated(ListDto listDto);

        /// <summary>
        /// Notifies clients that a board has been updated.
        /// </summary>
        /// <param name="boardDto">The DTO of the updated board.</param>
        Task BoardUpdated(BoardDto boardDto);

        /// <summary>
        /// Notifies clients that a workspace has been updated.
        /// </summary>
        /// <param name="workspaceDto">The DTO of the updated workspace.</param>
        Task WorkspaceUpdated(WorkspaceDto workspaceDto);

        /// <summary>
        /// Notifies clients that a card has been deleted.
        /// </summary>
        /// <param name="cardId">The ID of the deleted card.</param>
        /// <param name="listId">The ID of the list the card belonged to.</param>
        Task CardDeleted(int cardId, int listId);

        /// <summary>
        /// Notifies clients that a list has been deleted.
        /// </summary>
        /// <param name="listId">The ID of the deleted list.</param>
        /// <param name="boardId">The ID of the board the list belonged to.</param>
        Task ListDeleted(int listId, int boardId);

        /// <summary>
        /// Notifies clients that a board has been deleted.
        /// </summary>
        /// <param name="boardId">The ID of the deleted board.</param>
        /// <param name="workspaceId">The ID of the workspace the board belonged to.</param>
        Task BoardDeleted(int boardId, int workspaceId);
    }
}