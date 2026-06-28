using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Main.WebAppCore.Tenant;

public static class TenantResolutionExtensions
{
    private const string RootDomain = "https://yourwebsite.com";
    private const string SessionKey = "CurrentTenantId";

    public static async Task<bool> TryResolveTenantAsync (
        this HttpContext context,
        ITenantContext tenantContext,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IMemoryCache memoryCache) // Added Memory Cache
    {

        // 1. Fast Path: Check if tenant is already cached in this User's Session
        var cachedTenant = GetTenantFromSession(context);

        if ( cachedTenant != null )
        {
            SetTenantSetter (cachedTenant,tenantSetter);
            return true;
        }

        // 2. Resolve the incoming key string using your strategies
        string? tenantKey = context.ResolveFromHeader()
                            ?? context.ResolveFromQuery()
                            ?? context.ResolveFromPath()
                            ?? context.ResolveFromSubdomain()
                            ?? context.ResolveFromDomain();

        if ( !string.IsNullOrEmpty (tenantKey) )
        {
            // 3. Middle Path: Look in Application Memory Cache, fall back to DB if missing

            var tenant = await memoryCache.GetOrCreateAsync($"tenant_{tenantKey}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); 
                // Cache in app for 30 mins
                return await tenancyService.GetTenantByKeyAsync(tenantKey);
            });

            if ( tenant != null )
            {
                // 4. Save to Session for subsequent user requests and apply context

                SaveTenantToSession (context,tenant);
                SetTenantSetter (tenant,tenantSetter);
                return true;
            }
        }

        // 5. Fallback: Short-circuit and redirect to root domain
        context.Response.Redirect (RootDomain);
        return false;
    }

    // Helper Method for ITenantSetter Set 
    private static void SetTenantSetter (TenantModel cachedTenant,ITenantSetter tenantSetter)
    {
        tenantSetter.CurrentTenantId = cachedTenant.TenantId;
        tenantSetter.TenantName = cachedTenant.TenantName;
    }

    // Helper to extract Tenant from Session
    private static TenantModel? GetTenantFromSession (HttpContext context)
    {
        var sessionData = context.Session.GetString(SessionKey);
        return sessionData == null ? null : JsonSerializer.Deserialize<TenantModel> (sessionData);
    }

    // Helper to store Tenant in Session
    private static void SaveTenantToSession (HttpContext context,TenantModel tenant)
    {
        var sessionData = JsonSerializer.Serialize(tenant);
        context.Session.SetString (SessionKey,sessionData);
    }

    // Keep your strategy methods (ResolveFromHeader, ResolveFromQuery, etc.) down here...
    private static string? ResolveFromPath (this HttpContext context)
    {
        var pathSegments = context.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        // Assumes path structure like: /tenant-name/products
        return pathSegments?.Length > 0 ? pathSegments[0] : null;
    }
}