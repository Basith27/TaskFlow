using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to update an existing card.
    /// </summary>
    public class UpdateCardRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the card to update.
        /// </summary>
        [Required]
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
        /// Gets or sets the due date for the card.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}