using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a workspace, the highest-level container for multiple project boards.
    /// </summary>
    public class Workspace
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workspace.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the workspace.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the workspace was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the workspace was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the ID of the user who owns this workspace.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationUser associated with this workspace.
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the collection of boards within this workspace.
        /// </summary>
        public ICollection<Board> Boards { get; set; }
    }
}