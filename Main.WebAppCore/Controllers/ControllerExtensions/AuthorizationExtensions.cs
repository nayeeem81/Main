using Main.Infrastructure;
using Main.Services;
using System.Security.Claims;

namespace Main.WebAppCore.Controllers.Extensions;

public static class AuthorizationExtensions
{
    public static async Task<string> GetTenantUserRole
    (IAccountService accountService,string email,string resolvedTenantId)
    {
        string tenantRole = await accountService.GetTenantUserRoleClaim
        (email, resolvedTenantId);

        return tenantRole;
    }

    public static void AddTenantIsolatedHeaderToken
    (HttpContext context,ITokenService tokenService,
     string userId,string resolvedTenantId,
     string role,int minutes,int days)
    {
        // 2. Create your tokens after successful sign-in
        var accessJwt =  tokenService.GenerateAccessToken
        (userId,resolvedTenantId,minutes);

        var refreshTokenStr = tokenService.GenerateRefreshToken();

        // 3. COOKIE 1: Save the short-lived Access JWT (Expires in 15 minutes)
        context.Response.Cookies.Append ($".App.AccessToken.{resolvedTenantId}",
        accessJwt.ToString () ?? "",
        new CookieOptions
        {
            HttpOnly = true,   // Protects against XSS attacks stealing your JWT
            Secure = true,     // Mandates HTTPS through Nginx
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes (15),
            Path = "/"         // Accessible by all pages in your app
        });

        // 4. COOKIE 2: Save the long-lived Refresh Token (Expires in 7 days)
        context.Response.Cookies.Append ($".App.RefreshToken.{resolvedTenantId}",refreshTokenStr,new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays (7),
            Path = "/account/refresh-token" // Locked down specifically to your refresh endpoint
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