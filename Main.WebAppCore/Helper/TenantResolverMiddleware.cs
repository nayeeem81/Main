using Main.Common.Enums;
using Main.Infrastructure;
using Main.Services;
namespace WebAppCore.Helper;

public class TenantSetter: ITenantSetter
{
    public string CurrentTenantId
    {
        get; set;
    }

    public EnumStoreType TenantStore
    {
        get; set;
    }

    public string TenantName
    {
        get; set;
    }
}

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;


    public TenantResolverMiddleware ( RequestDelegate next )
    {
        _next = next;
    }

    public async Task InvokeAsync ( HttpContext context,ITenantSetter tenantSetter,
        ITenancyService tenancyService )
    {
        string host = context.Request.Host.Host ?? string.Empty;

        if ( !string.IsNullOrWhiteSpace ( host ) )
        {
            string[] segments = host.Split('.');

            if ( segments.Length > 0 && segments[0] == "www" )
            {
                segments = segments.Skip ( 1 ).ToArray ( );
                host = string.Join ( ".",segments );
            }
            else if ( segments.Length > 2 )
            {
                string subdomain = segments[0];

                await tenancyService.FindTenantAsync ( subdomain );

            }
            else if ( segments.Length > 1 )
            {
                string domain = segments[0];
                await tenancyService.FindTenantAsync ( domain );
            }
            else if ( host.Length > 0 )
            {
                await tenancyService.FindTenantAsync ( host );
            }
        }

        if ( tenancyService.TenancyFound )
        {
            tenantSetter.CurrentTenantId = tenancyService.CurrentTenant!.TenantId;
            tenantSetter.TenantName = tenancyService.CurrentTenant!.Name;
            tenantSetter.TenantStore = tenancyService.CurrentTenant!.ShopType;
        }
        else
        {
            throw new Exception ( "We are facing challenges and under maintenance." );
        }


        await _next ( context );
    }
}