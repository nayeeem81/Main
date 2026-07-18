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
        string? tenantHost = context.ResolveFromSubdomain()
                            ?? context.ResolveFromDomain()
                            ?? ReutePathExtensions.ResolveFromPath(context);

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

}