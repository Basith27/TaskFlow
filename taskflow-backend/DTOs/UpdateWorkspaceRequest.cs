using System.ComponentModel.DataAnnotations;

namespace taskflow_backend.DTOs
{
    public class UpdateWorkspaceRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}