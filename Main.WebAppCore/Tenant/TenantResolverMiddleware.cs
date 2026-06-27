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
        string? cachedTenantId = context.Session.GetString("CurrentTenantId");

        // try from cache or session. If already resolved or not
        if ( !string.IsNullOrEmpty (cachedTenantId) )
        {
            // Cache Hit: Skip header parsing and validation
            // alidaton inserver session apped here
            tenantSetter.CurrentTenantId = cachedTenantId;
            // .Net IHttpAcessior (ITenantContext)
            tenantContext!.TenantId = cachedTenantId;
        }
        else  // first time resolve
        {
            string host = context.Request.Host.Host ?? string.Empty;

            // Cache Miss: This is the "First Time Resolve"
            if ( context.Request.Headers.TryGetValue ("X-Tenant-ID",out var tenantIdHeader) )
            {
                _ = tenantIdHeader.ToString ();
                string tenantName;


                string resolvedId;
                // validate gainst database                                 
                // tenants with sub directory type or te root website
                // Checking if te tenant is subdirectore based from database
                if ( (
                !string.IsNullOrEmpty (host) &&
                host == "localhost" ) ||
                host == "tenators" )
                {
                    string path = context.Request.Path.Value ?? "";
                    var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

                    if ( segments.Length > 0 )
                    {
                        string tenantHost = segments[0];
                        await tenancyService.FindTenantAsync (tenantHost);

                        if ( tenancyService.TenancyFound )
                        {
                            // set header "X-Tenant-ID"
                            resolvedId = tenancyService.CurrentTenant!.TenantId;

                            // used by tenant setter service
                            tenantName = tenancyService.CurrentTenant!.Name;

                            // .net context
                            tenantContext.TenantId = resolvedId;
                            // scoped service
                            tenantSetter.CurrentTenantId = resolvedId;
                            tenantSetter.TenantName = tenantName;

                            // 2. Save to Session so future calls bypass validation
                            context.Session.SetString ("CurrentTenantId",resolvedId);
                            // 3. Create your custom tracking session key if needed for audit/other systems
                            string customSessionKey = $"XTenantID:{resolvedId}";
                            context.Session.SetString (customSessionKey,resolvedId);

                        }
                        else
                        {
                            resolvedId = "root";
                            tenantSetter.CurrentTenantId = "root";
                            tenantSetter.TenantName = host;

                            // 2. Save to Session so future calls bypass validation
                            context.Session.SetString ("CurrentTenantId",resolvedId);
                            // 3. Create your custom tracking session key if needed for audit/other systems
                            string customSessionKey = $"XTenantID:{resolvedId}";
                            context.Session.SetString (customSessionKey,resolvedId);
                        }
                    }
                }

                // with domain or sub domain tenants
                if ( !string.IsNullOrWhiteSpace (host) )
                {
                    string[] segments = host.Split('.');

                    if ( segments.Length > 0 && segments[0] == "www" )
                    {
                        segments = segments.Skip (1).ToArray ();
                        _ = string.Join (".",segments);
                    }

                    // sub domain
                    if ( segments.Length > 2 )
                    {
                        string subdomain = segments[0];
                        await tenancyService.FindTenantAsync (subdomain);

                        if ( tenancyService.TenancyFound )
                        {
                            // set header "X-Tenant-ID"
                            resolvedId = tenancyService.CurrentTenant!.TenantId;

                            // used by tenant setter service
                            tenantName = tenancyService.CurrentTenant!.Name;

                            // .net context
                            tenantContext.TenantId = resolvedId;
                            // scoped service
                            tenantSetter.CurrentTenantId = resolvedId;
                            tenantSetter.TenantName = tenantName;

                            // 2. Save to Session so future calls bypass validation
                            context.Session.SetString ("CurrentTenantId",resolvedId);
                            // 3. Create your custom tracking session key if needed for audit/other systems
                            string customSessionKey = $"XTenantID:{resolvedId}";
                            context.Session.SetString (customSessionKey,resolvedId);

                        }
                    }

                    // domain
                    if ( segments.Length > 1 )
                    {
                        string domain = segments[0];
                        await tenancyService.FindTenantAsync (domain);
                        if ( tenancyService.TenancyFound )
                        {
                            // set header "X-Tenant-ID"
                            resolvedId = tenancyService.CurrentTenant!.TenantId;

                            // used by tenant setter service
                            tenantName = tenancyService.CurrentTenant!.Name;

                            // .net context
                            tenantContext.TenantId = resolvedId;
                            // scoped service
                            tenantSetter.CurrentTenantId = resolvedId;
                            tenantSetter.TenantName = tenantName;

                            // 2. Save to Session so future calls bypass validation
                            context.Session.SetString ("CurrentTenantId",resolvedId);
                            // 3. Create your custom tracking session key if needed for audit/other systems
                            string customSessionKey = $"XTenantID:{resolvedId}";
                            context.Session.SetString (customSessionKey,resolvedId);

                        }
                    }
                }
            }
        }

        if ( string.IsNullOrEmpty (tenantSetter.TenantName) )
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync ("Invald request!");
            return;
        }

        await _next (context);
    }
}