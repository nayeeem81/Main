using ClosedXML.Excel;
using Main.Infrastructure.DatabaseContext;
using Main.Model.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyDevelopers.Logging.Api.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class ExportController: ControllerBase
{
    private readonly LogDbContext _context;

    public ExportController (LogDbContext context)
    {
        _context = context;
    }

    [HttpGet ("download-logs")]
    public async Task<IActionResult> DownloadLogs ()
    {
        // 1. Fetch data efficiently using AsNoTracking (bypasses EF cache for fast reads)
        var logs = await _context.ExceptionLogs
                .AsNoTracking()
                .OrderByDescending(l => l.ModifiedDate)
                .ToListAsync();

        // 1. Stream data to memory without saving physical files to the server disk
        var memoryStream = new MemoryStream();

        // 2. Get properties
        PropertyInfo[] properties = typeof(ExceptionLogs).GetProperties();

        // 3. Create an Excel workbook and write logs (each log serialized to JSON in a single column)
        using ( var workbook = new XLWorkbook () )
        {
            var ws = workbook.Worksheets.Add("ExceptionLogs");

            for ( int col = 0 ; col < properties.Length ; col++ )
            {
                ws.Cell (1,col + 1).Value = properties[col].Name;
                ws.Cell (1,col + 1).Style.Font.Bold = true;
                ws.Cell (1,col + 1).Style.Font.FontSize = 15;
            }

            // 5. Write data rows
            for ( int i = 0 ; i < logs.Count ; i++ )
            {
                var log = logs[i];
                int row = i + 2;
                int index = i;
                for ( int col = 0 ; col < properties.Length ; col++ )
                {

                    if ( col == 0 )
                    {
                        ws.Cell (row,col + 1).Value = index + 1;
                        continue;
                    }

                    // 2. Extract the actual value
                    var value = properties[col].GetValue(log);

                    Type propertyType = properties[col].PropertyType;

                    // 3. Check the type to apply custom formats or logic
                    if ( propertyType == typeof (int) && value != null )
                    {
                        // It's an integer! You can cast it if needed
                        int intValue = (int)value;
                        ws.Cell (row,col + 1).Value = intValue;
                    }
                    else if ( propertyType == typeof (string) && value != null )
                    {
                        // It's text!
                        string textValue = (string)value;
                        ws.Cell (row,col + 1).Value = textValue;
                    }
                    else if ( propertyType == typeof (DateTime) && value != null )
                    {
                        // 1. Cast the raw object back to a true C# DateTime
                        DateTime dateValue = (DateTime)value;

                        // 2. Assign the true DateTime object directly to Excel
                        ws.Cell (row,col + 1).Value = dateValue;

                        // 3. Apply the visual display format (Excel uses 'mm' for minutes in format strings)
                        ws.Cell (row,col + 1).Style.DateFormat.Format = "yyyy-mm-dd hh:mm:ss";
                    }

                    else
                    {
                        if ( value == null )
                        {
                            ws.Cell (row,col + 1).Value = string.Empty;
                        }
                    }
                }

            }

            var lastRowRange = ws.LastRowUsed();
            if ( lastRowRange != null )
            {
                // 1. Get the exact range of rows and columns containing data
                var dataRange = ws.Range(2, 1, lastRowRange.RowNumber(), properties.Length);

                // 2. Apply font size to the whole range at once
                dataRange.Style.Font.FontSize = 14;

                // 2. Force horizontal alignment to the left
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // 3. Keep your existing styling and layout rules
                ws.ColumnsUsed ().Style.Alignment.WrapText = false;

                _ = ws.ColumnsUsed (XLCellsUsedOptions.AllContents)
                  .AdjustToContents (1,lastRowRange.RowNumber (),0.0,100.0);
            }

            workbook.SaveAs (memoryStream); // ClosedXML synchronous save to stream
        }


        memoryStream.Position = 0;

        // 3. Format file name with today's date stamp
        string fileName = $"ExceptionLogs_{DateTime.Now:yyyyMMdd}.xlsx";

        // 4. Return file with official Excel MIME headers
        return File (
            memoryStream,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName
        );
    }
}