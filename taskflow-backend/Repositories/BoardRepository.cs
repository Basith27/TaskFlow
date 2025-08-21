using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the board repository.
    /// </summary>
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public BoardRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all boards associated with a specific workspace.
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace.</param>
        /// <returns>A collection of boards.</returns>
        public async Task<IEnumerable<Board>> GetBoardsByWorkspaceIdAsync(int workspaceId)
        {
            return await _dbSet.Where(b => b.WorkspaceId == workspaceId).ToListAsync();
        }
    }
}