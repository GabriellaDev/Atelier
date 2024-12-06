using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtelierAPI.Data;
using AtelierShared.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AtelierAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class InitiativePostController : ControllerBase
    {
        private readonly AtelierContext _context;

        public InitiativePostController(AtelierContext context)
        {
            _context = context;
        }

        // GET: api/InitiativePosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InitiativePost>>> GetInitiativePosts()
        {
            try
            {
                // Include related Category navigation property
                var initiatives = await _context.InitiativePosts
                    .Include(i => i.Category) // Load Category details
                    .ToListAsync();

                return Ok(initiatives);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching initiatives: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching initiatives.");
            }
        }


        // GET: api/InitiativePost/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InitiativePost>> GetInitiativePost(int id)
        {
            var initiative = await _context.InitiativePosts.FindAsync(id);

            if (initiative == null)
                return NotFound();

            return Ok(initiative);
        }

        // POST: api/InitiativePost
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InitiativePost>> PostInitiativePost(InitiativePost initiativePostRequest)
        {
            // Extract User ID from the token using the correct claim type
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new
                {
                    message = "User ID not found in token.",
                    claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
                });
            }

            Console.WriteLine($"Extracted User ID: {userId}");

            if (initiativePostRequest.Category == null || initiativePostRequest.Category.Id <= 0)
            {
                throw new ArgumentException("Invalid or missing Category.");
            }

            // Create a new InitiativePost object to save to the database
            var initiativePost = new InitiativePost
            {
                Title = initiativePostRequest.Title,
                Content = initiativePostRequest.Content,
                AuthorId = int.Parse(userId), // Use the ID from the token
                DatePublished = DateTime.UtcNow,
                CategoryId = initiativePostRequest.CategoryId
            };

            try
            {
                // Add the new InitiativePost object to the database
                _context.InitiativePosts.Add(initiativePost);
                await _context.SaveChangesAsync();

                // Return the newly created object
                return CreatedAtAction(nameof(GetInitiativePost), new { id = initiativePost.Id }, initiativePost);
            }
            catch (DbUpdateException dbEx)
            {

                Console.WriteLine($"Database Update Error: {dbEx.Message}");
                return StatusCode(500, new { message = "Database update error occurred.", error = dbEx.Message });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"General Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while saving the initiative.", error = ex.Message });
            }
        }


        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InitiativePost>>> GetUserInitiatives()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            var userInitiatives = await _context.InitiativePosts
                .Where(i => i.AuthorId == int.Parse(userId))
                .Include(i => i.Category) // Include Category if needed
                .ToListAsync();

            return Ok(userInitiatives);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInitiative(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var initiative = await _context.InitiativePosts.FindAsync(id);

            if (initiative == null)
            {
                return NotFound();
            }

            if (initiative.AuthorId != int.Parse(userId))
            {
                return Forbid("You can only delete your own initiatives.");
            }

            _context.InitiativePosts.Remove(initiative);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInitiative(int id, [FromBody] InitiativePost updatedInitiative)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var initiative = await _context.InitiativePosts.FindAsync(id);

            if (initiative == null)
            {
                return NotFound();
            }

            if (initiative.AuthorId != int.Parse(userId))
            {
                return Forbid("You can only edit your own initiatives.");
            }

            initiative.Title = updatedInitiative.Title;
            initiative.Content = updatedInitiative.Content;
            initiative.CategoryId = updatedInitiative.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error: {ex.Message}");
                return StatusCode(500, "Failed to update initiative.");
            }
        }



    }
}