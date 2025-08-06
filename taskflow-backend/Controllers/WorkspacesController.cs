using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskflow_backend.DTOs;
using TaskFlow.Models;
using TaskFlow.Data;
using Microsoft.EntityFrameworkCore;

namespace taskflow_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkspacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkspacesController(ApplicationDbContext context)
        {
            _context = context;
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

            var workspaces = await _context.Workspaces
                .Where(w => w.UserId == userId)
                .Select(w => new WorkspaceDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                })
                .ToListAsync();

            return Ok(workspaces);
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

            var workspace = await _context.Workspaces
                .Where(w => w.Id == id && w.UserId == userId)
                .Select(w => new WorkspaceDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (workspace == null)
            {
                return NotFound();
            }

            return Ok(workspace);
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

            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync();

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

            var workspace = await _context.Workspaces
                .Where(w => w.Id == id && w.UserId == userId)
                .FirstOrDefaultAsync();

            if (workspace == null)
            {
                return NotFound();
            }

            workspace.Name = request.Name;
            workspace.UpdatedAt = DateTime.UtcNow;

            _context.Entry(workspace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Workspaces.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            var workspace = await _context.Workspaces
                .Where(w => w.Id == id && w.UserId == userId)
                .FirstOrDefaultAsync();

            if (workspace == null)
            {
                return NotFound();
            }

            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}