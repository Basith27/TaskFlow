using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DTOs;
using TaskFlow.Models;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Controllers
{
    /// <summary>
    /// Controller for user authentication, including registration and login.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager for handling user-related operations.</param>
        /// <param name="signInManager">The sign-in manager for handling user sign-in operations.</param>
        /// <param name="configuration">The application configuration for JWT settings.</param>
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing email and password.</param>
        /// <returns>An authentication response indicating success or failure.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse { IsSuccess = false, Errors = GetErrorsFromModelState() });
            }

            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Optionally add user to a role here, e.g., await _userManager.AddToRoleAsync(user, "User");
                return Ok(new AuthResponse { IsSuccess = true, Token = GenerateJwtToken(user) });
            }

            return BadRequest(new AuthResponse { IsSuccess = false, Errors = GetErrorsFromResult(result) });
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>An authentication response indicating success or failure.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse { IsSuccess = false, Errors = GetErrorsFromModelState() });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized(new AuthResponse { IsSuccess = false, Errors = new[] { "Invalid credentials." } });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                return Ok(new AuthResponse { IsSuccess = true, Token = GenerateJwtToken(user) });
            }

            return Unauthorized(new AuthResponse { IsSuccess = false, Errors = new[] { "Invalid credentials." } });
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the authenticated user.
        /// </summary>
        /// <param name="user">The application user for whom to generate the token.</param>
        /// <returns>A JWT string.</returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                // Changed: Removed Sub claim and added proper claims
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpirationDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Extracts error messages from ModelState.
        /// </summary>
        /// <returns>An array of error messages.</returns>
        private string[] GetErrorsFromModelState()
        {
            return ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
        }

        /// <summary>
        /// Extracts error messages from an IdentityResult.
        /// </summary>
        /// <param name="result">The IdentityResult object.</param>
        /// <returns>An array of error messages.</returns>
        private string[] GetErrorsFromResult(IdentityResult result)
        {
            return result.Errors.Select(e => e.Description).ToArray();
        }
    }
}