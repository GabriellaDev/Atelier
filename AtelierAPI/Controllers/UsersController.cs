using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtelierAPI.Data;
using AtelierShared.Models;
using SharedUser = AtelierShared.Models.User;
using APIUser = AtelierShared.Models.User;


namespace AtelierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AtelierContext _context;

        public UsersController(AtelierContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SharedUser>>> GetUsers()
        {
            // Map database users to shared users
            var users = await _context.Users.ToListAsync();
            var sharedUsers = users.Select(user => new SharedUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = "example@example.com", 
                DateJoined = user.DateJoined
            });

            return Ok(sharedUsers);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SharedUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var sharedUser = new SharedUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = "example@example.com", 
                DateJoined = user.DateJoined 
            };

            return Ok(sharedUser);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<SharedUser>> PostUser(SharedUser sharedUser)
        {
            var apiUser = new APIUser
            {
                Username = sharedUser.Username,
                Password = HashPassword(sharedUser.Password),
                Role = "User",
                DateJoined = DateTime.UtcNow 
            };

            _context.Users.Add(apiUser);
            await _context.SaveChangesAsync();

            sharedUser.Id = apiUser.Id; // Set the ID returned by the database
            return CreatedAtAction("GetUser", new { id = sharedUser.Id }, sharedUser);
        }

    

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, SharedUser sharedUser)
        {
            if (id != sharedUser.Id)
            {
                return BadRequest();
            }

            var apiUser = await _context.Users.FindAsync(id);
            if (apiUser == null)
            {
                return NotFound();
            }

            // Update fields
            apiUser.Username = sharedUser.Username;
            apiUser.Password = HashPassword(sharedUser.Password); // Update password if needed

            _context.Entry(apiUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var apiUser = await _context.Users.FindAsync(id);
            if (apiUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(apiUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private string HashPassword(string password)
        {
            // Add your hashing logic here
            return password; // Placeholder
        }
    }
}
