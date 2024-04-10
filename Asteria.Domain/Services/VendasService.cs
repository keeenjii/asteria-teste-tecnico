using System.Diagnostics;
using Asteria.Domain.Entities;
using Asteria.Domain.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asteria.Domain;

public class VendasService : IVendasService
{
    public readonly IVendasRepository _vendasRepository;
    public readonly IWebHostEnvironment _env;

    public VendasService(IVendasRepository vendasRepository, IWebHostEnvironment env)
    {
        _vendasRepository = vendasRepository;
        _env = env;
    }

    public async Task<String> Upload(IFormFile file, CancellationToken ct)
    {   
        try {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var filePath = SaveFile(file);

            var vendasImport = ExcelUtils.Import(filePath);
            
            await _vendasRepository.AddRangeAsync(vendasImport, ct);
            await _vendasRepository.SaveChangesAsync(ct);

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            return $"File uploaded successfully. Elapsed time: {elapsedTime}";

        } 
        catch (Exception e) {
            return "No file uploaded. Error:" + e.Message;
        }
    }
    private string SaveFile(IFormFile file)
    {
        if (file.Length == 0)
        {
            throw new BadHttpRequestException("File is empty.");
        }

        var extension = Path.GetExtension(file.FileName);

        var webRootPath = _env.WebRootPath;
        if (string.IsNullOrWhiteSpace(webRootPath))
        {
            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }
            
        var folderPath = Path.Combine(webRootPath, "uploads");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
            
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(folderPath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);

        return filePath;
    }
}
