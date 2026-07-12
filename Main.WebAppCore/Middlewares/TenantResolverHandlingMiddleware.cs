using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.Middlewares;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public class TenantResolverHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private const string rootDomain = "localhost";

    public TenantResolverHandlingMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
    HttpContext context,
    ITenantContext tenantContext,
    ITenantSetter tenantSetter,
    ITenancyService tenancyService,
    IMemoryCache memoryCache,
    ITokenService tokenService)
    {
        bool result = await TenantResolutionExtensions.TryResolveTenantAsync(context,tenantContext,tenantSetter,tenancyService,memoryCache,rootDomain);


        if ( result && TenantSafetyCheckExtensions.CheckContamination
            (tenantSetter.CurrentTenantId,context) )
        {
            context.Response.Redirect (rootDomain);
        }

        await _next (context);
    }
}

