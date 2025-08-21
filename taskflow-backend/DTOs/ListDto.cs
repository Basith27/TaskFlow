namespace TaskFlow.DTOs
{
    /// <summary>
    /// Placeholder DTO for a List.
    /// </summary>
    public class ListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BoardId { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp of the list.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}