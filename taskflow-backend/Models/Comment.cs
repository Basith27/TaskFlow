using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a comment on a card.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the comment.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated card.
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated card.
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the user who made the comment.
        /// </summary>
        public string UserId { get; set; } // Assuming UserId is string for IdentityUser

        /// <summary>
        /// Gets or sets the navigation property to the user who made the comment.
        /// </summary>
        public ApplicationUser User { get; set; } // Will be ApplicationUser from Identity

        /// <summary>
        /// Gets or sets the date and time when the comment was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}