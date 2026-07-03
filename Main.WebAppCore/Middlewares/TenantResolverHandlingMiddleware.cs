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
    IMemoryCache memoryCache)
    {
        bool result = await TenantResolutionExtensions.TryResolveTenantAsync(context,tenantContext,tenantSetter,tenancyService,memoryCache);

        await _next (context);
    }
}