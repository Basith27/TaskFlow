using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories; // Ensure this is at the top
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TaskFlow.Services
{
    /// <summary>
    /// Service for managing Board entities.
    /// Encapsulates business logic related to boards.
    /// </summary>
    public class BoardService
    {
        private readonly IBoardRepository _boardRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardService"/> class.
        /// </summary>
        /// <param name="boardRepository">The board repository.</param>
        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        /// <summary>
        /// Gets all boards for a specific workspace.
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace.</param>
        /// <returns>A list of boards.</returns>
        public async Task<IEnumerable<Board>> GetBoardsByWorkspaceIdAsync(int workspaceId)
        {
            return await _boardRepository.GetBoardsByWorkspaceIdAsync(workspaceId);
        }

        /// <summary>
        /// Gets a board by its ID.
        /// </summary>
        /// <param name="id">The ID of the board.</param>
        /// <returns>The board if found, otherwise null.</returns>
        public async Task<Board> GetBoardByIdAsync(int id)
        {
            return await _boardRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new board.
        /// </summary>
        /// <param name="board">The board to create.</param>
        /// <returns>The created board.</returns>
        public async Task<Board> CreateBoardAsync(Board board)
        {
            await _boardRepository.AddAsync(board);
            return board;
        }

        /// <summary>
        /// Updates an existing board.
        /// </summary>
        /// <param name="board">The board to update.</param>
        public async Task UpdateBoardAsync(Board board)
        {
            await _boardRepository.UpdateAsync(board);
        }

        /// <summary>
        /// Deletes a board.
        /// </summary>
        /// <param name="id">The ID of the board to delete.</param>
        public async Task DeleteBoardAsync(int id)
        {
            await _boardRepository.DeleteAsync(id);
        }
    }
}