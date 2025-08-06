using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a project board within a workspace.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Gets or sets the unique identifier for the board.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated workspace.
        /// </summary>
        public int WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated workspace.
        /// </summary>
        public Workspace Workspace { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the board was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the collection of lists within this board.
        /// </summary>
        public ICollection<List> Lists { get; set; }

        /// <summary>
        /// Gets or sets the collection of users who are members of this board.
        /// </summary>
        public ICollection<BoardUser> BoardUsers { get; set; }
    }
}