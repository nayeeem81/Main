using Main.Common;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using ILogger = Serilog.ILogger;

namespace Main.WebAppCore.Middleware;

/// <summary>
/// Global exception handling middleware
/// Catches all unhandled exceptions, logs them, and returns user-friendly responses
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalExceptionHandlingMiddleware (RequestDelegate next,ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync (
        HttpContext context,
        IExceptionLoggingService exceptionLoggingService,
        ITenantSetter tenantSetter)
    {
        try
        {
            await _next (context);
        }
        catch ( Exception exception )
        {
            await HandleExceptionAsync (context,exception,exceptionLoggingService,tenantSetter);
        }
    }

    /// <summary>
    /// Handles the exception by logging it and sending appropriate response
    /// </summary>
    private static async Task HandleExceptionAsync (
        HttpContext context,
        Exception exception,
        IExceptionLoggingService exceptionLoggingService,
        ITenantSetter tenantSetter)
    {
        // Map exception to error code and status code
        var (errorCode,statusCode,userMessage) = MapException (exception);

        // Get request information for logging
        var request = context.Request;
        var userId = context.User?.FindFirst("sub")?.Value ??
                    context.User?.FindFirst("nameid")?.Value;
        var clientIpAddress = GetClientIpAddress(context);

        // Get request body if available
        string? requestBody = null;
        if ( request.ContentLength.GetValueOrDefault () > 0 )
        {
            request.EnableBuffering ();
            using var reader = new StreamReader (request.Body,leaveOpen: true);
            requestBody = await reader.ReadToEndAsync ();
            request.Body.Position = 0; // Reset for next middleware
        }

        var requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        var requestHeaders = SerializeHeaders(request.Headers);

        try
        {
            // Log exception to database and file
            await exceptionLoggingService.LogExceptionAsync (
                exception: exception,
                errorCode: errorCode,
                statusCode: statusCode,
                userMessage: userMessage,
                userId: userId,
                clientIpAddress: clientIpAddress,
                requestUrl: requestUrl,
                httpMethod: request.Method,
                requestHeaders: requestHeaders,
                requestBody: requestBody,
                customData: null,
                source: "API");
        }
        catch ( Exception ex )
        {
            // Log to Serilog if database logging fails
            Log.Fatal (ex,"Failed to log exception to database. Original exception: {Message}",exception.Message);
        }

        // Send error response to client
        await SendErrorResponseAsync (context,errorCode,statusCode,userMessage);
    }

    /// <summary>
    /// Maps exception types to error codes and status codes
    /// </summary>
    private static (string ErrorCode,int StatusCode,string UserMessage) MapException (Exception exception)
    {
        return exception switch
        {
            // Validation exceptions
            ArgumentNullException => (
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR,
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR_CODE,
                UserFriendlyMessages.BAD_REQUEST),

            ArgumentException => (
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR,
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR_CODE,
                UserFriendlyMessages.BAD_REQUEST),

            // Not found exceptions
            KeyNotFoundException => (
                ExceptionErrorCodes.NOT_FOUND,
                ExceptionErrorCodes.NOT_FOUND_CODE,
                UserFriendlyMessages.NOT_FOUND),

            // Timeout exceptions
            TimeoutException => (
                ExceptionErrorCodes.TIMEOUT_ERROR,
                ExceptionErrorCodes.TIMEOUT_ERROR_CODE,
                UserFriendlyMessages.TIMEOUT_ERROR),

            // Network/IO exceptions
            HttpRequestException => (
                ExceptionErrorCodes.NETWORK_ERROR,
                ExceptionErrorCodes.NETWORK_ERROR_CODE,
                UserFriendlyMessages.NETWORK_ERROR),

            FileNotFoundException => (
                ExceptionErrorCodes.FILE_NOT_FOUND,
                ExceptionErrorCodes.FILE_NOT_FOUND_CODE,
                UserFriendlyMessages.DATABASE_ERROR),

            IOException => (
                ExceptionErrorCodes.IO_ERROR,
                ExceptionErrorCodes.IO_ERROR_CODE,
                UserFriendlyMessages.DATABASE_ERROR),



            // Database exceptions
            DbUpdateConcurrencyException => (
                ExceptionErrorCodes.CONFLICT,
                ExceptionErrorCodes.CONFLICT_CODE,
                UserFriendlyMessages.CONFLICT),

            DbUpdateException => (
                ExceptionErrorCodes.DATA_INTEGRITY_ERROR,
                ExceptionErrorCodes.DATA_INTEGRITY_ERROR_CODE,
                UserFriendlyMessages.DATABASE_ERROR),

            OperationCanceledException => (
                ExceptionErrorCodes.TIMEOUT_ERROR,
                ExceptionErrorCodes.TIMEOUT_ERROR_CODE,
                UserFriendlyMessages.TIMEOUT_ERROR),

            InvalidOperationException => (
                ExceptionErrorCodes.INVALID_OPERATION,
                ExceptionErrorCodes.INVALID_OPERATION_CODE,
                UserFriendlyMessages.INVALID_OPERATION),

            // Default case
            _ => (
                ExceptionErrorCodes.UNKNOWN_ERROR,
                ExceptionErrorCodes.UNKNOWN_ERROR_CODE,
                UserFriendlyMessages.UNKNOWN_ERROR)
        };
    }

    /// <summary>
    /// Sends error response to client
    /// </summary>
    private static async Task SendErrorResponseAsync (
        HttpContext context,
        string errorCode,
        int statusCode,
        string userMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            ErrorCode = errorCode,
            Message = userMessage,
            StatusCode = statusCode,
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path.ToString(),
            TraceId = context.TraceIdentifier
        };

        // Add detailed exception info in development only
        if ( context.Request.Host.Host == "localhost" ||
            Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") == "Development" )
        {
            // In development, you might want to include more details
            // response.Details = exception.Message;
        }

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsJsonAsync (response,options);
    }

    /// <summary>
    /// Gets client IP address from request
    /// </summary>
    private static string GetClientIpAddress (HttpContext context)
    {
        // Try to get IP from X-Forwarded-For header (for proxied requests)
        if ( context.Request.Headers.TryGetValue ("X-Forwarded-For",out var forwardedFor) )
        {
            var ips = forwardedFor.ToString().Split(',');
            return ips[0].Trim ();
        }

        // Fall back to RemoteIpAddress
        return context.Connection.RemoteIpAddress?.ToString () ?? "Unknown";
    }

    /// <summary>
    /// Serializes request headers for logging (excluding sensitive headers)
    /// </summary>
    private static string SerializeHeaders (IHeaderDictionary headers)
    {
        var sensitiveHeaders = new[]
        {
            "Authorization",
            "Cookie",
            "X-Api-Key",
            "X-Access-Token",
            "X-Secret-Token",
            "Password"
        };

        var filteredHeaders = headers
            .Where(h => !sensitiveHeaders.Contains(h.Key, StringComparer.OrdinalIgnoreCase))
            .ToDictionary(h => h.Key, h => h.Value.ToString());

        return JsonSerializer.Serialize (filteredHeaders);
    }
}

/// <summary>
/// Extension method to register the global exception handling middleware
/// </summary>
public static class GlobalExceptionHandlingMiddlewareExtensions
{
    /// <summary>
    /// Adds global exception handling middleware to the pipeline
    /// Should be called early in the middleware pipeline, after logging but before other middleware
    /// </summary>
    public static IApplicationBuilder UseGlobalExceptionHandling (this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlingMiddleware> ();
    }
}
