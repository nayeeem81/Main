using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Main.Infrastructure.Services;

/// <summary>
/// Service for logging exceptions to database and file
/// </summary>
public class ExceptionLoggingService: IExceptionLoggingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ITenantSetter _tenantSetter;
    private readonly ILogger _logger;

    public ExceptionLoggingService (
        ApplicationDbContext dbContext,
        ITenantSetter tenantSetter,
        ILogger logger)
    {
        _dbContext = dbContext;
        _tenantSetter = tenantSetter;
        _logger = logger;
    }

    /// <summary>
    /// Logs an exception to the database and file
    /// </summary>
    public async Task LogExceptionAsync (
        Exception exception,
        string errorCode,
        int statusCode,
        string userMessage,
        string? userId = null,
        string? clientIpAddress = null,
        string? requestUrl = null,
        string? httpMethod = null,
        string? requestHeaders = null,
        string? requestBody = null,
        string? customData = null,
        string source = "API")
    {
        try
        {
            // Log to Serilog (file)
            _logger.Error (
                exception,
                "Exception occurred - ErrorCode: {ErrorCode}, StatusCode: {StatusCode}, UserId: {UserId}, Source: {Source}",
                errorCode,
                statusCode,
                userId ?? "Anonymous",
                source);

            // Try to find existing exception log to increment occurrence count
            var existingLog = await _dbContext.ExceptionLogs
                .AsNoTracking()
                .Where(e => e.ExceptionType == exception.GetType().Name
                    && e.ErrorCode == errorCode
                    && e.StatusCode == statusCode
                    && e.IsResolved == false)
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();

            ExceptionLog exceptionLog;

            if ( existingLog != null &&
                ( DateTime.UtcNow - existingLog.CreatedAt ).TotalHours < 1 ) // Within 1 hour
            {
                // Update existing log
                existingLog.OccurrenceCount++;
                existingLog.CreatedAt = DateTime.UtcNow; // Update timestamp
                _ = _dbContext.ExceptionLogs.Update (existingLog);
                exceptionLog = existingLog;
            }
            else
            {
                // Create new exception log entry
                exceptionLog = new ExceptionLog
                {
                    ExceptionType = exception.GetType ().Name,
                    StatusCode = statusCode,
                    ErrorCode = errorCode,
                    DetailedMessage = exception.Message,
                    StackTrace = exception.StackTrace,
                    InnerException = exception.InnerException?.ToString (),
                    UserMessage = userMessage,
                    RequestUrl = requestUrl,
                    HttpMethod = httpMethod,
                    RequestHeaders = TruncateString (requestHeaders,2000),
                    RequestBody = TruncateString (requestBody,2000),
                    UserId = userId,
                    ClientIpAddress = clientIpAddress,
                    CreatedAt = DateTime.UtcNow,
                    Source = source,
                    Environment = System.Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") ?? "Production",
                    CustomData = customData,
                    IsResolved = false,
                    OccurrenceCount = 1
                };

                _ = _dbContext.ExceptionLogs.Add (exceptionLog);
            }

            // Save to database
            _ = await _dbContext.SaveChangesAsync ();
        }
        catch ( Exception ex )
        {
            // Log failure to Serilog
            _logger.Fatal (
                ex,
                "Failed to log exception to database - Original Exception: {OriginalException}",
                exception.Message);
        }
    }

    /// <summary>
    /// Retrieves logged exceptions with pagination and filtering
    /// </summary>
    public async Task<List<ExceptionLog>> GetExceptionsAsync (
        int? statusCode = null,
        string? errorCode = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isResolved = null,
        int pageNumber = 1,
        int pageSize = 20)
    {
        var query = _dbContext.ExceptionLogs.AsNoTracking();

        // Apply filters
        if ( statusCode.HasValue )
        {
            query = query.Where (e => e.StatusCode == statusCode);
        }

        if ( !string.IsNullOrEmpty (errorCode) )
        {
            query = query.Where (e => e.ErrorCode.Contains (errorCode));
        }

        if ( startDate.HasValue )
        {
            query = query.Where (e => e.CreatedAt >= startDate);
        }

        if ( endDate.HasValue )
        {
            query = query.Where (e => e.CreatedAt <= endDate);
        }

        if ( isResolved.HasValue )
        {
            query = query.Where (e => e.IsResolved == isResolved);
        }

        // Pagination and sorting
        var exceptions = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return exceptions;
    }

    /// <summary>
    /// Gets exception summary statistics
    /// </summary>
    public async Task<(int Total,int Unresolved,int Today)> GetExceptionSummaryAsync ()
    {
        var today = DateTime.UtcNow.Date;

        var total = await _dbContext.ExceptionLogs.CountAsync();
        var unresolved = await _dbContext.ExceptionLogs.CountAsync(e => e.IsResolved == false);
        var todayCount = await _dbContext.ExceptionLogs.CountAsync(e => e.CreatedAt.Date == today);

        return (total,unresolved,todayCount);
    }

    /// <summary>
    /// Marks an exception log as resolved
    /// </summary>
    public async Task MarkAsResolvedAsync (long exceptionId,string? notes = null)
    {
        var exceptionLog = await _dbContext.ExceptionLogs.FindAsync(exceptionId);

        if ( exceptionLog != null )
        {
            exceptionLog.IsResolved = true;
            exceptionLog.ResolutionNotes = notes;
            exceptionLog.ResolvedAt = DateTime.UtcNow;

            _ = _dbContext.ExceptionLogs.Update (exceptionLog);
            _ = await _dbContext.SaveChangesAsync ();

            _logger.Information (
                "Exception resolved - ExceptionId: {ExceptionId}, ErrorCode: {ErrorCode}",
                exceptionId,
                exceptionLog.ErrorCode);
        }
    }

    /// <summary>
    /// Gets a specific exception by ID
    /// </summary>
    public async Task<ExceptionLog?> GetExceptionByIdAsync (long id)
    {
        return await _dbContext.ExceptionLogs
            .AsNoTracking ()
            .FirstOrDefaultAsync (e => e.Id == id);
    }

    /// <summary>
    /// Truncates string to maximum length
    /// </summary>
    private static string? TruncateString (string? value,int maxLength)
    {
        if ( string.IsNullOrEmpty (value) )
        {
            return value;
        }

        return value.Length <= maxLength ? value : value.Substring (0,maxLength) + "...";
    }
}
