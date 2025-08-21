using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a list repository, extending the generic repository.
    /// </summary>
    public interface IListRepository : IRepository<List>
    {
        /// <summary>
        /// Gets all lists associated with a specific board.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <returns>A collection of lists.</returns>
        Task<IEnumerable<List>> GetListsByBoardIdAsync(int boardId);
    }
}