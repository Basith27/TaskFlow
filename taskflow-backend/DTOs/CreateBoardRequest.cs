using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to create a new board.
    /// </summary>
    public class CreateBoardRequest
    {
        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workspace to which the board belongs.
        /// </summary>
        [Required]
        public int WorkspaceId { get; set; }
    }
}