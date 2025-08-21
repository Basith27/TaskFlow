using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services; // Add this for ListService and BoardService

namespace TaskFlow.Controllers
{
    /// <summary>
    /// API controller for managing lists within boards.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ListsController : ControllerBase
    {
        private readonly ListService _listService;
        private readonly BoardService _boardService; // Needed to verify board ownership
        private readonly WorkspaceService _workspaceService; // Needed to verify workspace ownership

        /// <summary>
        /// Initializes a new instance of the <see cref="ListsController"/> class.
        /// </summary>
        /// <param name="listService">The list service for business logic.</param>
        /// <param name="boardService">The board service for verifying board ownership.</param>
        /// <param name="workspaceService">The workspace service for verifying workspace ownership.</param>
        public ListsController(ListService listService, BoardService boardService, WorkspaceService workspaceService)
        {
            _listService = listService;
            _boardService = boardService;
            _workspaceService = workspaceService;
        }

        /// <summary>
        /// Gets all lists for a specific board for the authenticated user.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <returns>A list of ListDto objects.</returns>
        [HttpGet("byBoard/{boardId}")]
        public async Task<ActionResult<IEnumerable<ListDto>>> GetListsByBoard(int boardId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the board belongs to the user
            var board = await _boardService.GetBoardByIdAsync(boardId);
            if (board == null)
            {
                return NotFound("Board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound("Board not found or not authorized.");
            }

            var lists = await _listService.GetListsByBoardIdAsync(boardId);

            var listDtos = lists.Select(l => new ListDto
            {
                Id = l.Id,
                Title = l.Title,
                BoardId = l.BoardId,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList();

            return Ok(listDtos);
        }

        /// <summary>
        /// Gets a specific list by ID for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>A ListDto object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ListDto>> GetList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var list = await _listService.GetListByIdAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            // Verify the list's board belongs to the user
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

            var listDto = new ListDto
            {
                Id = list.Id,
                Title = list.Title,
                BoardId = list.BoardId,
                CreatedAt = list.CreatedAt,
                UpdatedAt = list.UpdatedAt
            };

            return Ok(listDto);
        }

        /// <summary>
        /// Creates a new list within a specific board for the authenticated user.
        /// </summary>
        /// <param name="request">The request body for creating a list.</param>
        /// <returns>The created ListDto object.</returns>
        [HttpPost]
        public async Task<ActionResult<ListDto>> CreateList(CreateListRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Verify the board belongs to the user
            var board = await _boardService.GetBoardByIdAsync(request.BoardId);
            if (board == null)
            {
                return BadRequest("Board not found.");
            }
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(board.WorkspaceId);
            if (workspace == null || workspace.UserId != userId)
            {
                return BadRequest("Board not found or not authorized.");
            }

            var list = new List
            {
                Title = request.Title,
                BoardId = request.BoardId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _listService.CreateListAsync(list);

            var listDto = new ListDto
            {
                Id = list.Id,
                Title = list.Title,
                BoardId = list.BoardId,
                CreatedAt = list.CreatedAt,
                UpdatedAt = list.UpdatedAt
            };

            return CreatedAtAction(nameof(GetList), new { id = list.Id }, listDto);
        }

        /// <summary>
        /// Updates an existing list for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the list to update.</param>
        /// <param name="request">The request body for updating a list.</param>
        /// <returns>No content if successful, otherwise BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, UpdateListRequest request)
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

            var list = await _listService.GetListByIdAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            // Verify the list's board belongs to the user
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

            list.Title = request.Title;
            list.UpdatedAt = DateTime.UtcNow;

            await _listService.UpdateListAsync(list);

            return NoContent();
        }

        /// <summary>
        /// Deletes a list for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the list to delete.</param>
        /// <returns>No content if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var list = await _listService.GetListByIdAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            // Verify the list's board belongs to the user
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

            await _listService.DeleteListAsync(id);

            return NoContent();
        }
    }
}