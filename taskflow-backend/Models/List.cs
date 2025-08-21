using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a column on a board that represents a stage in the workflow.
    /// </summary>
    public class List
    {
        /// <summary>
        /// Gets or sets the unique identifier for the list.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the list.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated board.
        /// </summary>
        public int BoardId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated board.
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the list was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the list was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the collection of cards within this list.
        /// </summary>
        public ICollection<Card> Cards { get; set; }
    }
}