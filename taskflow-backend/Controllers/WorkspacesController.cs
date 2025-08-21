using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.DTOs;
using TaskFlow.Models;
using TaskFlow.Services; // Add this for WorkspaceService
using Microsoft.Extensions.Logging;

namespace TaskFlow.Controllers
{
    /// <summary>
    /// API controller for managing workspaces.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkspacesController : ControllerBase
    {
        private readonly WorkspaceService _workspaceService;
        private readonly ILogger<WorkspacesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspacesController"/> class.
        /// </summary>
        /// <param name="workspaceService">The workspace service for business logic.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public WorkspacesController(WorkspaceService workspaceService, ILogger<WorkspacesController> logger)
        {
            _workspaceService = workspaceService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all workspaces for the authenticated user.
        /// </summary>
        /// <returns>A list of WorkspaceDto objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkspaceDto>>> GetWorkspaces()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var workspaces = await _workspaceService.GetWorkspacesByUserIdAsync(userId);

            var workspaceDtos = workspaces.Select(w => new WorkspaceDto
            {
                Id = w.Id,
                Name = w.Name,
                CreatedAt = w.CreatedAt,
                UpdatedAt = w.UpdatedAt
            }).ToList();

            return Ok(workspaceDtos);
        }

        /// <summary>
        /// Gets a specific workspace by ID for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the workspace.</param>
        /// <returns>A WorkspaceDto object if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkspaceDto>> GetWorkspace(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var workspace = await _workspaceService.GetWorkspaceByIdAsync(id);

            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound();
            }

            var workspaceDto = new WorkspaceDto
            {
                Id = workspace.Id,
                Name = workspace.Name,
                CreatedAt = workspace.CreatedAt,
                UpdatedAt = workspace.UpdatedAt
            };

            return Ok(workspaceDto);
        }

        /// <summary>
        /// Creates a new workspace for the authenticated user.
        /// </summary>
        /// <param name="request">The request body for creating a workspace.</param>
        /// <returns>The created WorkspaceDto object.</returns>
        [HttpPost]
        public async Task<ActionResult<WorkspaceDto>> CreateWorkspace(CreateWorkspaceRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var workspace = new Workspace
            {
                Name = request.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _workspaceService.CreateWorkspaceAsync(workspace);

            var workspaceDto = new WorkspaceDto
            {
                Id = workspace.Id,
                Name = workspace.Name,
                CreatedAt = workspace.CreatedAt,
                UpdatedAt = workspace.UpdatedAt
            };

            return CreatedAtAction(nameof(GetWorkspace), new { id = workspace.Id }, workspaceDto);
        }

        /// <summary>
        /// Updates an existing workspace for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the workspace to update.</param>
        /// <param name="request">The request body for updating a workspace.</param>
        /// <returns>No content if successful, otherwise BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkspace(int id, UpdateWorkspaceRequest request)
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

            var workspace = await _workspaceService.GetWorkspaceByIdAsync(id);

            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound();
            }

            workspace.Name = request.Name;
            workspace.UpdatedAt = DateTime.UtcNow;

            await _workspaceService.UpdateWorkspaceAsync(workspace);

            return NoContent();
        }

        /// <summary>
        /// Deletes a workspace for the authenticated user.
        /// </summary>
        /// <param name="id">The ID of the workspace to delete.</param>
        /// <returns>No content if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkspace(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var workspace = await _workspaceService.GetWorkspaceByIdAsync(id);

            if (workspace == null || workspace.UserId != userId)
            {
                return NotFound();
            }

            await _workspaceService.DeleteWorkspaceAsync(id);

            return NoContent();
        }
    }
}