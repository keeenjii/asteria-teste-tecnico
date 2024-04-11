using Asteria.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Asteria.Domain;

public interface IVendasService 
{
    public Task<String> Upload(IFormFile file, CancellationToken ct, int pageSize = 5000);
    public Task<(IEnumerable <Vendas> models, bool hasNext)> GetVendasList(
        int page, int pageSize, int searchCodigo, string? searchCategoria, string? searchSku, int searchMonth);
}
