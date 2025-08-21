using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for creating a new workspace.
    /// </summary>
    public class CreateWorkspaceRequest
    {
        /// <summary>
        /// Gets or sets the name of the workspace.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}