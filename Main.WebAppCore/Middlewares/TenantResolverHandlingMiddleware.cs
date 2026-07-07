using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public class TenantResolverHandlingMiddleware
{
    private readonly RequestDelegate _next;

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
        bool result = await TenantResolutionExtensions.TryResolveTenantAsync(context,tenantContext,tenantSetter,tenancyService,memoryCache);

        if ( result )
        {
            string resolvedTenantId = tenantSetter.CurrentTenantId;

            TenantResolutionExtensions.TenantResolveMiddlewareTokenMatching
            (resolvedTenantId,context,tokenService);
        }

        await _next (context);
    }

}