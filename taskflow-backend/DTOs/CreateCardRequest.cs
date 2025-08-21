using System;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to create a new card.
    /// </summary>
    public class CreateCardRequest
    {
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
        [Required]
        public int ListId { get; set; }

        /// <summary>
        /// Gets or sets the due date for the card.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}