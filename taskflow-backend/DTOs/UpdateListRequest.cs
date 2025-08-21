using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a request to update an existing list.
    /// </summary>
    public class UpdateListRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the list to update.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the list.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}