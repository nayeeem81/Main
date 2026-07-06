using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

public class MultiTenantAntiforgeryCookieFilter: IAsyncAuthorizationFilter
{
    private readonly IAntiforgery _antiforgery;
    private readonly ITenantSetter _tenantSetter;

    public MultiTenantAntiforgeryCookieFilter (IAntiforgery antiforgery,ITenantSetter tenantSetter)
    {
        _antiforgery = antiforgery;
        _tenantSetter = tenantSetter;
    }

    public async Task OnAuthorizationAsync (AuthorizationFilterContext context)
    {
        // Dynamically alter cookie names according to tenant context

        var options = context.HttpContext
            .RequestServices
            .GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

        options.Cookie.Name = $".AspNetCore.Antiforgery.{_tenantSetter.CurrentTenantId}";

        if ( HttpMethods.IsPost (context.HttpContext.Request.Method) )
        {
            try
            {
                await _antiforgery.ValidateRequestAsync (context.HttpContext);
            }
            catch ( AntiforgeryValidationException )
            {
                context.Result = new BadRequestObjectResult ("Antiforgery token verification failed for tenant environment.");
            }
        }
    }

}
