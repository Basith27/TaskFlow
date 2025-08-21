using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the comment repository.
    /// </summary>
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all comments associated with a specific card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <returns>A collection of comments.</returns>
        public async Task<IEnumerable<Comment>> GetCommentsByCardIdAsync(int cardId)
        {
            return await _dbSet.Where(c => c.CardId == cardId).ToListAsync();
        }
    }
}