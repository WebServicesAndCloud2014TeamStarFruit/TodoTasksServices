namespace TodoTasks.FileExporter
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using TodoTasks.Services.Models;

    public static class ExcelFileCreator
    {
        public static void ExportReportToXlsxFile(ICollection<TaskModel> tasks, string userId)
        {


            var newFile = new FileInfo(@"..\..\..\Reports\ExcelReports\Profits report.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            ExcelPackage xlPackage = new ExcelPackage(newFile);

            using (xlPackage)
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Profit report");

                worksheet.Cells[1, 1].Value = "Product code";
                worksheet.Cells[1, 2].Value = "Product name";
                worksheet.Cells[1, 3].Value = "Sold in";
                worksheet.Cells[1, 4].Value = "Incomes";
                worksheet.Cells[1, 5].Value = "Taxes";

                var columnsCount = worksheet.Dimension.End.Column;

                var row = 2;
                //foreach (var product in reportsFromMySQL)
                //{
                //    var productTaxAndExpenses = reportsFromSQLite
                //        .Where(r => r.ProductCode.Equals(product.ProductCode))
                //        .Select(r => 
                //            new 
                //            {
                //                tax = r.TaxPercent, 
                //                expenses = r.Expenses
                //            })
                //        .FirstOrDefault();

                //    double profit;
                //    int tax;
                //    double expenses;
                //    if (productTaxAndExpenses != null)
                //    {
                //        tax = productTaxAndExpenses.tax;
                //        expenses = productTaxAndExpenses.expenses;
                //        profit = (product.TotalIncomes - (product.TotalIncomes * ((double)tax / 100d))) - expenses;
                //    }
                //    else
                //    {
                //        tax = 0;
                //        expenses = 0;
                //        profit = product.TotalIncomes;
                //    }

                //    worksheet.Cells[row, 1].Value = product.ProductCode;
                //    worksheet.Cells[row, 2].Value = product.Name;
                //    worksheet.Cells[row, 3].Value = (product.ShopNames.Count > 0 ? product.ShopNames[0] : "");
                //    worksheet.Cells[row, 4].Value = product.TotalIncomes;
                //    worksheet.Cells[row, 5].Value = tax;

                //    row++;
                //}

                for (int i = 1; i <= columnsCount; i++)
                {
                    worksheet.Cells[1, i].Style.Font.Size = 12;
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Column(i).Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    worksheet.Column(i).AutoFit();
                }

                xlPackage.Save();
            }
        }
    }
}
