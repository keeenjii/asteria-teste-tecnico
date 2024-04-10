namespace Asteria.Domain;

public class VendasRequest
{
    public int CodigoCliente { get; set; }
    public string? CategoriadoProduto { get; set; }
    public string? SkuProduto { get; set; }
    public DateTime Data { get; set; }
    public int Quantidade { get; set; }
    public double ValordeFaturamento { get; set; }
}
