using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

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

        if ( tenantId != null )
        {
            // Dynamically set cookie name, path, and header based on the current tenant
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Name = $".AspNetCore.Antiforgery.{tenantId}";


            // Optional: Match the form field if you use standard MVC forms
            options.FormFieldName = $"__RequestVerificationToken_{tenantId}";
        }
    }
}
