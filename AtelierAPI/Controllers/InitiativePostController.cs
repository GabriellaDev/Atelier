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
        // Log detailed database error
        Console.WriteLine($"Database Update Error: {dbEx.Message}");
        return StatusCode(500, new { message = "Database update error occurred.", error = dbEx.Message });
    }
    catch (Exception ex)
    {
        // Log general error
        Console.WriteLine($"General Error: {ex.Message}");
        return StatusCode(500, new { message = "An error occurred while saving the initiative.", error = ex.Message });
    }
}


        // PUT: api/TravelPost/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInitiativePost(int id, InitiativePost initiativePost)
        {
            if (id != initiativePost.Id)
                return BadRequest();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.InitiativePosts.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/TravelPost/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInitiativePost(int id)
        {
            var initiative = await _context.InitiativePosts.FindAsync(id);
            if (initiative == null)
                return NotFound();

            _context.InitiativePosts.Remove(initiative);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
