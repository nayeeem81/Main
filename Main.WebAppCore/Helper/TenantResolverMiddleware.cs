using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace WebAppCore.Helper;

public class TenantService: ITenantSetter
{
    public string CurrentTenantId
    {
        get; set;
    }
}

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware ( RequestDelegate next )
    {
        _next = next;
    }

    public async Task InvokeAsync ( HttpContext context,ITenantSetter tenantSetter,
        ApplicationDbContext dbContext )
    {
        string host = context.Request.Host.Host ?? string.Empty;

        // Localhost for fine arts (development)
        string tenantId = "e02fd0e1-00fd-009a-ca30-0d00a2345ba0";

        if ( !string.IsNullOrWhiteSpace ( host ) )
        {
            string[] segments = host.Split('.');

            if ( segments.Length > 0 && segments[0] == "www" )
            {
                segments = segments.Skip ( 1 ).ToArray ( );
                host = string.Join ( ".",segments );
            }
            else if ( segments.Length > 2 )
            {
                string subdomain = segments[0];

                var tenant = await dbContext.Tenants
                                    .IgnoreQueryFilters()
                                    .FirstOrDefaultAsync(t => t.Domain == subdomain);

            }
            else if ( segments.Length > 1 )
            {
                string subdomain = segments[0];
                var tenant = await dbContext.Tenants
                                    .IgnoreQueryFilters()
                                    .FirstOrDefaultAsync(t => t.Domain == subdomain);
            }

            else if ( segments.Length == 1 )
            {
                var tenant = await dbContext.Tenants
                                    .IgnoreQueryFilters()
                                    .FirstOrDefaultAsync(t => t.Domain == host);
            }
            else
            {
                tenantSetter.CurrentTenantId = tenantId;
            }
        }

        await _next ( context );
    }
}