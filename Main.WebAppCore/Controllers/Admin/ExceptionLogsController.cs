using Domain.Model;
using Main.Common;
using Main.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Main.WebAppCore;

/// <summary>
/// API controller for managing and viewing exception logs
/// Restricted to administrators only
/// </summary>
[ApiController]
[Route ("api/[controller]")]
[Authorize (Roles = "Admin")]
public class ExceptionLogsController: ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IExceptionLoggingService _exceptionLoggingService;

    public ExceptionLogsController (
        ApplicationDbContext dbContext,
        IExceptionLoggingService exceptionLoggingService)
    {
        _dbContext = dbContext;
        _exceptionLoggingService = exceptionLoggingService;
    }

    /// <summary>
    /// Gets paginated list of exception logs with optional filtering
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>Paginated list of exception logs</returns>
    [HttpPost ("search")]
    public async Task<ActionResult<PaginatedResponse<ExceptionLogViewModel>>> SearchExceptionLogs (
        [FromBody] ExceptionLogFilterRequest filter)
    {
        try
        {
            // Get filtered exceptions
            var exceptions = await _exceptionLoggingService.GetExceptionsAsync(
                statusCode: filter.StatusCode,
                errorCode: filter.ErrorCode,
                startDate: filter.StartDate,
                endDate: filter.EndDate,
                isResolved: filter.IsResolved,
                pageNumber: filter.PageNumber,
                pageSize: filter.PageSize);

            // Get total count for pagination
            var query = _dbContext.ExceptionLogs.AsNoTracking();

            if ( filter.StatusCode.HasValue )
            {
                query = query.Where (e => e.StatusCode == filter.StatusCode);
            }

            if ( !string.IsNullOrEmpty (filter.ErrorCode) )
            {
                query = query.Where (e => e.ErrorCode.Contains (filter.ErrorCode));
            }

            if ( filter.StartDate.HasValue )
            {
                query = query.Where (e => e.CreatedAt >= filter.StartDate);
            }

            if ( filter.EndDate.HasValue )
            {
                query = query.Where (e => e.CreatedAt <= filter.EndDate);
            }

            if ( filter.IsResolved.HasValue )
            {
                query = query.Where (e => e.IsResolved == filter.IsResolved);
            }

            var totalCount = await query.CountAsync();

            // Map to view model
            var items = exceptions.Select(MapToViewModel).ToList();

            var response = new PaginatedResponse<ExceptionLogViewModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            return Ok (response);
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Gets a specific exception log by ID
    /// </summary>
    /// <param name="id">Exception log ID</param>
    /// <returns>Exception log details</returns>
    [HttpGet ("{id}")]
    public async Task<ActionResult<ExceptionLogViewModel>> GetExceptionLog (long id)
    {
        try
        {
            var exceptionLog = await _exceptionLoggingService.GetExceptionByIdAsync(id);

            if ( exceptionLog == null )
            {
                return NotFound (new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.NOT_FOUND,
                    Message = "Exception log not found.",
                    StatusCode = StatusCodes.Status404NotFound,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            return Ok (MapToViewModel (exceptionLog));
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Gets exception logs summary/dashboard data
    /// </summary>
    /// <returns>Exception summary statistics</returns>
    [HttpGet ("summary/stats")]
    public async Task<ActionResult<ExceptionSummary>> GetExceptionSummary ()
    {
        try
        {
            var (total,unresolved,today) = await _exceptionLoggingService.GetExceptionSummaryAsync ();

            // Get exception types distribution
            var exceptionsByType = await _dbContext.ExceptionLogs
                .AsNoTracking()
                .GroupBy(e => e.ExceptionType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type, x => x.Count);

            // Get status code distribution
            var exceptionsByStatus = await _dbContext.ExceptionLogs
                .AsNoTracking()
                .GroupBy(e => e.StatusCode)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status.ToString(), x => x.Count);

            // Get trends (last 7 days)
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
            var trends = await _dbContext.ExceptionLogs
                .AsNoTracking()
                .Where(e => e.CreatedAt >= sevenDaysAgo)
                .GroupBy(e => e.CreatedAt.Date)
                .Select(g => new ExceptionTrend
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(t => t.Date)
                .ToListAsync();

            var summary = new ExceptionSummary
            {
                TotalExceptions = total,
                UnresolvedCount = unresolved,
                TodayCount = today,
                ExceptionsByType = exceptionsByType,
                ExceptionsByStatus = exceptionsByStatus,
                Trends = trends
            };

            return Ok (summary);
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Marks an exception as resolved
    /// </summary>
    /// <param name="id">Exception log ID</param>
    /// <param name="request">Resolution details</param>
    /// <returns>Success response</returns>
    [HttpPut ("{id}/resolve")]
    public async Task<IActionResult> ResolveException (
        long id,
        [FromBody] ResolveExceptionRequest request)
    {
        try
        {
            var exceptionLog = await _exceptionLoggingService.GetExceptionByIdAsync(id);

            if ( exceptionLog == null )
            {
                return NotFound (new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.NOT_FOUND,
                    Message = "Exception log not found.",
                    StatusCode = StatusCodes.Status404NotFound,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            await _exceptionLoggingService.MarkAsResolvedAsync (id,request.Notes);

            return Ok (new
            {
                message = "Exception marked as resolved successfully."
            });
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Exports exception logs to CSV format
    /// </summary>
    /// <param name="filter">Filter criteria</param>
    /// <returns>CSV file</returns>
    [HttpPost ("export/csv")]
    public async Task<IActionResult> ExportExceptionsAsCsv (
        [FromBody] ExceptionLogFilterRequest filter)
    {
        try
        {
            var query = _dbContext.ExceptionLogs.AsNoTracking();

            // Apply filters
            if ( filter.StatusCode.HasValue )
            {
                query = query.Where (e => e.StatusCode == filter.StatusCode);
            }

            if ( !string.IsNullOrEmpty (filter.ErrorCode) )
            {
                query = query.Where (e => e.ErrorCode.Contains (filter.ErrorCode));
            }

            if ( filter.StartDate.HasValue )
            {
                query = query.Where (e => e.CreatedAt >= filter.StartDate);
            }

            if ( filter.EndDate.HasValue )
            {
                query = query.Where (e => e.CreatedAt <= filter.EndDate);
            }

            if ( filter.IsResolved.HasValue )
            {
                query = query.Where (e => e.IsResolved == filter.IsResolved);
            }

            var exceptions = await query
                .OrderByDescending(e => e.CreatedAt)
                .Take(10000) // Limit to prevent memory issues
                .ToListAsync();

            // Generate CSV
            var csv = new System.Text.StringBuilder();
            _ = csv.AppendLine ("ID,ErrorCode,ExceptionType,StatusCode,Message,UserId,ClientIp,CreatedAt,Source,IsResolved");

            foreach ( var exc in exceptions )
            {
                _ = csv.AppendLine (
                    $"\"{exc.Id}\",\"{exc.ErrorCode}\",\"{exc.ExceptionType}\",\"{exc.StatusCode}\"," +
                    $"\"{EscapeCsvValue (exc.DetailedMessage)}\",\"{exc.UserId ?? "N/A"}\",\"{exc.ClientIpAddress ?? "N/A"}\"," +
                    $"\"{exc.CreatedAt:yyyy-MM-dd HH:mm:ss}\",\"{exc.Source}\",\"{exc.IsResolved}\"");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            return File (
                bytes,
                "text/csv",
                $"exception_logs_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv");
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Bulk delete resolved exceptions older than specified days
    /// </summary>
    /// <param name="days">Days to keep (delete older than this)</param>
    /// <returns>Number of deleted records</returns>
    [HttpDelete ("cleanup")]
    [Authorize (Roles = "SuperAdmin")] // More restrictive for delete operations
    public async Task<IActionResult> CleanupOldExceptions ([FromQuery] int days = 90)
    {
        try
        {
            if ( days < 7 )
            {
                return BadRequest (new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INVALID_ARGUMENT_ERROR,
                    Message = "Minimum retention period is 7 days.",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            var cutoffDate = DateTime.UtcNow.AddDays(-days);

            var recordsToDelete = await _dbContext.ExceptionLogs
                .Where(e => e.CreatedAt < cutoffDate && e.IsResolved)
                .ToListAsync();

            if ( recordsToDelete.Count > 0 )
            {
                _dbContext.ExceptionLogs.RemoveRange (recordsToDelete);
                _ = await _dbContext.SaveChangesAsync ();
            }

            return Ok (new
            {
                deletedCount = recordsToDelete.Count
            });
        }
        catch
        {
            return StatusCode (
                StatusCodes.Status500InternalServerError,
                new ErrorResponse
                {
                    ErrorCode = ExceptionErrorCodes.INTERNAL_SERVER_ERROR,
                    Message = UserFriendlyMessages.DATABASE_ERROR,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                    TraceId = HttpContext.TraceIdentifier
                });
        }
    }

    /// <summary>
    /// Maps ExceptionLog entity to ViewModel
    /// </summary>
    private static ExceptionLogViewModel MapToViewModel (ExceptionLog log)
    {
        return new ExceptionLogViewModel
        {
            Id = log.Id,
            ExceptionType = log.ExceptionType,
            StatusCode = log.StatusCode,
            ErrorCode = log.ErrorCode,
            DetailedMessage = log.DetailedMessage,
            StackTrace = log.StackTrace,
            InnerException = log.InnerException,
            RequestUrl = log.RequestUrl,
            HttpMethod = log.HttpMethod,
            UserId = log.UserId,
            ClientIpAddress = log.ClientIpAddress,
            CreatedAt = log.CreatedAt,
            Source = log.Source,
            Environment = log.Environment,
            IsResolved = log.IsResolved,
            ResolutionNotes = log.ResolutionNotes,
            OccurrenceCount = log.OccurrenceCount
        };
    }

    /// <summary>
    /// Escapes CSV values to handle commas and quotes
    /// </summary>
    private static string EscapeCsvValue (string? value)
    {
        if ( string.IsNullOrEmpty (value) )
        {
            return string.Empty;
        }

        return value.Replace ("\"","\"\"");
    }
}

/// <summary>
/// Request model for resolving exceptions
/// </summary>
public class ResolveExceptionRequest
{
    /// <summary>
    /// Resolution notes from the developer
    /// </summary>
    public string? Notes
    {
        get; set;
    }
}
