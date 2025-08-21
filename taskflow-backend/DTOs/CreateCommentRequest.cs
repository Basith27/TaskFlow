using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to create a new comment.
    /// </summary>
    public class CreateCommentRequest
    {
        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the ID of the card to which the comment belongs.
        /// </summary>
        [Required]
        public int CardId { get; set; }
    }
}