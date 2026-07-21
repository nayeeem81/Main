
using Main.Infrastructure;

namespace Main.WebAppCore.Middleware;

public class TenantSecurityMiddleware
{
    private readonly RequestDelegate _next;

    public TenantSecurityMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (HttpContext context,ITenantSetter tenantSetter)
    {
        // Only run this check if the user is successfully authenticated
        if ( context.User.Identity?.IsAuthenticated == true )
        {
            // 1. Get the TenantId embedded securely inside the user's login session
            var userTenantId = context.User.FindFirst("TenantId")?.Value;

            // 2. Get the TenantId that matches the current browser URL
            var resolvedTenantId = tenantSetter.CurrentTenantId;

            // 3. ENFORCE ISOLATION: Reject if they don't match
            if ( string.Equals (userTenantId,resolvedTenantId.ToString (),StringComparison.OrdinalIgnoreCase) )
            {
                // Sign out or short-circuit with a 403 Forbidden page
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync ("Access Denied: You do not belong to this tenant space.");



                return; // Stop the request pipeline immediately
            }
        }

        /// ArgumentNullException.ThrowIfNull (tenantSetter);

        await _next (context);
    }
}
