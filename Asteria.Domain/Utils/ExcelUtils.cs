
using System.Data;
using Asteria.Domain.Entities;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Asteria.Domain.Utils;
public static class ExcelUtils
{
    public static DataTable Import(string filePath) 
    {
        XSSFWorkbook workbook;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            workbook = new XSSFWorkbook(stream);
        }

        var sheet = workbook.GetSheetAt(0);

        var dataTable = new DataTable();
        dataTable.Columns.Add("CodigoCliente", typeof(int));
        dataTable.Columns.Add("Categoria", typeof(string));
        dataTable.Columns.Add("sku", typeof(string));
        dataTable.Columns.Add("Data", typeof(DateTime));
        dataTable.Columns.Add("Quantidade", typeof(int));
        dataTable.Columns.Add("Faturamento", typeof(double));

        var currentRow = 1;
        while (currentRow <= sheet.LastRowNum)
        {
            var row = sheet.GetRow(currentRow);
            if (row == null) break;

            var dataRow = dataTable.NewRow();

            if (row.GetCell(0).CellType == CellType.Numeric)
            {
                dataRow["CodigoCliente"] = Convert.ToInt32(row.GetCell(0).NumericCellValue);
            }
            else if (row.GetCell(0).CellType == CellType.String)
            {
                int.TryParse(row.GetCell(0).StringCellValue, out int codigoCliente);
                dataRow["CodigoCliente"] = codigoCliente;
            }
            
            dataRow["Categoria"] = row.GetCell(1).StringCellValue;
            dataRow["sku"] = row.GetCell(2).StringCellValue;
            
            if (row.GetCell(3).CellType == CellType.Numeric)
            {
                dataRow["Data"] = Convert.ToDateTime(row.GetCell(3).DateCellValue);
            }
            
            if (row.GetCell(4).CellType == CellType.Numeric)
            {
                dataRow["Quantidade"] = Convert.ToInt32(row.GetCell(4).NumericCellValue);
            }
            else if (row.GetCell(4).CellType == CellType.String)
            {
                int.TryParse(row.GetCell(4).StringCellValue, out int quantidade);
                dataRow["Quantidade"] = quantidade;
            }
            
            if (row.GetCell(5).CellType == CellType.Numeric)
            {
                dataRow["Faturamento"] = Convert.ToDouble(row.GetCell(5).NumericCellValue);
            }
            else if (row.GetCell(5).CellType == CellType.String)
            {
                double.TryParse(row.GetCell(5).StringCellValue, out double faturamento);
                dataRow["Faturamento"] = faturamento;
            }

            dataTable.Rows.Add(dataRow);
            currentRow++;
        }

        return dataTable;
    }
    
}