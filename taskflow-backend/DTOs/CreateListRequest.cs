using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to create a new list.
    /// </summary>
    public class CreateListRequest
    {
        /// <summary>
        /// Gets or sets the title of the list.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ID of the board to which the list belongs.
        /// </summary>
        [Required]
        public int BoardId { get; set; }
    }
}