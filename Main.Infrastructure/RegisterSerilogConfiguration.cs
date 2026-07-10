using Domain.Model;
using Main.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Main.Infrastructure;

public static class RegisterSerilogConfiguration
{
    public static WebApplicationBuilder AddSerilogConfiguration (this WebApplicationBuilder builder)
    {
        var loggingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        // Ensure logs directory exists
        if ( !Directory.Exists (loggingPath) )
        {
            _ = Directory.CreateDirectory (loggingPath);
        }

        Log.Logger = new LoggerConfiguration ()
            // Log level configuration
            .MinimumLevel.Information ()
            .MinimumLevel.Override ("Microsoft",LogEventLevel.Warning)
            .MinimumLevel.Override ("Microsoft.EntityFrameworkCore",LogEventLevel.Warning)
            .MinimumLevel.Override ("System",LogEventLevel.Warning)

            // Enrich logs with additional context
            .Enrich.FromLogContext ()
            .Enrich.WithProperty ("Application","Main.WebAppCore")
            .Enrich.WithProperty ("Environment",builder.Environment.EnvironmentName)
            .Enrich.WithThreadId ()
            .Enrich.WithMachineName ()
            .Enrich.When (le => le.Level >= LogEventLevel.Error,e => e.WithProperty ("IsError",true))

            // Write to console
            .WriteTo.Console (
                theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

            // Write all logs to file
            .WriteTo.File (
                path: Path.Combine (loggingPath,"application-log-.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj} {Properties:j}{NewLine}{Exception}")

            // Write errors to separate file
            .WriteTo.File (
                path: Path.Combine (loggingPath,"errors-log-.txt"),
                restrictedToMinimumLevel: LogEventLevel.Error,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj} {Properties:j}{NewLine}{Exception}")

            // Write exceptions to separate file with compact JSON format
            .WriteTo.File (
                new CompactJsonFormatter (),
                path: Path.Combine (loggingPath,"exceptions-log-.json"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30)

            // Create Logger
            .CreateLogger ();

        // Replace logging providers with Serilog
        _ = builder.Logging.ClearProviders ();
        _ = builder.Logging.AddSerilog (Log.Logger);

        return builder;
    }

    public static IServiceCollection AddExceptionLogging (this IServiceCollection services)
    {
        // Exception logging service will be registered separately
        _ = services.AddScoped<IExceptionLoggingService,ExceptionLoggingService> ();
        return services;
    }
}


public interface IExceptionLoggingService
{
    /// <summary>
    /// Logs an exception to persistent storage
    /// </summary>
    Task LogExceptionAsync (
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
        string source = "API");


    Task<List<ExceptionLog>> GetExceptionsAsync (
        int? statusCode = null,
        string? errorCode = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        bool? isResolved = null,
        int pageNumber = 1,
        int pageSize = 20);


    Task<(int Total,int Unresolved,int Today)> GetExceptionSummaryAsync ();


    Task MarkAsResolvedAsync (long exceptionId,string? notes = null);


    Task<ExceptionLog?> GetExceptionByIdAsync (long id);
}
