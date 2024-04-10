using Microsoft.AspNetCore.Http;

namespace Asteria.Domain;

public interface IVendasService 
{
    public Task<String> Upload(IFormFile file, CancellationToken ct);
}
