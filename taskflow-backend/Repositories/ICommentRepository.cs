using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a comment repository, extending the generic repository.
    /// </summary>
    public interface ICommentRepository : IRepository<Comment>
    {
        /// <summary>
        /// Gets all comments associated with a specific card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <returns>A collection of comments.</returns>
        Task<IEnumerable<Comment>> GetCommentsByCardIdAsync(int cardId);
    }
}