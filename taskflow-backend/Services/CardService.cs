using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories; // Add this line
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TaskFlow.Services
{
    /// <summary>
    /// Service for managing Card entities.
    /// Encapsulates business logic related to cards.
    /// </summary>
    public class CardService
    {
        private readonly ICardRepository _cardRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardService"/> class.
        /// </summary>
        /// <param name="cardRepository">The card repository.</param>
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        /// <summary>
        /// Gets all cards for a specific list.
        /// </summary>
        /// <param name="listId">The ID of the list.</param>
        /// <returns>A list of cards.</returns>
        public async Task<IEnumerable<Card>> GetCardsByListIdAsync(int listId)
        {
            return await _cardRepository.GetCardsByListIdAsync(listId);
        }

        /// <summary>
        /// Gets a card by its ID.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <returns>The card if found, otherwise null.</returns>
        public async Task<Card> GetCardByIdAsync(int id)
        {
            return await _cardRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="card">The card to create.</param>
        /// <returns>The created card.</returns>
        public async Task<Card> CreateCardAsync(Card card)
        {
            await _cardRepository.AddAsync(card);
            return card;
        }

        /// <summary>
        /// Updates an existing card.
        /// </summary>
        /// <param name="card">The card to update.</param>
        public async Task UpdateCardAsync(Card card)
        {
            await _cardRepository.UpdateAsync(card);
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <param name="id">The ID of the card to delete.</param>
        public async Task DeleteCardAsync(int id)
        {
            await _cardRepository.DeleteAsync(id);
        }
    }
}