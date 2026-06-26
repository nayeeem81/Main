using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.Infrastructure;

public class ConfigureAntiforgeryOptions: IConfigureNamedOptions<AntiforgeryOptions>
{
    private readonly ITenantSetter _tenantSetter;

    public ConfigureAntiforgeryOptions (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    public void Configure (AntiforgeryOptions options) => Configure (Options.DefaultName,options);

    public void Configure (string? name,AntiforgeryOptions options)
    {
        string? tenantId = _tenantSetter.CurrentTenantId;
        if ( tenantId != null )
        {
            // Dynamically name the cookie based on the tenant identifier
            options.Cookie.Name = $"XSRF-TOKEN-{tenantId}";
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Path = $"/{tenantId}";
            options.Cookie.Name = $".Antiforgery.{tenantId}";
        }
    }
}
