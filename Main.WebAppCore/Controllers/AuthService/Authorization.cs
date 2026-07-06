using Main.Infrastructure;
using System.Security.Claims;

namespace Main.WebAppCore.Controllers.AuthService;

public static class AuthorizationExtension
{
    public static void AddTenantIsolatedHeaderToken
    (HttpContext context,ITokenService tokenService,
     string userId,string resolvedTenantId,
     string role,int minutes,int days)
    {
        // Identity role
        var roles = new[] { role };

        // Set isolated cookie for short life token
        _ = tokenService.GenerateAccessToken
        (userId,resolvedTenantId,roles,minutes);

        var refreshToken = tokenService.GenerateRefreshToken();

        // Set refresh token cookie key with tenant id, set refresh token
        context.Response.Cookies.Append
        ($"X-Refresh-Token-{resolvedTenantId}",
        refreshToken,
        new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays (days)
        });
    }

    public static void AddUserClaims
    (HttpContext context,string userId,string resolvedTenantId,
    string formatedTenantRole,string userName,string email)
    {
        List<Claim> listUserClaims =
        [
            new Claim (ClaimTypes.NameIdentifier,userId),
            new Claim (ClaimTypes.Role,"User"),
            new Claim ("tenant_id",resolvedTenantId),
            new Claim("TenantRole",formatedTenantRole),
            new Claim ("UserName", userName),
            new Claim ("Email",email)
        ];

        ClaimsIdentity claimsIdentity = new(listUserClaims);

        context.User.AddIdentity (claimsIdentity);
    }
}