using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Infrastructure.ICrosscuttingServices;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace Main.WebAppCore.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger < ExceptionLoggingService > _logger;

    public GlobalExceptionHandlingMiddleware (RequestDelegate next,ILogger<ExceptionLoggingService> logger)
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


    private static async Task HandleExceptionAsync (
    HttpContext context,
    Exception exception,
    IExceptionLoggingService exceptionLoggingService,
    ITenantSetter tenantSetter)
    {
        var (errorCode,statusCode,userMessage) = MapException (exception);
        var request = context.Request;

        // FIX 1: Use the standard security claim identity type mapped by .NET Identity
        var userId = context.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        // FIX 2: Unleash your GetClientIpAddress helper to read through Nginx proxy lines!
        var clientIpAddress = GetClientIpAddress(context);

        // Safely extract request stream data if buffered correctly upstream
        string? requestBody = null;
        if ( request.ContentLength.GetValueOrDefault () > 0 && request.Body.CanSeek )
        {
            try
            {
                request.Body.Position = 0; // Wind back to the beginning
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync ();
                request.Body.Position = 0; // Reset for security tracking down the chain
            }
            catch
            {
                requestBody = "[Unreadable Stream payload]";
            }
        }

        var requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        var requestHeaders = SerializeHeaders(request.Headers);

        try
        {
            await exceptionLoggingService.LogExceptionAsync (
                exception: exception,
                errorCode: errorCode,
                statusCode: statusCode,
                userMessage: userMessage,
                userId: userId,
                clientIpAddress: clientIpAddress, // Restored tracking metrics
                requestUrl: requestUrl,
                httpMethod: request.Method,
                requestHeaders: requestHeaders,
                requestBody: requestBody,
                customData: null,
                source: "MVC_WEB_APP");
        }
        catch ( Exception ex )
        {
            Log.Fatal (ex,"Failed to log exception to database. Original exception: {Message}",exception.Message);
        }


        // For standard browser page clicks, redirect to your Razor view error action page
        context.Response.Redirect ($"/Home/Error?errorCode={errorCode}&statusCode={statusCode}");

    }

    private static string GetClientIpAddress (HttpContext context)
    {
        // Reads the original user IP address forwarded by your Nginx proxy setup
        if ( context.Request.Headers.TryGetValue ("X-Forwarded-For",out var forwardedFor) )
        {
            var ips = forwardedFor.ToString().Split(',');
            return ips[0].Trim ();
        }

        return context.Connection.RemoteIpAddress?.ToString () ?? "Unknown";
    }



    private static (string ErrorCode,int StatusCode,string UserMessage) MapException (Exception exception)
    {
        return exception switch
        {
            // Validation exceptions
            ArgumentNullException or ArgumentException => (
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR,
                ExceptionErrorCodes.INVALID_ARGUMENT_ERROR_CODE,
                UserFriendlyMessages.BAD_REQUEST),

            // Not found exceptions
            KeyNotFoundException => (
                ExceptionErrorCodes.NOT_FOUND,
                ExceptionErrorCodes.NOT_FOUND_CODE,
                UserFriendlyMessages.NOT_FOUND),

            // Timeout and cancellation exceptions
            TimeoutException or OperationCanceledException => (
                ExceptionErrorCodes.TIMEOUT_ERROR,
                ExceptionErrorCodes.TIMEOUT_ERROR_CODE,
                UserFriendlyMessages.TIMEOUT_ERROR),

            // Network exceptions
            HttpRequestException => (
                ExceptionErrorCodes.NETWORK_ERROR,
                ExceptionErrorCodes.NETWORK_ERROR_CODE,
                UserFriendlyMessages.NETWORK_ERROR),

            // File and IO exceptions
            FileNotFoundException => (
                ExceptionErrorCodes.FILE_NOT_FOUND,
                ExceptionErrorCodes.FILE_NOT_FOUND_CODE,
                UserFriendlyMessages.DATABASE_ERROR),

            IOException => (
                ExceptionErrorCodes.IO_ERROR,
                ExceptionErrorCodes.IO_ERROR_CODE,
                UserFriendlyMessages.DATABASE_ERROR),

            // Database specific exceptions (Child must come before Parent)
            DbUpdateConcurrencyException => (
                ExceptionErrorCodes.CONFLICT,
                ExceptionErrorCodes.CONFLICT_CODE,
                UserFriendlyMessages.CONFLICT),

            DbUpdateException => (
                ExceptionErrorCodes.DATA_INTEGRITY_ERROR,
                ExceptionErrorCodes.DATA_INTEGRITY_ERROR_CODE,
                UserFriendlyMessages.DATABASE_ERROR),

            InvalidOperationException => (
                ExceptionErrorCodes.INVALID_OPERATION,
                ExceptionErrorCodes.INVALID_OPERATION_CODE,
                UserFriendlyMessages.INVALID_OPERATION),

            // Default fallback case
            _ => (
                ExceptionErrorCodes.UNKNOWN_ERROR,
                ExceptionErrorCodes.UNKNOWN_ERROR_CODE,
                UserFriendlyMessages.UNKNOWN_ERROR)
        };
    }

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