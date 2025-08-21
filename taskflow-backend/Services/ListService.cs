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
    /// Service for managing List entities.
    /// Encapsulates business logic related to lists.
    /// </summary>
    public class ListService
    {
        private readonly IListRepository _listRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListService"/> class.
        /// </summary>
        /// <param name="listRepository">The list repository.</param>
        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        /// <summary>
        /// Gets all lists for a specific board.
        /// </summary>
        /// <param name="boardId">The ID of the board.</param>
        /// <returns>A list of lists.</returns>
        public async Task<IEnumerable<List>> GetListsByBoardIdAsync(int boardId)
        {
            return await _listRepository.GetListsByBoardIdAsync(boardId);
        }

        /// <summary>
        /// Gets a list by its ID.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The list if found, otherwise null.</returns>
        public async Task<List> GetListByIdAsync(int id)
        {
            return await _listRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="list">The list to create.</param>
        /// <returns>The created list.</returns>
        public async Task<List> CreateListAsync(List list)
        {
            await _listRepository.AddAsync(list);
            return list;
        }

        /// <summary>
        /// Updates an existing list.
        /// </summary>
        /// <param name="list">The list to update.</param>
        public async Task UpdateListAsync(List list)
        {
            await _listRepository.UpdateAsync(list);
        }

        /// <summary>
        /// Deletes a list.
        /// </summary>
        /// <param name="id">The ID of the list to delete.</param>
        public async Task DeleteListAsync(int id)
        {
            await _listRepository.DeleteAsync(id);
        }
    }
}