using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to update an existing board.
    /// </summary>
    public class UpdateBoardRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the board to update.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}