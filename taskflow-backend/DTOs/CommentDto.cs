namespace TaskFlow.DTOs
{
    /// <summary>
    /// Placeholder DTO for a Comment.
    /// </summary>
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CardId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp of the comment.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        public UserDto User { get; set; }
    }
}