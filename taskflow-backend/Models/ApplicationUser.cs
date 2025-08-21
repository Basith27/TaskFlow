using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TaskFlow.Models
{
    /// <summary>
    /// Represents a user in the application, extending the default IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the collection of boards this user is a member of.
        /// </summary>
        public ICollection<BoardUser> BoardUsers { get; set; }

        /// <summary>
        /// Gets or sets the collection of cards this user is assigned to.
        /// </summary>
        public ICollection<CardUser> CardUsers { get; set; }

        /// <summary>
        /// Gets or sets the collection of comments made by this user.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the collection of workspaces owned by this user.
        /// </summary>
        public ICollection<Workspace> Workspaces { get; set; }
    }
}