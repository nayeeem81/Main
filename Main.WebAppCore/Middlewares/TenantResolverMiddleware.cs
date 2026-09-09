using Main.Common.Models;
using Main.Infrastructure;
using Main.Services;
using Main.WebAppCore.DependentServices;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middlewares;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync
    (HttpContext context,
    ITenantSetter tenantSetter,
    ITenancyService tenancyService,
    IThemeService themeService,
    IMemoryCache memoryCache,
    ILogger<TenantResolverMiddleware> logger)
    {
        TenantDataModel? resolvedTenant
        = await TenantResolutionExtensions.TryResolveTenantAsync
        ( context, tenancyService, memoryCache );

        TenantThemeModel? tenantTheme =
            await memoryCache.GetOrCreateAsync($"tenanttheme_{resolvedTenant?.ResolvedTenantId}", async entry =>
            {
                _ =  entry.SetSize(1);
                _ =  entry.SetSlidingExpiration(new System.TimeSpan(600));
                _ =  entry.SetAbsoluteExpiration(new System.TimeSpan(600));

                return await themeService.GetThemeByTenantAsync( resolvedTenant?.ResolvedTenantId ?? Guid.Empty );
            });


        if ( resolvedTenant != null )
        {
            tenantSetter.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            tenantSetter.CurrentTenant = resolvedTenant;
            tenantSetter.CurrentTenant.ResolvedTenantId = resolvedTenant.ResolvedTenantId;
            tenantSetter.CurrentTenant.StoreType = resolvedTenant.StoreType;
            context.Items["TenantId"] = resolvedTenant.ResolvedTenantId;

            if ( tenantTheme == null )
            {
                tenantSetter.CurrentTenant.TenantThemeModel = new TenantThemeModel ()
                {
                    Default = true,
                    FontStack = tenantTheme?.FontStack ?? "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
                    LogoRelativeFilePath = tenantTheme?.LogoRelativeFilePath ?? ""
                };
            }
            else
            {
                tenantSetter.CurrentTenant.TenantThemeModel = tenantTheme;
            }
        }

        await _next (context);
    }
}

