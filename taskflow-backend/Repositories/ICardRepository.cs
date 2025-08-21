using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a card repository, extending the generic repository.
    /// </summary>
    public interface ICardRepository : IRepository<Card>
    {
        /// <summary>
        /// Gets all cards associated with a specific list.
        /// </summary>
        /// <param name="listId">The ID of the list.</param>
        /// <returns>A collection of cards.</returns>
        Task<IEnumerable<Card>> GetCardsByListIdAsync(int listId);
    }
}