using System;
using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a board.
    /// </summary>
    public class BoardDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the board.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated workspace.
        /// </summary>
        public int WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the board was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the board was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the collection of lists within this board.
        /// </summary>
        public ICollection<ListDto> Lists { get; set; }
    }
}