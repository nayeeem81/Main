using System.Security.Claims;

namespace Main.WebAppCore.Middleware;

public static class TenantTokenResolverExtensions
{
    public static bool CreateToken (TenantExpiringTokenEngine tokenEngine,string tenantId,string key,string token)
    {
        // context.Request.Headers.TryGetValue ("X-Tenant-Id",out var tenantIdHeader);

        //   if ( string.IsNullOrEmpty (tenantIdHeader) )
        //   {

        //   }

        //    string tenantIdToken = tenantIdHeader.ToString();
        //  string token =  tenantIdToken;

        // 4. Validate Token with resolved key

        _ = tokenEngine.ValidateToken (key,tenantId,token,key);




        // 5. Build Claims principal and append to HttpContext

        _ = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, tenantId),
            new Claim("TenantId", tenantId)
        };



        return true;
    }
}
