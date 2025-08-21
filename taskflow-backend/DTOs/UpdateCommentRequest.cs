using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to update an existing comment.
    /// </summary>
    public class UpdateCommentRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the comment to update.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}