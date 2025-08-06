using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between a Board and a User.
    /// </summary>
    public class BoardUser
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated board.
        /// </summary>
        public int BoardId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated board.
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated user.
        /// </summary>
        public string UserId { get; set; } // Assuming UserId is string for IdentityUser

        /// <summary>
        /// Gets or sets the navigation property to the associated user.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}