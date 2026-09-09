using ClosedXML.Excel;
using Main.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

        // 2. Stream data to memory without saving physical files to the server disk
        var memoryStream = new MemoryStream();

        // Create an Excel workbook and write logs (each log serialized to JSON in a single column)
        using ( var workbook = new XLWorkbook () )
        {
            var ws = workbook.Worksheets.Add("ExceptionLogs");
            ws.Cell (1,1).Value = "Record";
            for ( int i = 0 ; i < logs.Count ; i++ )
            {
                ws.Cell (i + 2,1).Value = JsonSerializer.Serialize (logs[i]);
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