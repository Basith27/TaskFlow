using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories; // Add this line
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TaskFlow.Services
{
    /// <summary>
    /// Service for managing Workspace entities.
    /// Encapsulates business logic related to workspaces.
    /// </summary>
    public class WorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceService"/> class.
        /// </summary>
        /// <param name="workspaceRepository">The workspace repository.</param>
        public WorkspaceService(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        /// <summary>
        /// Gets all workspaces for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of workspaces.</returns>
        public async Task<IEnumerable<Workspace>> GetWorkspacesByUserIdAsync(string userId)
        {
            return await _workspaceRepository.GetWorkspacesByUserIdAsync(userId);
        }

        /// <summary>
        /// Gets a workspace by its ID.
        /// </summary>
        /// <param name="id">The ID of the workspace.</param>
        /// <returns>The workspace if found, otherwise null.</returns>
        public async Task<Workspace> GetWorkspaceByIdAsync(int id)
        {
            return await _workspaceRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        /// <param name="workspace">The workspace to create.</param>
        /// <returns>The created workspace.</returns>
        public async Task<Workspace> CreateWorkspaceAsync(Workspace workspace)
        {
            await _workspaceRepository.AddAsync(workspace);
            return workspace;
        }

        /// <summary>
        /// Updates an existing workspace.
        /// </summary>
        /// <param name="workspace">The workspace to update.</param>
        public async Task UpdateWorkspaceAsync(Workspace workspace)
        {
            await _workspaceRepository.UpdateAsync(workspace);
        }

        /// <summary>
        /// Deletes a workspace.
        /// </summary>
        /// <param name="id">The ID of the workspace to delete.</param>
        public async Task DeleteWorkspaceAsync(int id)
        {
            await _workspaceRepository.DeleteAsync(id);
        }
    }
}