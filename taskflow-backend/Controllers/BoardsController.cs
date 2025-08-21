using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services; // Add this for BoardService and WorkspaceService
using Microsoft.Extensions.Logging;

namespace TaskFlow.Controllers
{
    /// <summary>
    /// API controller for managing boards within workspaces.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private readonly BoardService _boardService;
        private readonly WorkspaceService _workspaceService; // Needed to verify workspace ownership
        private readonly ILogger<BoardsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardsController"/> class.
        /// </summary>
        /// <param name="boardService">The board service for business logic.</param>
        /// <param name="workspaceService">The workspace service for verifying workspace ownership.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public BoardsController(BoardService boardService, WorkspaceService workspaceService, ILogger<BoardsController> logger)
        {
            _boardService = boardService;
            _workspaceService = workspaceService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all boards for a specific workspace for the authenticated user.
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace.</param>
        /// <returns>A list of BoardDto objects.</returns>
        [HttpGet("byWorkspace/{workspaceId}")]
        public async Task<ActionResult<IEnumerable<BoardDto>>> GetBoardsByWorkspace(int workspaceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the workspace belongs to the user
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(workspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Workspace not found or not authorized.");
            }

            var boards = await _boardService.GetBoardsByWorkspaceIdAsync(workspaceId);

            var boardDtos = boards.Select(b => new BoardDto
            {
                Id = b.Id,
                Name = b.Name,
                WorkspaceId = b.WorkspaceId,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToList();

            return Ok(boardDtos);
        }

        /// <summary>
        /// Gets a specific board by ID for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <returns>A BoardDto object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDto>> GetBoard(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var board = await _boardService.GetBoardByIdAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            // Verify the board's workspace belongs to the user
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Board not found or not authorized.");
            }

            var boardDto = new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                WorkspaceId = board.WorkspaceId,
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt
            };

            return Ok(boardDto);
        }

        /// <summary>
        /// Creates a new board within a specific workspace for the authenticated user.
        /// </summary>
        /// <param name="request">The request body for creating a board.</param>
        /// <returns>The created BoardDto object.</returns>
        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the workspace belongs to the user
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(request.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Workspace not found or not authorized.");
            }

            var board = new Board
            {
                Name = request.Name,
                WorkspaceId = request.WorkspaceId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _boardService.CreateBoardAsync(board);

            var boardDto = new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                WorkspaceId = board.WorkspaceId,
                CreatedAt = board.CreatedAt,
                UpdatedAt = board.UpdatedAt
            };

            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, boardDto);
        }

        /// <summary>
        /// Updates an existing board for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the board to update.</param>
        /// <param name="request">The request body for updating a board.</param>
        /// <returns>No content if successful, otherwise BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, UpdateBoardRequest request)
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

            var board = await _boardService.GetBoardByIdAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            // Verify the board's workspace belongs to the user
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Board not found or not authorized.");
            }

            board.Name = request.Name;
            board.UpdatedAt = DateTime.UtcNow;

            await _boardService.UpdateBoardAsync(board);

            return NoContent();
        }

        /// <summary>
        /// Deletes a board for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        /// <returns>No content if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var board = await _boardService.GetBoardByIdAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            // Verify the board's workspace belongs to the user
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Board not found or not authorized.");
            }

            await _boardService.DeleteBoardAsync(id);

            return NoContent();
        }
    }
}