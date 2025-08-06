using System.ComponentModel.DataAnnotations;

namespace taskflow_backend.DTOs
{
    public class CreateWorkspaceRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}