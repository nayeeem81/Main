using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace Main.WebAppCore.Middlewares;

public class TenantLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public TenantLoggingMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context)
    {
        // 1. Read the pre-resolved TenantId from HttpContext.Items
        // (Ensure the key string matches exactly what TenantResolverMiddleware uses)
        string? tenantId = context.Items["TenantId"]?.ToString();

        if ( string.IsNullOrEmpty (tenantId) )
        {
            tenantId = "Unknown-Tenant";
        }

        var stopwatch = Stopwatch.StartNew();

        // 2. Push to Serilog Context so it routes to the correct tenant folder
        using ( LogContext.PushProperty ("TenantId",tenantId) )
        {
            try
            {
                await _next (context); // Continues down the pipeline to your controllers
            }
            finally
            {
                stopwatch.Stop ();
                var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
                var statusCode = context.Response.StatusCode;

                Log.ForContext<TenantLoggingMiddleware> ()
                   .Information (
                       "Tenant: {TenantId} | {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms",
                       tenantId,
                       context.Request.Method,
                       context.Request.Path,
                       statusCode,
                       elapsedMs
                   );
            }
        }
    }

}
