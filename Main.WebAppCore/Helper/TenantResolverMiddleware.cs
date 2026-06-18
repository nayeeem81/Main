using Main.Infrastructure;
namespace WebAppCore.Helper;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware ( RequestDelegate next )
    {
        _next = next;
    }

    public async Task InvokeAsync ( HttpContext context,ITenantSetter tenantSetter )
    {

        var host = context.Request.Host.Value;

        var segments = host.Split('.');

        string tenantId = "default";

        if ( segments.Length > 2 )
        {
            tenantId = segments[0].ToLower ( );
        }


        if ( context.Request.Headers.TryGetValue ( "X-Tenant",out var headerTenant ) )
        {
            tenantId = headerTenant.ToString ( );
        }

        tenantSetter.CurrentTenantId = tenantId;

        await _next ( context );
    }
}