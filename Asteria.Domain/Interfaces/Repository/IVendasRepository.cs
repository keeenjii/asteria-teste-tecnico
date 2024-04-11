using Asteria.Domain.Entities;

namespace Asteria.Domain;

public interface IVendasRepository
{
    public Task AddRangeAsync(List<Vendas> vendas, CancellationToken ct);
    
    public Task SaveChangesAsync(CancellationToken ct);

    public Task BulkInsertAsync(List<Vendas> vendas, CancellationToken ct);
    
    public Task<(IEnumerable <Vendas> modeols, bool hasNext)> GetVendasList(
        int page, int pageSize, int searchCodigo, string? searchCategoria, string? searchSku, int searchMonth);
}
