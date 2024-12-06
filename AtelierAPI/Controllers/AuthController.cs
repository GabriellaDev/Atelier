using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AtelierShared.Models;
using AtelierAPI.Data;
using Microsoft.AspNetCore.Authorization;



namespace AtelierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AtelierContext _context;

        public AuthController(IConfiguration configuration, AtelierContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: api/Auth/Register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Validate the request
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)
            || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Username and Password are required.");
            }

            // Check if the username already exists
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return Conflict("Username already exists.");
            }



            // Create the new user
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new AtelierShared.Models.User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
                Role = "User", // Default role
                DateJoined = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // POST: api/Auth/Login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        private string GenerateToken(AtelierShared.Models.User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Use User ID
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("role", user.Role)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

