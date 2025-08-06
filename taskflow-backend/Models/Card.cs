using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a single task that can be moved between lists.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets the unique identifier for the card.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the card.
        /// </summary>
        [Required]
        [MaxLength(200)]
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
        /// Gets or sets the navigation property to the associated list.
        /// </summary>
        public List List { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the card was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the due date for the card.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of comments on this card.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the collection of users assigned to this card.
        /// </summary>
        public ICollection<CardUser> CardUsers { get; set; }
    }
}