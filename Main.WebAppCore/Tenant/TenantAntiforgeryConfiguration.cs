using Main.Infrastructure;
using Main.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Tenant;

public class TenantAntiforgeryConfiguration: IConfigureNamedOptions<AntiforgeryOptions>
{
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantContext _tenantContext;

    public TenantAntiforgeryConfiguration (ITenantSetter tenantSetter,ITenantContext tenantContext)
    {
        _tenantSetter = tenantSetter;
        _tenantContext = tenantContext;
    }

    public void Configure (AntiforgeryOptions options) => Configure (Options.DefaultName,options);

    public void Configure (string? name,AntiforgeryOptions options)
    {
        string? tenantId = _tenantSetter.CurrentTenantId;
        string? tenantUserId = _tenantContext.IdentityId;
        if ( tenantId != null )
        {
            // Dynamically name the cookie based on the tenant identifier
            options.Cookie.Name = $"XSRF-TOKEN-{tenantId}-{tenantUserId}";
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Path = $"/{tenantId}/{tenantUserId}/";
            options.Cookie.Name = $".Antiforgery.{tenantId}.{tenantUserId}";
        }
    }
}
