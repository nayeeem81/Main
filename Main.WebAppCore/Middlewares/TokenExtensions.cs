using Main.Infrastructure;

namespace Main.WebAppCore.Middlewares;

public static class TokenExtensions
{
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
