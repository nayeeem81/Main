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
        var rawHost = context.Request.Host.Value;

        string? tenantHost = context.ResolveFromSubdomain(rawHost)
                            ?? context.ResolveFromDomain(rawHost);
        //ReutePathExtensions.ResolveFromPath(context, tenantPath);

        if ( !string.IsNullOrEmpty (tenantHost) )
        {
            TenantDisplayDataModel? tenantDisplayDataModel =
            await memoryCache.GetOrCreateAsync($"tenant_{tenantHost}", async entry =>
            {
                // 1. MANDATORY: Set the size to satisfy your global SizeLimit
                _ =  entry.SetSize(1) ;

                // Resets the 1-hour lifetime every time this tenant is requested
                _ =  entry.SetSlidingExpiration(TimeSpan.FromHours(1)) ;

                // 2. OPTIONAL: Set how long this tenant data stays in memory
                _ =  entry.SetAbsoluteExpiration(TimeSpan.FromHours(1)) ;

                return await tenancyService.FindTenantAsync(tenantHost);
            });

            if ( tenantDisplayDataModel != null )
            {
                SetTenantSetter (tenantSetter,tenantDisplayDataModel);
                context.Request.Headers[TenantHeaderKey] = tenantSetter.CurrentTenantId.ToString ();
                return true;
            }
        }

        //context.Response.Redirect (rootDomain);
        return false;
    }

    private static void SetTenantSetter (ITenantSetter tenantSetter,TenantDisplayDataModel tenantDisplayDataModel)
    {
        tenantSetter.CurrentTenantId = tenantDisplayDataModel.MyTenantId;
        tenantSetter.TenantStore = tenantDisplayDataModel.StoreType;
        tenantSetter.TenantName = tenantDisplayDataModel.Name;
    }

    private static string ResolveFromSubdomain (this HttpContext context,string host)
    {

        string[]? segments = host.Split('.', StringSplitOptions.RemoveEmptyEntries);

        segments = RemoveResevedWord (segments.Length > 0 ? segments : null);

        if ( segments!.Length > 2 )
        {
            return segments[0];
        }

        return "";
    }

    private static string? ResolveFromDomain (this HttpContext context,string host)
    {
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