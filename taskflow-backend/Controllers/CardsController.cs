using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskFlow.Data;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services; // Add this for CardService, ListService, BoardService, WorkspaceService

namespace TaskFlow.Controllers
{
    /// <summary>
    /// API controller for managing cards within lists.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly CardService _cardService;
        private readonly ListService _listService; // Needed to verify list ownership
        private readonly BoardService _boardService; // Needed to verify board ownership
        private readonly WorkspaceService _workspaceService; // Needed to verify workspace ownership

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsController"/> class.
        /// </summary>
        /// <param name="cardService">The card service for business logic.</param>
        /// <param name="listService">The list service for verifying list ownership.</param>
        /// <param name="boardService">The board service for verifying board ownership.</param>
        /// <param name="workspaceService">The workspace service for verifying workspace ownership.</param>
        public CardsController(CardService cardService, ListService listService, BoardService boardService, WorkspaceService workspaceService)
        {
            _cardService = cardService;
            _listService = listService;
            _boardService = boardService;
            _workspaceService = workspaceService;
        }

        /// <summary>
        /// Gets all cards for a specific list for the authenticated user.
        /// </summary>
        /// <param name="listId">The ID of the list.</param>
        /// <returns>A list of CardDto objects.</returns>
        [HttpGet("byList/{listId}")]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCardsByList(int listId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the list belongs to the user
            var list = await _listService.GetListByIdAsync(listId);
            if (list == null)
            {
                return NotFound("List not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return NotFound("List's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("List not found or not authorized.");
            }

            var cards = await _cardService.GetCardsByListIdAsync(listId);

            var cardDtos = cards.Select(c => new CardDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ListId = c.ListId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                DueDate = c.DueDate,
                Comments = c.Comments.Select(comment => new CommentDto { Id = comment.Id, Content = comment.Content, CardId = comment.CardId, UserId = comment.UserId, CreatedAt = comment.CreatedAt }).ToList(), // Include all CommentDto properties
                AssignedUsers = c.CardUsers.Select(cu => new UserDto { Id = cu.User.Id, UserName = cu.User.UserName, Email = cu.User.Email }).ToList() // Include all UserDto properties
            }).ToList();

            return Ok(cardDtos);
        }

        /// <summary>
        /// Gets a specific card by ID for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <returns>A CardDto object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDto>> GetCard(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            // Verify the card's list, board, and workspace belong to the user
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

            var cardDto = new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                ListId = card.ListId,
                CreatedAt = card.CreatedAt,
                UpdatedAt = card.UpdatedAt,
                DueDate = card.DueDate,
                Comments = card.Comments.Select(comment => new CommentDto { Id = comment.Id, Content = comment.Content, CardId = comment.CardId, UserId = comment.UserId, CreatedAt = comment.CreatedAt }).ToList(),
                AssignedUsers = card.CardUsers.Select(cu => new UserDto { Id = cu.User.Id, UserName = cu.User.UserName, Email = cu.User.Email }).ToList()
            };

            return Ok(cardDto);
        }

        /// <summary>
        /// Creates a new card within a specific list for the authenticated user.
        /// </summary>
        /// <param name="request">The request body for creating a card.</param>
        /// <returns>The created CardDto object.</returns>
        [HttpPost]
        public async Task<ActionResult<CardDto>> CreateCard(CreateCardRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the list belongs to the user
            var list = await _listService.GetListByIdAsync(request.ListId);
            if (list == null)
            {
                return BadRequest("List not found.");
            }
            var board = await _boardService.GetBoardByIdAsync(list.BoardId);
            if (board == null)
            {
                return BadRequest("List's board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return BadRequest("List not found or not authorized.");
            }

            var card = new Card
            {
                Title = request.Title,
                Description = request.Description,
                ListId = request.ListId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DueDate = request.DueDate
            };

            await _cardService.CreateCardAsync(card);

            var cardDto = new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                ListId = card.ListId,
                CreatedAt = card.CreatedAt,
                UpdatedAt = card.UpdatedAt,
                DueDate = card.DueDate,
                Comments = new List<CommentDto>(),
                AssignedUsers = new List<UserDto>()
            };

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, cardDto);
        }

        /// <summary>
        /// Updates an existing card for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the card to update.</param>
        /// <param name="request">The request body for updating a card.</param>
        /// <returns>No content if successful, otherwise BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, UpdateCardRequest request)
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

            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            // Verify the card's list, board, and workspace belong to the user
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

            card.Title = request.Title;
            card.Description = request.Description;
            card.DueDate = request.DueDate;
            card.UpdatedAt = DateTime.UtcNow;

            await _cardService.UpdateCardAsync(card);

            return NoContent();
        }

        /// <summary>
        /// Deletes a card for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the card to delete.</param>
        /// <returns>No content if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            // Verify the card's list, board, and workspace belong to the user
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

            await _cardService.DeleteCardAsync(id);

            return NoContent();
        }
    }
}