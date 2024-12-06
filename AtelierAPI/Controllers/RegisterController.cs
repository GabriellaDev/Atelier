using Microsoft.AspNetCore.Mvc;
using AtelierAPI.Data;
using AtelierShared.Models;
using BCrypt.Net;


namespace AtelierAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly AtelierContext _context;

        public RegisterController(AtelierContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            var user = new AtelierShared.Models.User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password), // Hash password
                Role = "User", // Default role
                DateJoined = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
    }
}
