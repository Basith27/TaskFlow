using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TaskFlow.Repositories
{
    /// <summary>
    /// Concrete implementation of the user repository, specifically for ApplicationUser.
    /// Leverages UserManager for Identity-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userManager">The user manager for Identity operations.</param>
        /// <param name="context">The application database context.</param>
        public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            // IdentityUser.Id is string, so this method needs adjustment or a different approach
            // For now, we'll assume 'id' can be converted to string for FindByIdAsync
            return await _userManager.FindByIdAsync(id.ToString());
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        /// <summary>
        /// Adds a new user. This operation is typically handled by Identity's registration process.
        /// </summary>
        /// <param name="entity">The user to add.</param>
        public async Task AddAsync(ApplicationUser entity)
        {
            // This method is typically handled by UserManager.CreateAsync in authentication service
            // For repository pattern, we might just save changes if the entity is already tracked
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="entity">The user to update.</param>
        public async Task UpdateAsync(ApplicationUser entity)
        {
            await _userManager.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        /// <summary>
        /// Updates a user's profile.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The IdentityResult of the update operation.</returns>
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>The IdentityResult of the delete operation.</returns>
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
    }
}