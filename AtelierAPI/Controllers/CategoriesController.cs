using AtelierAPI.Data;
using AtelierShared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AtelierContext _context;

    public CategoriesController(AtelierContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    [HttpGet] // This is required to specify the HTTP verb for this action
    public async Task<ActionResult<List<CategoryModel>>> GetCategories()
    {
        var categories = await _context.Category.ToListAsync();
        return Ok(categories);
    }
}
