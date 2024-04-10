
using Asteria.Domain.Entities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Asteria.Domain.Utils;
public static class ExcelUtils
{
    public static List<Vendas> Import(string filePath) 
    {
        XSSFWorkbook workbook;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            workbook = new XSSFWorkbook(stream);
        }

        var sheet = workbook.GetSheetAt(0);

        var listResult = new List<Vendas>();
        var currentRow = 1;
        while (currentRow <= sheet.LastRowNum)
        {
            var row = sheet.GetRow(currentRow);
            if (row == null) break;

            var obj = new Vendas();

            if (row.GetCell(0).CellType == CellType.Numeric)
            {
                obj.CodigoCliente = Convert.ToInt32(row.GetCell(0).NumericCellValue);
            }
            else if (row.GetCell(0).CellType == CellType.String)
            {
                int.TryParse(row.GetCell(0).StringCellValue, out int codigoCliente);
                obj.CodigoCliente = codigoCliente;
            }
            
            obj.Categoria = row.GetCell(1).StringCellValue;
            obj.sku = row.GetCell(2).StringCellValue;
            
            if (row.GetCell(3).CellType == CellType.Numeric)
            {
                obj.Data = Convert.ToDateTime(row.GetCell(3).DateCellValue);
            }
            
            if (row.GetCell(4).CellType == CellType.Numeric)
            {
                obj.Quantidade = Convert.ToInt32(row.GetCell(4).NumericCellValue);
            }
            else if (row.GetCell(4).CellType == CellType.String)
            {
                int.TryParse(row.GetCell(4).StringCellValue, out int quantidade);
                obj.Quantidade = quantidade;
            }
            
            if (row.GetCell(5).CellType == CellType.Numeric)
            {
                obj.Faturamento = Convert.ToDouble(row.GetCell(5).NumericCellValue);
            }
            else if (row.GetCell(5).CellType == CellType.String)
            {
                double.TryParse(row.GetCell(5).StringCellValue, out double faturamento);
                obj.Faturamento = faturamento;
            }

            listResult.Add(obj);
            currentRow++;
        }

        return listResult;
    }
    
}