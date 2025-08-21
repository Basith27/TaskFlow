using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Repositories; // Add this line
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Services
{
    /// <summary>
    /// Service for managing User entities and related operations.
    /// Encapsulates business logic related to users.
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(int.Parse(userId)); // Assuming GetByIdAsync takes int
        }

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetUserByUserNameAsync(userName);
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        /// <summary>
        /// Updates a user's profile.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The IdentityResult of the update operation.</returns>
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>The IdentityResult of the delete operation.</returns>
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}