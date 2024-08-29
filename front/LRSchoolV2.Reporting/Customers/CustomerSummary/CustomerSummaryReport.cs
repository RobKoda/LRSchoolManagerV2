using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Reporting.Customers.CustomerSummary;

public static class CustomerSummaryReport
{
    public static byte[] ConvertToXlsxByteArray(IEnumerable<PersonSummaryLineDisplay> inSummaryLines)
    {
        var summaryLines = inSummaryLines.ToList();
        var orderedSummaryLines = summaryLines.OrderBy(inSummaryLine => inSummaryLine.Date);
        using var memoryStream = new MemoryStream();
        using (var spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
        {
            
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();
            
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            
            var sheets = spreadsheetDocument.WorkbookPart!.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Data"
            };
            sheets.Append(sheet);
            
            var columns = new Columns();
            columns.Append(new Column { Min = (UInt32Value)1U, Max = (UInt32Value)15U, Width = 20D, CustomWidth = true });
            worksheetPart.Worksheet.InsertBefore(columns, worksheetPart.Worksheet.GetFirstChild<SheetData>());
            
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;
            var properties = typeof(PersonSummaryLineDisplay).GetProperties();
            var headerRow = new Row();
            foreach (var property in properties)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(property.Name)
                };
                headerRow.AppendChild(cell);
            }
            headerRow.AppendChild(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue("Balance")
            });
            
            sheetData.AppendChild(headerRow);
            
            var balance = 0m;
            foreach (var item in orderedSummaryLines)
            {
                var dataRow = new Row();
                foreach (var property in properties)
                {
                    var value = property.GetValue(item)?.ToString() ?? string.Empty;
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(value)
                    };
                    dataRow.AppendChild(cell);
                }
                
                if (!string.IsNullOrWhiteSpace(item.Debit))
                {
                    balance -= decimal.Parse(item.Debit);
                }
                
                if (!string.IsNullOrWhiteSpace(item.Credit))
                {
                    balance += decimal.Parse(item.Credit);
                }
                
                dataRow.AppendChild(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(balance)
                });
                
                sheetData.AppendChild(dataRow);
            }
            
            workbookPart.Workbook.Save();
        }
        
        return memoryStream.ToArray();
    }
}