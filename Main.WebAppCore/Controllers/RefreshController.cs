using Main.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace Main.WebAppCore.Controllers
{
    public class RefreshController: Controller
    {
        public readonly ITenantSetter _tenantSetter;
        public readonly ITokenService _tokenService;
        public readonly ITenantContext _tenantContext;

        public RefreshController (ITenantSetter tenantSetter,ITokenService tokenService,ITenantContext tenantContext)
        {
            _tenantSetter = tenantSetter;
            _tokenService = tokenService;
            _tenantContext = tenantContext;
        }

        [HttpPost ("refresh-token")]
        public async Task<IActionResult> Refresh ()
        {
            var tenantId = _tenantSetter.CurrentTenantId;
            var cookieName = $".App.RefreshToken.{tenantId}";

            // Extract token from the secure cookie
            if ( !Request.Cookies.TryGetValue (cookieName,out var currentRefreshToken) )
            {
                return Unauthorized ("Missing token.");
            }

            try
            {
                // Execute the service logic
                var tokenResult = await _tokenService.RotateRefreshTokenAsync
                    (currentRefreshToken, tenantId,_tenantContext.ApplicationUserId);

                if ( tokenResult == null )
                {
                    return Unauthorized ("Invalid or expired token.");
                }

                // 3. COOKIE 1: Save the short-lived Access JWT (Expires in 15 minutes)
                Response.Cookies.Append ($".App.AccessToken.{tenantId}",
                tokenResult.AccessToken.ToString () ?? "",
                new CookieOptions
                {
                    HttpOnly = true,   // Protects against XSS attacks stealing your JWT
                    Secure = true,     // Mandates HTTPS through Nginx
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes (15),
                    Path = "/"         // Accessible by all pages in your app
                });

                // 4. COOKIE 2: Save the long-lived Refresh Token (Expires in 7 days)
                Response.Cookies.Append ($".App.RefreshToken.{tenantId}",tokenResult.RefreshToken,new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays (7),
                    Path = "/account/refresh-token" // Locked down specifically to your refresh endpoint
                });

                // Return the fresh access JWT in the JSON payload
                return Ok (new
                {
                    token = tokenResult.AccessToken
                });
            }
            catch ( SecurityException ex )
            {
                // Clear cookies immediately on breach detection
                Response.Cookies.Delete (cookieName);
                return Unauthorized (ex.Message);
            }
        }
    }
}
