using Asteria.Domain;
using Asteria.Domain.Entities;
using Asteria.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Iana;

namespace Asteria.WebApp.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/vendas")]
public class VendasController : Controller
{

    private readonly IVendasService _vendasService;

    public VendasController(IVendasService vendasService)
    {
        _vendasService = vendasService;
    }

    [HttpGet("get-vendas-list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Vendas>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVendasList(int page = 1, int pageSize = 10, 
    int searchCodigo = 0, string? searchCategoria = "", string? searchSku = "", int searchMonth = 0) 
    {
        try
        {
            var (result, hasNext) = await _vendasService.GetVendasList(page, pageSize, searchCodigo, searchCategoria, searchSku, searchMonth);
            return Ok(new{result, hasNext});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
