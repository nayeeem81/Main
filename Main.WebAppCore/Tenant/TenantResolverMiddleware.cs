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
        string? resolvedId = null;

        // 1. Try to read the header
        bool headerValues = context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantIdHeader);

        if ( headerValues )
        {
            // Header exists. Extract the first value safely.
            resolvedId = tenantIdHeader.FirstOrDefault ();
        }

        string? tenantName = null;

        // check sessin data
        string? cachedTenantId = context.Session.GetString("CurrentTenantId");
        string? cachedCustomTenantId = context.Session.GetString($"XTenantID:{cachedTenantId}");

        // 1. try from cache or (session) if already resolved 
        // validaton in server session apped here (not cache)
        if ( !string.IsNullOrEmpty (cachedTenantId) &&
            !string.IsNullOrEmpty (cachedCustomTenantId) &&
            headerValues )
        {
            // Cache Hit: Skip Prseing 
            if ( cachedTenantId == cachedCustomTenantId )
            {
                if ( cachedTenantId == resolvedId )
                {
                    // set scoped service (di)
                    tenantSetter.CurrentTenantId = cachedTenantId!;

                    // set .Net IHttpAcessior (ITenantContext)
                    tenantContext!.TenantId = cachedTenantId!;

                    // reset tenantId
                    context.Request.Headers["X-Tenant-ID"] = cachedTenantId!;
                }
            }
        }
        else
        {
            // Cache/Session Miss: This is the "First Time Resolve"    
            // header parsing and validation (we set it here but do not parse from)
            // We parse Sub dictory, domain, sub domain (parsing) (In our case)

            // 1. sub directory tenants (used for parse 1)
            string path = context.Request.Path.Value ?? "";
            // 2. domain, sub domain tenants (used for parse 2, 3)
            string host = context.Request.Host.Host ?? string.Empty;

            // validate against database
            // tenants with sub directory type or te root website
            // Checking if te tenant is subdirectore based from database

            // Parse 1 (sub directoryor our own website when there is no tenant the website loads)
            if ( (
            !string.IsNullOrEmpty (host) &&
            host == "localhost" ) ||
            host == "tenators" )
            {
                // by path slt to fnd tenant name and later find fro database the tenantid
                var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

                // this is when tanent present
                if ( segments.Length > 0 )
                {
                    string tenantHost = segments[0];
                    // check in data base
                    await tenancyService.FindTenantAsync (tenantHost);

                    if ( tenancyService.TenancyFound )
                    {
                        // set header "X-Tenant-ID"
                        resolvedId = tenancyService.CurrentTenant!.TenantId;
                        // used by tenant setter scoped service
                        tenantName = tenancyService.CurrentTenant!.Name;
                        // .net context
                        tenantContext.TenantId = resolvedId;
                        // scoped service
                        tenantSetter.CurrentTenantId = resolvedId;
                        tenantSetter.TenantName = tenantName;
                        // 2. Save to Session so future calls bypass validation
                        context.Session.SetString ("CurrentTenantId",resolvedId);
                        // 3. Custom tracking session key if needed for audit/other systems
                        string customSessionKey = $"XTenantID:{resolvedId}";
                        context.Session.SetString (customSessionKey,resolvedId);
                        // create and set header
                        context.Request.Headers["X-Tenant-ID"] = resolvedId;
                    }
                    else
                    {
                        // when our root website, no tenant is acessing
                        // set header "X-Tenant-ID"
                        resolvedId = "root";
                        // scoped service
                        tenantSetter.CurrentTenantId = "root";
                        // used by tenant setter scoped service
                        tenantSetter.TenantName = host;
                        // 2. Save to Session so future calls bypass validation
                        context.Session.SetString ("CurrentTenantId",resolvedId);
                        // 3. Custom tracking session key if needed for audit/other systems
                        string customSessionKey = $"XTenantID:{resolvedId}";
                        context.Session.SetString (customSessionKey,resolvedId);
                        // create and set header
                        context.Request.Headers["X-Tenant-ID"] = resolvedId;
                    }
                }
            }
            else
            {
                // tenants with domain or sub domain tenants
                // parseing by host (domain or sub domain)
                if ( !string.IsNullOrWhiteSpace (host) )
                {
                    string[] segments = host.Split('.');
                    // www is a reserved keyword, we donot consider this as a sub domain
                    if ( segments.Length > 0 && segments[0] == "www" )
                    {
                        segments = segments.Skip (1).ToArray ();
                        _ = string.Join (".",segments);
                    }

                    // 2. check database for tenants who are using sub domain
                    if ( segments.Length > 2 )
                    {
                        string subdomain = segments[0];
                        // search in database
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
                            // create and set header
                            context.Request.Headers["X-Tenant-ID"] = resolvedId;
                        }
                    }
                    // 3. check database for tenants who are using domain
                    if ( segments.Length > 1 )
                    {
                        string domain = segments[0];
                        // search in database
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
                            // create and set header
                            context.Request.Headers["X-Tenant-ID"] = resolvedId;
                        }
                    }
                }
            }
        }

        if ( string.IsNullOrEmpty (resolvedId) )
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync ("Invald request!");
            return;
        }

        await _next (context);
    }
}