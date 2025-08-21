using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the list repository.
    /// </summary>
    public class ListRepository : Repository<List>, IListRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ListRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all lists associated with a specific board.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <returns>A collection of lists.</returns>
        public async Task<IEnumerable<List>> GetListsByBoardIdAsync(int boardId)
        {
            return await _dbSet.Where(l => l.BoardId == boardId).ToListAsync();
        }
    }
}