using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the workspace repository.
    /// </summary>
    public class WorkspaceRepository : Repository<Workspace>, IWorkspaceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public WorkspaceRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all workspaces associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of workspaces.</returns>
        public async Task<IEnumerable<Workspace>> GetWorkspacesByUserIdAsync(string userId)
        {
            return await _dbSet.Where(w => w.UserId == userId).ToListAsync();
        }
    }
}