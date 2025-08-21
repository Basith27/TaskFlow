namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a workspace.
    /// </summary>
    public class WorkspaceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workspace.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the workspace.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp of the workspace.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last update timestamp of the workspace.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}