using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using Asteria.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

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

    // TODO: BulkInsert, Transaction
    // Primeiro teste: 5 minutos e 22 segundos
    public async Task<String> Upload(IFormFile file, CancellationToken ct, int pageSize = 5000)
    {   
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        try {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, ct);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    int totalPages = (int)Math.Ceiling((double)rowCount / pageSize);

                    for (int page = 0; page < totalPages; page++)
                    {
                        var vendasImport = new List<Vendas>();
                        int startRow = page * pageSize + 1; // Ignorando a primeira linha, de cabeçalho
                        int endRow = Math.Min(startRow + pageSize, rowCount);
                        for (int row = startRow; row < endRow; row++)
                        {
                            var venda = new Vendas();

                            venda.CodigoCliente = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString());
                            venda.Categoria = worksheet.Cells[row, 2].Value.ToString();
                            venda.sku = worksheet.Cells[row, 3].Value.ToString();
                            
                            string dateValue = worksheet.Cells[row, 4].Value?.ToString();
                            if (!string.IsNullOrEmpty(dateValue))
                            {
                                if (double.TryParse(dateValue, out double oaDate))
                                {
                                    venda.Data = DateTime.FromOADate(oaDate);
                                }
                                else
                                {
                                    DateTime.TryParseExact(dateValue, new[] { "MM/dd/yyyy", "yyyy-MM-dd", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);
                                    venda.Data = parsedDate;
                                }
                            }
                                                        
                            venda.Quantidade = Convert.ToInt32(worksheet.Cells[row, 5].Value.ToString());
                            venda.Faturamento = Convert.ToDouble(worksheet.Cells[row, 6].Value.ToString());

                            vendasImport.Add(venda);
                        }
                        await insertData(vendasImport, ct);
                    }
                    
                }
            }

            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            return $"File uploaded successfully. Elapsed time: {elapsedTime}";

        } 
        catch (Exception e) {
            return "No file uploaded. Error:" + e.Message;
        }
    }

    private async Task insertData(List<Vendas> vendasImport, CancellationToken ct)
    {
        await _vendasRepository.AddRangeAsync(vendasImport, ct);
        await _vendasRepository.SaveChangesAsync(ct);
    }

    public async Task<(IEnumerable <Vendas> models, bool hasNext)> GetVendasList(
        int page, int pageSize, int searchCodigo, string? searchCategoria, string? searchSku, int searchMonth)
    {
        return await _vendasRepository.GetVendasList(page, pageSize, searchCodigo, searchCategoria, searchSku, searchMonth);
    }
}
