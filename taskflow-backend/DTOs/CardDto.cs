using System;
using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a card.
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the card.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the card.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the card.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated list.
        /// </summary>
        public int ListId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the card was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp of the card.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the due date for the card.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of comments on this card.
        /// </summary>
        public ICollection<CommentDto> Comments { get; set; }

        /// <summary>
        /// Gets or sets the collection of users assigned to this card.
        /// </summary>
        public ICollection<UserDto> AssignedUsers { get; set; }
    }
}