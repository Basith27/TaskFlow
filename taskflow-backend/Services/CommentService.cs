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
    /// Service for managing Comment entities.
    /// Encapsulates business logic related to comments.
    /// </summary>
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="commentRepository">The comment repository.</param>
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Gets all comments for a specific card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <returns>A list of comments.</returns>
        public async Task<IEnumerable<Comment>> GetCommentsByCardIdAsync(int cardId)
        {
            return await _commentRepository.GetCommentsByCardIdAsync(cardId);
        }

        /// <summary>
        /// Gets a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>The comment if found, otherwise null.</returns>
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="comment">The comment to create.</param>
        /// <returns>The created comment.</returns>
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
            return comment;
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="comment">The comment to update.</param>
        public async Task UpdateCommentAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}