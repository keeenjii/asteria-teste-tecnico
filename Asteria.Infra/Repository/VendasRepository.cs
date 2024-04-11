using System.Diagnostics.CodeAnalysis;
using Asteria.Domain;
using Asteria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Asteria.Infra;

public class VendasRepository : IVendasRepository
{
    private readonly AppDbContext _context;

    public VendasRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(List<Vendas> vendas, CancellationToken ct) => 
        await _context.AddRangeAsync(vendas, ct);

    public async Task SaveChangesAsync(CancellationToken ct) => 
        await _context.SaveChangesAsync(ct);

    public async Task BulkInsertAsync(List<Vendas> vendas, CancellationToken ct)
    {
    }

    public async Task<(IEnumerable <Vendas> modeols, bool hasNext)> GetVendasList(
        int page, int pageSize, int searchCodigo, string? searchCategoria, string? searchSku, int searchMonth){

            IQueryable<Vendas> query;
            
            if (searchCodigo > 0)
                query = _context.Vendas.Where(x => x.CodigoCliente == searchCodigo);
            else
                query = _context.Vendas;

            if (!string.IsNullOrEmpty(searchCategoria))
                query = query.Where(x => x.Categoria == searchCategoria);

            if (!string.IsNullOrEmpty(searchSku))
                query = query.Where(x => x.sku == searchSku);

            if (searchMonth > 0)
                query = query.Where(x => x.Data.Month == searchMonth);

            var listaVendas = await query.OrderByDescending(x => x.CodigoCliente)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return (listaVendas, listaVendas.Count == pageSize);
        }
}
