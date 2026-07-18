using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

public class ConfigureAntiforgeryCookieOptions: IConfigureOptions<AntiforgeryOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    //  Inject the Singleton HTTP context accessor, NOT your scoped ITenantContext directly
    public ConfigureAntiforgeryCookieOptions (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Configure (AntiforgeryOptions options)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if ( httpContext == null )
        {
            return;
        }

        //  Safely fetch your Scoped tenant service dynamically from the request scope
        var tenantContext = httpContext.RequestServices.GetRequiredService<ITenantContext>();

        // Apply your dynamic tenant logic here safely
        options.Cookie.Name = $".AspNetCore.Antiforgery{tenantContext.TenantId}";
    }
}
