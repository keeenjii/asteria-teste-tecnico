using Asteria.Domain;
using Asteria.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asteria.WebApp.Controllers;

[ApiController]
[Produces("application/json")]
[Route("")]
public class SetupController : Controller
{

    private readonly IVendasService _vendasService;
    private readonly AppDbContext _context;

    public SetupController(IVendasService vendasService, AppDbContext context)
    {
        _vendasService = vendasService;
        _context = context;
    }

    [HttpPost("Upload")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [IgnoreAntiforgeryToken] // Add this attribute to disable anti-forgery token validation
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct) 
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido");
            }
            var result = await _vendasService.Upload(file, ct);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddMigrations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddMigrations() 
    {
        try
        {
            _context.Database.Migrate();
            return Ok("Migration created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
