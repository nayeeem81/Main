using Main.Infrastructure;
using Main.Services;

namespace Main.WebAppCore.Tenant;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
    HttpContext context,
    ITenantContext tenantContext,
    ITenantSetter tenantSetter,
    ITenancyService tenancyService)
    {

        string host = context.Request.Host.Host ?? string.Empty;

        string? tenantId = tenantContext!.TenantId;

        // try from cache or session if user is logged in
        if ( !string.IsNullOrEmpty (tenantId) )
        {
            // get from cache
            // match with userid
            // match with tenantid (cache or database)
            // match with tenant name
        }


        // tenants with sub directory type or te root website
        if ( ( !string.IsNullOrEmpty (host) && host == "localhost" ) || host == "tenators" )
        {
            string path = context.Request.Path.Value ?? "";
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if ( segments.Length > 0 )
            {
                string tenantName = segments[0];
                await tenancyService.FindTenantAsync (tenantName);
                if ( tenancyService.TenancyFound )
                {
                    tenantContext.TenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.CurrentTenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.TenantName = tenancyService.CurrentTenant!.Name;
                    tenantSetter.TenantStore = tenancyService.CurrentTenant!.ShopType;
                }
            }

            tenantSetter.CurrentTenantId = "root";
            tenantSetter.TenantName = host;
        }


        // with domain or sub domain tenants
        if ( !string.IsNullOrWhiteSpace (host) )
        {
            string[] segments = host.Split('.');

            if ( segments.Length > 0 && segments[0] == "www" )
            {
                segments = segments.Skip (1).ToArray ();
                host = string.Join (".",segments);
            }

            // sub domain
            if ( segments.Length > 2 )
            {
                string subdomain = segments[0];
                await tenancyService.FindTenantAsync (subdomain);

                if ( tenancyService.TenancyFound )
                {
                    tenantContext.TenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.CurrentTenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.TenantName = tenancyService.CurrentTenant!.Name;
                    tenantSetter.TenantStore = tenancyService.CurrentTenant!.ShopType;
                }
            }

            // domain
            if ( segments.Length > 1 )
            {
                string domain = segments[0];
                await tenancyService.FindTenantAsync (domain);
                if ( tenancyService.TenancyFound )
                {
                    tenantContext.TenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.CurrentTenantId = tenancyService.CurrentTenant!.TenantId;
                    tenantSetter.TenantName = tenancyService.CurrentTenant!.Name;
                    tenantSetter.TenantStore = tenancyService.CurrentTenant!.ShopType;
                }
            }
        }

        if ( string.IsNullOrEmpty (tenantSetter.TenantName) )
        {
            throw new Exception ("Website is failing, please try later!");
        }

        await _next (context);
    }
}