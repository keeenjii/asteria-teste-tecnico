using Asteria.Domain;
using Asteria.Domain.Entities;
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
    {}
}
