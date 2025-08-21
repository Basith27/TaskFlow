using TaskFlow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Defines the interface for a user repository, extending the generic repository for ApplicationUser.
    /// </summary>
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        Task<ApplicationUser> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

        /// <summary>
        /// Updates a user's profile.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The IdentityResult of the update operation.</returns>
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>The IdentityResult of the delete operation.</returns>
        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}