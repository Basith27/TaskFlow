namespace TaskFlow.DTOs
{
    /// <summary>
    /// Represents a data transfer object for authentication responses.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Gets or sets a boolean indicating if the authentication was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets an array of error messages, if any.
        /// </summary>
        public string[] Errors { get; set; }
    }
}