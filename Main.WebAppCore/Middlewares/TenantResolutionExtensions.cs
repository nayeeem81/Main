using DataTransferModel;
using Main.Infrastructure;
using Main.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Main.WebAppCore.Middleware;

public static class TenantResolutionExtensions
{
    private const string TenantHeaderKey = "X-Tenant-ID";

    public static async Task<bool> TryResolveTenantAsync (
        this HttpContext context,
        ITenantContext tenantContext,
        ITenantSetter tenantSetter,
        ITenancyService tenancyService,
        IMemoryCache memoryCache,
        string rootDomain)
    {
        // 1. Get the host string from Nginx header, fallback to local host if empty
        string? rawHost = context.Request.Headers["X-Forwarded-Host"].FirstOrDefault();
        // 1. Check for the Nginx subdirectory header
        string? tenantPath = context.Request.Headers["X-Forwarded-Prefix"].FirstOrDefault();

        if ( string.IsNullOrEmpty (rawHost) )
        {
            rawHost = context.Request.Host.Value; // Fallback for local debugging without Nginx
        }

        string? tenantHost = context.ResolveFromSubdomain(rawHost)
                            ?? context.ResolveFromDomain(rawHost)
                            ?? ReutePathExtensions.ResolveFromPath(context, tenantPath);

        if ( !string.IsNullOrEmpty (tenantHost) )
        {
            TenantDisplayDataModel? tenantDisplayDataModel =
            await memoryCache.GetOrCreateAsync($"tenant_{tenantHost}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                return await tenancyService.FindTenantAsync(tenantHost);
            });

            if ( tenantDisplayDataModel != null )
            {
                SetTenantSetter (tenantSetter,tenantDisplayDataModel);
                context.Request.Headers[TenantHeaderKey] = tenantSetter.CurrentTenantId.ToString ();
                return true;
            }
        }

        context.Response.Redirect (rootDomain);
        return false;
    }

    private static void SetTenantSetter (ITenantSetter tenantSetter,TenantDisplayDataModel tenantDisplayDataModel)
    {
        tenantSetter.CurrentTenantId = tenantDisplayDataModel.MyTenantId;
        tenantSetter.TenantStore = tenantDisplayDataModel.StoreType;
        tenantSetter.TenantName = tenantDisplayDataModel.Name;
    }

    private static string ResolveFromSubdomain (this HttpContext context,string? rawHost)
    {
        string? host;
        if ( rawHost == null )
        {
            host = context.Request.Host.Host ?? "";
        }
        else
        {
            host = rawHost;
        }

        if ( host != null )
        {
            string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

            segments = RemoveResevedWord (segments.Length > 0 ? segments : null);

            if ( segments!.Length > 2 )
            {
                return segments[0];
            }
        }

        return "";
    }

    private static string? ResolveFromDomain (this HttpContext context,string? rawHost)
    {
        string? host;
        if ( rawHost == null )
        {
            host = context.Request.Host.Host ?? "";
        }
        else
        {
            host = rawHost;
        }

        if ( host != null )
        {

            string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

            segments = RemoveResevedWord (segments!.Length > 0 ? segments! : null);

            if ( segments!.Length > 1 )
            {
                return segments[0];
            }
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

}