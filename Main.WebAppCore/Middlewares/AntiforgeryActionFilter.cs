using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Main.WebAppCore.Middleware;

public class TenantAntiforgeryFilter: IAsyncActionFilter
{
    private readonly IAntiforgery _antiforgery;
    private const string TenantHeaderKey = "X-Tenant-ID";
    private const string BaseCookieName = ".AspNetCore.Antiforgery";

    public TenantAntiforgeryFilter (IAntiforgery antiforgery)
    {
        _antiforgery = antiforgery ?? throw new ArgumentNullException (nameof (antiforgery));
    }

    public async Task OnActionExecutionAsync (ActionExecutingContext context,ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        // 1. Resolve current tenant ID from request headers map
        if ( !httpContext.Request.Headers.TryGetValue (TenantHeaderKey,out var tenantId) || string.IsNullOrEmpty (tenantId) )
        {
            tenantId = "Default";
        }

        string tenantCookieName = $"{BaseCookieName}.{tenantId}";

        // 2. Handle safe HTTP GET requests (Generate & Drop Tenant Cookie)
        if ( HttpMethods.IsGet (httpContext.Request.Method) )
        {
            var tokens = _antiforgery.GetAndStoreTokens(httpContext);

            // Write tenant-suffixed cookie to response headers
            httpContext.Response.Cookies.Append (tenantCookieName,tokens.CookieToken!,new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/"
            });

            _ = await next ();
            return;
        }

        // 3. Backup the original cookie header state before modifying it
        string originalCookieHeader = httpContext.Request.Headers.Cookie.ToString();

        // 4. Modify the context layout strictly for validation processing
        if ( httpContext.Request.Cookies.TryGetValue (tenantCookieName,out var tokenValue) )
        {
            // Force overwrite the cookie header to match standard layout expected by EF Core/ASP.NET Core
            httpContext.Request.Headers.Cookie = $"{BaseCookieName}={tokenValue}";
        }

        try
        {
            // 5. Execute validation engine using the temporary morphed cookie layout
            await _antiforgery.ValidateRequestAsync (httpContext);

            Log.Information ("Antiforgery token successfully verified for Tenant: {TenantId}",tenantId);
        }
        catch ( AntiforgeryValidationException ex )
        {
            // Log security breach using global Serilog static class instance
            Log.Warning (ex,"Antiforgery security validation token failed for Tenant: {TenantId}",tenantId);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsJsonAsync (new
            {
                Error = "Security validation failed. Missing or invalid token layout."
            });

            return; // Short-circuit execution path
        }
        finally
        {
            // 6. RESTORE PREVIOUS COOKIE ENVIRONMENT 
            // This block always runs whether validation succeeds or fails!
            if ( !string.IsNullOrEmpty (originalCookieHeader) )
            {
                httpContext.Request.Headers.Cookie = originalCookieHeader;
            }
            else
            {
                _ = httpContext.Request.Headers.Remove ("Cookie");
            }
        }

        // 7. Proceed into target controller action seamlessly with original context intact
        _ = await next ();
    }
}
