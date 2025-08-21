using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services; // Add this for CommentService, CardService, ListService, BoardService, WorkspaceService

namespace TaskFlow.Controllers
{
    /// <summary>
    /// API controller for managing comments on cards.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly CardService _cardService; // Needed to verify card ownership
        private readonly ListService _listService; // Needed to verify list ownership
        private readonly BoardService _boardService; // Needed to verify board ownership
        private readonly WorkspaceService _workspaceService; // Needed to verify workspace ownership
        private readonly UserService _userService; // Needed to retrieve user for CommentDto

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service for business logic.</param>
        /// <param name="cardService">The card service for verifying card ownership.</param>
        /// <param name="listService">The list service for verifying list ownership.</param>
        /// <param name="boardService">The board service for verifying board ownership.</param>
        /// <param name="workspaceService">The workspace service for verifying workspace ownership.</param>
        /// <param name="userService">The user service for retrieving user details.</param>
        public CommentsController(CommentService commentService, CardService cardService, ListService listService, BoardService boardService, WorkspaceService workspaceService, UserService userService)
        {
            _commentService = commentService;
            _cardService = cardService;
            _listService = listService;
            _boardService = boardService;
            _workspaceService = workspaceService;
            _userService = userService;
        }

        /// <summary>
        /// Gets all comments for a specific card for the authenticated user.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <returns>A list of CommentDto objects.</returns>
        [HttpGet("byCard/{cardId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByCard(int cardId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the card belongs to the user
            var card = await _cardService.GetCardByIdAsync(cardId);
            if (card == null)
            {
                return NotFound("Card not found.");
            }
            var list = await _listService.GetListByIdAsync(card.ListId);
            if (list == null)
            {
                return NotFound("Card's list not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return NotFound("Card's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Card not found or not authorized.");
            }

            var comments = await _commentService.GetCommentsByCardIdAsync(cardId);

            var commentDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {
                var user = await _userService.GetUserByIdAsync(comment.UserId);
                commentDtos.Add(new CommentDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CardId = comment.CardId,
                    UserId = comment.UserId,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt,
                    User = user != null ? new UserDto { Id = user.Id, UserName = user.UserName, Email = user.Email } : null
                });
            }

            return Ok(commentDtos);
        }

        /// <summary>
        /// Gets a specific comment by ID for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>A CommentDto object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetComment(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            // Verify the comment's card, list, board, and workspace belong to the user
            var card = await _cardService.GetCardByIdAsync(comment.CardId);
            if (card == null)
            {
                return NotFound("Comment's card not found.");
            }
            var list = await _listService.GetListByIdAsync(card.ListId);
            if (list == null)
            {
                return NotFound("Comment's list not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return NotFound("Comment's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Comment not found or not authorized.");
            }

            var user = await _userService.GetUserByIdAsync(comment.UserId);

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CardId = comment.CardId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                User = user != null ? new UserDto { Id = user.Id, UserName = user.UserName, Email = user.Email } : null
            };

            return Ok(commentDto);
        }

        /// <summary>
        /// Creates a new comment within a specific card for the authenticated user.
        /// </summary>
        /// <param name="request">The request body for creating a comment.</param>
        /// <returns>The created CommentDto object.</returns>
        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the card belongs to the user
            var card = await _cardService.GetCardByIdAsync(request.CardId);
            if (card == null)
            {
                return BadRequest("Card not found.");
            }
            var list = await _listService.GetListByIdAsync(card.ListId);
            if (list == null)
            {
                return BadRequest("Card's list not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return BadRequest("Card's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return BadRequest("Card not found or not authorized.");
            }

            var comment = new Comment
            {
                Content = request.Content,
                CardId = request.CardId,
                UserId = userId, // Assign the current authenticated user as the comment creator
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _commentService.CreateCommentAsync(comment);

            // Retrieve the user to populate the UserDto
            var user = await _userService.GetUserByIdAsync(comment.UserId);

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CardId = comment.CardId,
                UserId = comment.UserId,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                User = user != null ? new UserDto { Id = user.Id, UserName = user.UserName, Email = user.Email } : null
            };

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
        }

        /// <summary>
        /// Updates an existing comment for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="request">The request body for updating a comment.</param>
        /// <returns>No content if successful, otherwise BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            // Verify the comment belongs to the user and its card, list, board, and workspace belong to the user
            if (comment.UserId != userId)
            {
                return Unauthorized("You are not authorized to update this comment.");
            }

            var card = await _cardService.GetCardByIdAsync(comment.CardId);
            if (card == null)
            {
                return NotFound("Comment's card not found.");
            }
            var list = await _listService.GetListByIdAsync(card.ListId);
            if (list == null)
            {
                return NotFound("Comment's list not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return NotFound("Comment's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Comment not found or not authorized.");
            }

            comment.Content = request.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentService.UpdateCommentAsync(comment);

            return NoContent();
        }

        /// <summary>
        /// Deletes a comment for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>No content if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            // Verify the comment belongs to the user and its card, list, board, and workspace belong to the user
            if (comment.UserId != userId)
            {
                return Unauthorized("You are not authorized to delete this comment.");
            }

            var card = await _cardService.GetCardByIdAsync(comment.CardId);
            if (card == null)
            {
                return NotFound("Comment's card not found.");
            }
            var list = await _listService.GetListByIdAsync(card.ListId);
            if (list == null)
            {
                return NotFound("Comment's list not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return NotFound("Comment's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Comment not found or not authorized.");
            }

            await _commentService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}