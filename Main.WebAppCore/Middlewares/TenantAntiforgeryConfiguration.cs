using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

public class TenantAntiforgeryOptions: IConfigureNamedOptions<AntiforgeryOptions>
{
    private readonly ITenantSetter _tenantSetter;
    public TenantAntiforgeryOptions (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    public void Configure (AntiforgeryOptions options) => Configure (Options.DefaultName,options);

    public void Configure (string? name,AntiforgeryOptions options)
    {
        string? tenantId = _tenantSetter.CurrentTenantId;

        if ( tenantId != null )
        {
            // Dynamically set cookie name and header based on the current tenant
            options.HeaderName = "RequestVerificationToken";
            options.Cookie.Name = $".AspNetCore.Antiforgery.{tenantId}";
            // Optional: Match the form field if you use standard MVC forms
            options.FormFieldName = $"__RequestVerificationToken_{tenantId}";

            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.HttpOnly = true;
        }
    }
}


