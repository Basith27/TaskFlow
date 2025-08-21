using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a board repository, extending the generic repository.
    /// </summary>
    public interface IBoardRepository : IRepository<Board>
    {
        /// <summary>
        /// Gets all boards associated with a specific workspace.
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace.</param>
        /// <returns>A collection of boards.</returns>
        Task<IEnumerable<Board>> GetBoardsByWorkspaceIdAsync(int workspaceId);
    }
}