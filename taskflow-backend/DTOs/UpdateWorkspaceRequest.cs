using System.ComponentModel.DataAnnotations;

namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for updating an existing workspace.
    /// </summary>
    public class UpdateWorkspaceRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the workspace to update.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new name of the workspace.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}