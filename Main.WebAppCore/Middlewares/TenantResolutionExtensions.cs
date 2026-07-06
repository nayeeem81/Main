using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Main.WebAppCore.Middleware;

public static class TenantResolutionExtensions
{
    private const string SessionKey = "CurrentTenantId";
    private const string RootDomain = "localhost";
    public static async Task<bool> TryResolveTenantAsync (
        this HttpContext context,
        ITenantContext tenantContext,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IMemoryCache memoryCache)
    {
        var cachedTenant = GetTenantFromSession(context);

        if ( cachedTenant != null )
        {
            SetTenantSetter (cachedTenant,tenantSetter);
            return true;
        }

        string? tenantHost = context.ResolveFromPath()
                            ?? context.ResolveFromSubdomain()
                            ?? context.ResolveFromDomain();


        if ( !string.IsNullOrEmpty (tenantHost) )
        {
            TenantDisplayDataModel? tenantDisplayDataModel = await memoryCache.GetOrCreateAsync($"tenant_{tenantHost}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await tenancyService.FindTenantAsync(tenantHost);
            });

            if ( tenantDisplayDataModel != null )
            {
                SaveTenantToSession (context,tenantDisplayDataModel);
                SetTenantSetter (tenantSetter,tenantDisplayDataModel);

                return true;
            }
        }

        context.Response.Redirect (RootDomain);

        return false;
    }

    private static void SetTenantSetter (ITenantSetter tenantSetter,TenantDisplayDataModel tenantDisplayDataModel)
    {
        tenantSetter.CurrentTenantId = tenantDisplayDataModel.TenantId;
        tenantSetter.TenantStore = tenantDisplayDataModel.StoreType;
        tenantSetter.TenantName = tenantDisplayDataModel.Name;
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
    private static void SaveTenantToSession (HttpContext context,TenantDisplayDataModel tenantDisplayDataModel)
    {

        var sessionData = JsonSerializer.Serialize(tenantDisplayDataModel);
        context.Session.SetString (SessionKey,sessionData);
    }

    // Keep your strategy methods
    // (ResolveFromPath, ResolveFromSubdomain, ResolveFromDomain)
    private static string? ResolveFromPath (this HttpContext context)
    {
        string pathRequest = context.Request.Path.Value ?? "";
        var pathSegments = pathRequest?.Split('/', StringSplitOptions.RemoveEmptyEntries);

        return pathSegments?.Length > 0 ? pathSegments[0] : null;
    }

    private static string ResolveFromSubdomain (this HttpContext context)
    {
        string host = context.Request.Host.Host ?? "";
        string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

        segments = RemoveResevedWord (segments.Length > 0 ? segments : null);

        if ( segments!.Length > 2 )
        {
            return segments[0];
        }

        return "";
    }

    private static string? ResolveFromDomain (this HttpContext context)
    {
        string host = context.Request.Host.Host ?? "";
        string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

        segments = RemoveResevedWord (segments!.Length > 0 ? segments! : null);

        if ( segments!.Length > 1 )
        {
            return segments[0];
        }

        return "";
    }

    private static string[]? RemoveResevedWord (string[]? segments)
    {
        if ( segments!.Length > 0 && segments[0] == "www" )
        {
            segments = segments.Skip (1).ToArray ();
        }

        return segments;
    }

    public static void TenantResolveMiddlewareTokenMatching (string resolvedTenantId,HttpContext context,ITokenService tokenService)
    {
        // 2. Extract Access Token
        string? authHeader = context.Request
                                    .Headers["Authorization"]
                                    .FirstOrDefault();

        if ( authHeader != null && authHeader.StartsWith ("Bearer",StringComparison.OrdinalIgnoreCase) )
        {
            // token from header
            var token = authHeader.Substring("Bearer ".Length).Trim();

            // 3. Centralized Decryption & Validation
            var principal = tokenService.ValidateAndDecryptToken
                (token, out _);

            if ( principal != null )
            {
                var tokenTenant = principal.FindFirst("tenant_id")?.Value;

                // 4. Multi-Tenant Cross-Contamination Check
                if ( tokenTenant != null && tokenTenant.Equals (resolvedTenantId,StringComparison.OrdinalIgnoreCase) )
                {
                    // Token matches requested tenant context
                    context.User = principal;
                }
            }
        }
    }
}