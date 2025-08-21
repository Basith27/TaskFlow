using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a workspace repository, extending the generic repository.
    /// </summary>
    public interface IWorkspaceRepository : IRepository<Workspace>
    {
        /// <summary>
        /// Gets all workspaces associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of workspaces.</returns>
        Task<IEnumerable<Workspace>> GetWorkspacesByUserIdAsync(string userId);
    }
}