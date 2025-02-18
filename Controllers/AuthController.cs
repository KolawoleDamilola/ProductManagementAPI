using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey = "your-very-secure-secret-key-16bytes";  // Use a more secure method in production
        private readonly string _issuer = "your-issuer";
        private readonly string _audience = "your-audience";

        // Simulate user validation (this can be replaced by database validation)
        private bool ValidateUser(string username, string password)
        {
            // For example, let's assume the user credentials are 'user' and 'password123'
            return username == "user" && password == "password123";
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Ensure loginModel is valid and contains necessary values
            if (loginModel == null || string.IsNullOrWhiteSpace(loginModel.Username) || string.IsNullOrWhiteSpace(loginModel.Password) || !ValidateUser(loginModel.Username, loginModel.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            // Create claims
            var claims = new[] 
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(ClaimTypes.Role, "User"), // Optional: You can add roles or other claims as needed
            };

            // Create signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: credentials
            );

            // Return the token
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public class LoginModel
    {
        // Ensure these properties are non-nullable
        public string? Username { get; set; }  // Non-nullable
        public string? Password { get; set; }  // Non-nullable
    }
}
