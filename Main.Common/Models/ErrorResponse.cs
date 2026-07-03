namespace Main.Common;

/// <summary>
/// API Exception Response Model - returned to clients on errors
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Unique error code for this error type
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// User-friendly error message (safe to display)
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode
    {
        get; set;
    }

    /// <summary>
    /// Timestamp when error occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Request path where error occurred
    /// </summary>
    public string? Path
    {
        get; set;
    }

    /// <summary>
    /// Trace ID for correlating logs
    /// </summary>
    public string? TraceId
    {
        get; set;
    }

    /// <summary>
    /// Validation errors (if applicable)
    /// </summary>
    public Dictionary<string,List<string>>? ValidationErrors
    {
        get; set;
    }

    /// <summary>
    /// Additional error details for developers (only in development)
    /// </summary>
    public string? Details
    {
        get; set;
    }
}

/// <summary>
/// Exception log view model for displaying logs
/// </summary>
public class ExceptionLogViewModel
{
    public long Id
    {
        get; set;
    }
    public string ExceptionType { get; set; } = string.Empty;
    public int StatusCode
    {
        get; set;
    }
    public string ErrorCode { get; set; } = string.Empty;
    public string DetailedMessage { get; set; } = string.Empty;
    public string? StackTrace
    {
        get; set;
    }
    public string? InnerException
    {
        get; set;
    }
    public string? RequestUrl
    {
        get; set;
    }
    public string? HttpMethod
    {
        get; set;
    }
    public string? UserId
    {
        get; set;
    }
    public string? ClientIpAddress
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
    public string? Source
    {
        get; set;
    }
    public string? Environment
    {
        get; set;
    }
    public bool IsResolved
    {
        get; set;
    }
    public string? ResolutionNotes
    {
        get; set;
    }
    public int OccurrenceCount
    {
        get; set;
    }
}

/// <summary>
/// Filter parameters for querying exception logs
/// </summary>
public class ExceptionLogFilterRequest
{
    public string? ExceptionType
    {
        get; set;
    }
    public string? ErrorCode
    {
        get; set;
    }
    public int? StatusCode
    {
        get; set;
    }
    public DateTime? StartDate
    {
        get; set;
    }
    public DateTime? EndDate
    {
        get; set;
    }
    public string? UserId
    {
        get; set;
    }
    public string? Source
    {
        get; set;
    }
    public bool? IsResolved
    {
        get; set;
    }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Paginated response model
/// </summary>
public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = new ();
    public int TotalCount
    {
        get; set;
    }
    public int PageNumber
    {
        get; set;
    }
    public int PageSize
    {
        get; set;
    }
    public int TotalPages => ( int ) Math.Ceiling (( double ) TotalCount / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}

/// <summary>
/// Exception summary for dashboard
/// </summary>
public class ExceptionSummary
{
    public int TotalExceptions
    {
        get; set;
    }
    public int UnresolvedCount
    {
        get; set;
    }
    public int TodayCount
    {
        get; set;
    }
    public Dictionary<string,int> ExceptionsByType { get; set; } = new ();
    public Dictionary<string,int> ExceptionsByStatus { get; set; } = new ();
    public List<ExceptionTrend> Trends { get; set; } = new ();
}

/// <summary>
/// Exception trend data for charts
/// </summary>
public class ExceptionTrend
{
    public DateTime Date
    {
        get; set;
    }
    public int Count
    {
        get; set;
    }
}
