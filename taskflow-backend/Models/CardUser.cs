using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between a Card and a User (assignment).
    /// </summary>
    public class CardUser
    {
        /// <summary>
        /// Gets or sets the foreign key for the associated card.
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated card.
        /// </summary>
        public Card Card { get; set; }

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