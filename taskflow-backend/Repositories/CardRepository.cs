using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the card repository.
    /// </summary>
    public class CardRepository : Repository<Card>, ICardRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public CardRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all cards associated with a specific list.
        /// </summary>
        /// <param name="listId">The ID of the list.</param>
        /// <returns>A collection of cards.</returns>
        public async Task<IEnumerable<Card>> GetCardsByListIdAsync(int listId)
        {
            return await _dbSet.Where(c => c.ListId == listId).ToListAsync();
        }
    }
}