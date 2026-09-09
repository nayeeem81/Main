using Main.Infrastructure.DatabaseContext;
using Main.Infrastructure.ICrosscuttingServices;
using Main.Model.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public class ExceptionLoggingService: IExceptionLoggingService
{
    private readonly LogDbContext _LogContext;
    private readonly ILogger<ExceptionLoggingService> _logger;
    private readonly ITenantSetter _tenantSetter;

    public ExceptionLoggingService (
        LogDbContext logContext,
        ILogger<ExceptionLoggingService> logger,ITenantSetter tenantSetter)
    {
        _LogContext = logContext;
        _logger = logger;
        _tenantSetter = tenantSetter;
    }

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

            _logger.LogError (
                exception,
                "Exception occurred - ErrorCode: {ErrorCode}, StatusCode: {StatusCode}, UserId: {UserId}, Source: {Source}",
                errorCode,
                statusCode,
                userId ?? "Anonymous",
                source);


            var existingLog = await _LogContext.ExceptionLogs
                .AsNoTracking()
                .Where(e => e.ExceptionType == exception.GetType().Name
                    && e.ErrorCode == errorCode
                    && e.StatusCode == statusCode
                    && e.IsResolved == false)
                .OrderByDescending(e => e.CreatedAt)
                .FirstOrDefaultAsync();


            int occuranceCount = 1;
            if ( existingLog != null &&
                ( DateTime.UtcNow - existingLog.CreatedAt ).TotalHours < 1 )
            {
                existingLog.OccurrenceCount++;
                existingLog.CreatedAt = DateTime.UtcNow;
                _ = _LogContext.ExceptionLogs.Update (existingLog);

                occuranceCount = existingLog.OccurrenceCount;
            }

            ExceptionLogs exceptionLog;

            // Create new exception log entry
            exceptionLog = new ExceptionLogs
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
                UserId = string.IsNullOrEmpty (_tenantSetter.HttpContextUserId) ? "Anonymous" : _tenantSetter.HttpContextUserId,
                ClientIpAddress = clientIpAddress,
                CreatedAt = DateTime.UtcNow,
                Source = source,
                Environment = System.Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") ?? "Production",
                CustomData = customData,
                IsResolved = false,
                OccurrenceCount = occuranceCount
            };

            _ = _LogContext.ExceptionLogs.Add (exceptionLog);


            // Save to database
            _ = await _LogContext.SaveChangesAsync ();
        }
        catch ( Exception ex )
        {
            // Log failure to Serilog
            Log.Fatal (
                ex,
                "Failed to log exception to database - Original Exception: {OriginalException}",
                exception.Message);
        }
    }


    public async Task<List<ExceptionLogs>> GetExceptionsAsync (
        int? statusCode = null,
        string? errorCode = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isResolved = null,
        int pageNumber = 1,
        int pageSize = 20)
    {
        var query = _LogContext.ExceptionLogs.AsNoTracking();

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


    public async Task<(int Total,int Unresolved,int Today)> GetExceptionSummaryAsync ()
    {
        var today = DateTime.UtcNow.Date;

        var total = await _LogContext.ExceptionLogs.CountAsync();
        var unresolved = await _LogContext.ExceptionLogs.CountAsync(e => e.IsResolved == false);
        var todayCount = await _LogContext.ExceptionLogs.CountAsync(e => e.CreatedAt.Date == today);

        return (total,unresolved,todayCount);
    }


    public async Task MarkAsResolvedAsync (long exceptionId,string? notes = null)
    {
        var exceptionLog = await _LogContext.ExceptionLogs.FindAsync(exceptionId);

        if ( exceptionLog != null )
        {
            exceptionLog.IsResolved = true;
            exceptionLog.ResolutionNotes = notes;
            exceptionLog.ResolvedAt = DateTime.UtcNow;

            _ = _LogContext.ExceptionLogs.Update (exceptionLog);

            _ = await _LogContext.SaveChangesAsync (true);

            _logger.LogInformation (
                "Exception resolved - ExceptionId: {ExceptionId}, ErrorCode: {ErrorCode}",
                exceptionId,
                exceptionLog.ErrorCode);
        }
    }


    public async Task<ExceptionLogs?> GetExceptionByIdAsync (long id)
    {
        return await _LogContext.ExceptionLogs
            .AsNoTracking ()
            .FirstOrDefaultAsync (e => e.Id == id);
    }


    private static string? TruncateString (string? value,int maxLength)
    {
        if ( string.IsNullOrEmpty (value) )
        {
            return value;
        }

        return value.Length <= maxLength ? value : value.Substring (0,maxLength) + "...";
    }
}
