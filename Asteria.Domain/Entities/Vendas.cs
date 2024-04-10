namespace Asteria.Domain.Entities;

public class Vendas
{
    public int Id { get; set; }
    public int CodigoCliente { get; set; }
    public string? Categoria { get; set; }
    public string? sku { get; set; }
    public DateTime Data { get; set; }
    public int Quantidade { get; set; }
    public double Faturamento { get; set; }
}
