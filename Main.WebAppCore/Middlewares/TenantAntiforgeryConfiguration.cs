using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

public class TenantAntiforgeryOptions: IConfigureNamedOptions<AntiforgeryOptions>
{
    private readonly ITenantContext _tenantContext;
    public TenantAntiforgeryOptions (ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public void Configure (AntiforgeryOptions options) => Configure (Options.DefaultName,options);

    public void Configure (string? name,AntiforgeryOptions options)
    {
        string? tenantId = _tenantContext.ResolvedTenantId;

        if ( string.IsNullOrEmpty (tenantId) )
        {
            // Dynamically set cookie name based on the current tenant
            options.HeaderName = "X-XSRF-TOKEN";

            options.Cookie.Name = $".AspNetCore.Antiforgery.{tenantId}";

            // Optional: Match the form field if you use standard MVC forms
            options.FormFieldName = $"__RequestVerificationToken_{tenantId}";

            options.Cookie.SameSite = SameSiteMode.Strict;

            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            options.Cookie.HttpOnly = true;
        }
    }
}


