using Main.Infrastructure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Main.WebAppCore.Middleware;

public class AntiforgeryActionFilter: IAsyncAuthorizationFilter
{
    private readonly IAntiforgery _antiforgery;
    private readonly ITenantSetter _tenantSetter;
    private readonly ActionExecutionDelegate _next;

    public AntiforgeryActionFilter (IAntiforgery antiforgery,ITenantSetter tenantSetter)
    {
        _antiforgery = antiforgery;
        _tenantSetter = tenantSetter;
    }

    public async Task OnAuthorizationAsync (AuthorizationFilterContext context)
    {
        var method = context.HttpContext.Request.Method;
        if ( HttpMethods.IsGet (method) || HttpMethods.IsHead (method) || HttpMethods.IsOptions (method) )
        {
            _ = await _next ();
            return;
        }

        // Dynamically alter cookie names according to tenant context
        var options = context.HttpContext
            .RequestServices
            .GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

        options.Cookie.Name = $".AspNetCore.Antiforgery.{_tenantSetter.CurrentTenantId}";

        if ( HttpMethods.IsPost (method) ||
            HttpMethods.IsPut (method) ||
            HttpMethods.IsDelete (method) )
        {
            try
            {
                await _antiforgery.ValidateRequestAsync (context.HttpContext);
                _ = await _next ();
            }
            catch ( AntiforgeryValidationException )
            {
                context.Result = new BadRequestObjectResult ("Antiforgery token verification failed for tenant environment.");
            }
        }

    }
}
