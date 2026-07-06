## Understanding JWT and Multi-Tenancy

A JSON Web Token (JWT) is an open standard.

([RFC 7519] (https://datatracker.ietf.org/doc/html/rfc7519)) that defines a compact, self-contained way to securely transmit information between parties as a JSON object.

[1, 2, 3, 4] 

## What It Is?

A JWT consists of three parts separated by dots ():
[5, 6] 

* Header: Specifies the token type (JWT) and the signing algorithm (e.g., HMAC SHA256 or RSA).

* Payload: Contains the claims (statements about an entity, such as user ID, role, and tenant ID).

* Signature: Created by hashing the encoded header, encoded payload, and a secret key to ensure the token has not been tampered with.

[7, 8, 9, 10, 11] 

## Where and Why It Is Used

* Where: Used in APIs, Single Page Applications (SPAs), mobile backends, and distributed multi-tenant architectures.

* Why: They are stateless. The server does not need to store session data in memory or a database. The backend validates the incoming token using a cryptographic key, making it highly scalable.
[12, 13, 14, 15, 16] 

------------------------------
## Token Strategies: Short-Lived vs. Long-Lived Tokens

In a secure .NET 8.0 architecture, you must balance security with user experience by combining short-lived and long-lived tokens.


### Token Type: Short-Lived (Access Token)

1. Lifespan: 5 to 15 minutes
2. Why Used (Purpose): Minimizes damage if stolen; revokes access quickly when it expires.
3. When Used (Trigger): Sent with every HTTP request to access protected resources.
4. How Used (Storage & Transmission): Handled via the Authorization: Bearer <token> header. Stored in-memory by client apps.

### Token Type: Long-Lived (Refresh Token)

1. Lifespan: 7 to 30 days
2. Why Used (Purpose): Silently requests a new Access Token without forcing the user to re-authenticate.
3. When Used (Trigger): Triggered automatically by client interceptors when an access token expires.
4. How Used (Storage & Transmission) Stored in an HttpOnly, Secure, SameSite=Strict cookie to prevent XSS theft.
[17, 18, 19, 20, 21]

------------------------------
## Best Practice Token Naming for Multi-Tenancy

To support users opening multiple tenants across different browser tabs simultaneously, you cannot use a single fixed cookie name. If you do, Tab B will overwrite Tab A's cookies.

Use Tenant-Scoped Cookie Naming to isolate sessions completely:

* Access Token Header:
* Authorization: Bearer <Token> (passed dynamically by JavaScript per tab).
* Refresh Token Cookie: X-Refresh-Token-{TenantId} (e.g., X-Refresh-Token-tenantA).
* Antiforgery Token Cookie: X-XSRF-Token-{TenantId}
[22] 

------------------------------
## Architectural Design: Common Cryptographic Service

In a Monolithic Clean Architecture, token generation and decryption belong in your core application infrastructure. This promotes reusability across your Web MVC layer, background workers, and API endpoints.

// Location: Core 
// Application Layer (Interfaces)

public interface ITokenService
{

    string GenerateAccessToken(string userId, string tenantId, 
    IEnumerable<string> roles, int expiryInMinutes);

    string GenerateRefreshToken();

    ClaimsPrincipal? ValidateAndDecryptToken(string token, out SecurityToken? validatedToken);
}


// Location: Infrastructure Layer (Implementation)

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly TokenValidationParameters _validationParameters;
    private readonly byte[] _signingKey;

    public TokenService(IConfiguration config)
    {
        _signingKey = Encoding.UTF8.GetBytes(config["Jwt:Secret"]!);
        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_signingKey),
            ValidateIssuer = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

    public string GenerateAccessToken(string userId, string tenantId, IEnumerable<string> roles, int expiryInMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new("tenant_id", tenantId)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(62));

    public ClaimsPrincipal? ValidateAndDecryptToken(string token, out SecurityToken? validatedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            return tokenHandler.ValidateToken(token, _validationParameters, out validatedToken);
        }
        catch
        {
            validatedToken = null;
            return null; 
            // Return null if validation fails
        }
    }
}

------------------------------
## .NET 8.0 Multi-Tenant Middleware Framework
## 1. Context and Strategy

* Simple Design Strategy: The Custom Middleware intercepts requests early to resolve the current tenant and validate the access token using our ITokenService. 
[23] 

* Where to Validate: Inside the InvokeAsync step of the middleware pipeline. 
[24] 

* Purpose of Validation: Extracting the tenant_id and ClaimsPrincipal early allows the system to set up scoped database connections (e.g., query filtering) and populate the HttpContext.User object before routing occurs.

## 2. Implement the Tenant Resolution Middleware [25] 

// Location: Web MVC 
// Infrastructure Layer

public class TenantInfo
{
    public string? TenantId { get; set; }
}

public class MultiTenantAuthMiddleware
{
    private readonly RequestDelegate _next;

    public MultiTenantAuthMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ITokenService tokenService, TenantInfo tenantInfo)
    {
        // 1. Resolve Tenant from Subdomain, URL Path, or Header
        // Example: :
        //myapp.com or Header

        string? resolvedTenant = 
        context.Request.Headers["X-Tenant-Id"].FirstOrDefault() 
        ?? context.Request.Host.Value.Split('.').First();

        tenantInfo.TenantId = resolvedTenant;

        // 2. Extract Access Token

        string? authHeader = context.Request.Headers["Authorization"]
        .FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            
            // 3. Centralized Decryption & Validation

            var principal = 
            tokenService.ValidateAndDecryptToken(token, out var validatedToken);

            if (principal != null)
            {
                var tokenTenant = principal.FindFirst("tenant_id")?.Value;

                // 4. Multi-Tenant Cross-Contamination Check

                if (tokenTenant != null && 
                tokenTenant.Equals(resolvedTenant, StringComparison.OrdinalIgnoreCase))
                {
                    context.User = principal; 

                    // Token matches requested tenant context
                }
            }
        }

        await _next(context);
    }
}

------------------------------
## Multi-Tenant Antiforgery Validation

To secure a multi-tenant MVC layout against Cross-Site Request Forgery (CSRF) across browser tabs, configure dynamic tenant-scoped antiforgery settings.

// Location: Web MVC Customization

public class MultiTenantAntiforgeryCookieFilter : IAsyncAuthorizationFilter
{
    private readonly IAntiforgery _antiforgery;
    private readonly TenantInfo _tenantInfo;

    public MultiTenantAntiforgeryCookieFilter(IAntiforgery antiforgery, TenantInfo tenantInfo)
    {
        _antiforgery = antiforgery;
        _tenantInfo = tenantInfo;
    }

    public async Task OnAuthorizationFilterAsync
    (AuthorizationFilterContext context)
    {
        // Dynamically alter cookie names according to tenant context

        var options = context.HttpContext.RequestServices.GetRequiredService<IOptions<AntiforgeryOptions>>().Value;

        options.Cookie.Name = $".AspNetCore.Antiforgery.{_tenantInfo.TenantId}";

        if (HttpMethods.IsPost(context.HttpContext.Request.Method))
        {
            try
            {
                await _antiforgery.ValidateRequestAsync(context.HttpContext);
            }
            catch (AntiforgeryValidationException)
            {
                context.Result = new BadRequestObjectResult("Antiforgery token verification failed for tenant environment.");
            }
        }
    }
}

------------------------------
## Policy-Based Authorization for Multi-Tenancy
## 1. Define Requirement and Handler

This setup guarantees that user credentials match both the requested tenant context and the specific security roles allowed.
[26, 27] 

using Microsoft.AspNetCore.Authorization;

public class TenantRoleRequirement : IAuthorizationRequirement
{
    public string AllowedRole { get; }
    public TenantRoleRequirement(string allowedRole) => AllowedRole = allowedRole;
}

public class TenantRoleHandler : AuthorizationHandler<TenantRoleRequirement>
{
    private readonly TenantInfo _tenantInfo;

    public TenantRoleHandler(TenantInfo tenantInfo) => 
    _tenantInfo = tenantInfo;

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
    TenantRoleRequirement requirement)
    {
        var user = context.User;

        var currentTenant = _tenantInfo.TenantId;

        var tokenTenant = user.FindFirst("tenant_id")?.Value;

        var hasRole = user.IsInRole(requirement.AllowedRole);

        if (tokenTenant == currentTenant && hasRole)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

------------------------------
## Controller and Filter Architecture Execution
## Where to Instantiate and Use

* Controller Constructor: Instantiate long-running application dependencies here via dependency injection (ITokenService, DbContext).


* Action Filters (Best Practice): Use action filters for cross-cutting security operations like multi-tenant validation, validation checks, or global audit trails. This abstracts boilerplate code away from your business actions. 
[28, 29] 

// Location: Web MVC Controllers

[ApiController]
[Route("{tenant}/api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model, [FromRoute] string tenant)
    {
        // 1. Authenticate user against database tenant records
        // 2. If valid, build access and refresh strings

        var roles = new[] { "Admin" };

        var accessToken = _tokenService.GenerateAccessToken("user-123", tenant, roles, 15);

        var refreshToken = _tokenService.GenerateRefreshToken();

        // 3. Set isolated cookie for long life token

        Response.Cookies.Append($"X-Refresh-Token-{tenant}", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new { Token = accessToken });
    }

    [HttpGet("admin-dashboard")]
    [Authorize(Policy = "TenantAdminOnly")] 
    // Triggers policy verification

    [ServiceFilter(typeof(MultiTenantAntiforgeryCookieFilter))] 
    // Validates tenant-scoped CSRF
    public IActionResult GetDashboardData()
    {
        return Ok("Secure multi-tenant data stream accessible.");
    }
}

------------------------------
## .NET 8.0 Program.cs End-to-End Orchestration

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Infrastructure Dependencies

builder.Services.AddScoped<TenantInfo>();

builder.Services.AddSingleton<ITokenService, TokenService>();

// 2. Configure Scoped Action Filters
builder.Services.AddScoped<MultiTenantAntiforgeryCookieFilter>();

// 3. Register Core Policy Requirements
builder.Services.AddScoped<IAuthorizationHandler, TenantRoleHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantAdminOnly", policy =>
    policy.Requirements.Add(new TenantRoleRequirement("Admin")));
});

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// 4. Inject Multi-Tenant Verification into Pipeline Before Routing 
// Authorizations

app.UseMiddleware<MultiTenantAuthMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{tenant}/{controller=Home}/{action=Index}/{id?}");

app.Run();

### If you would like to explore this architecture further, tell me:

* What strategy do you use to resolve tenants (subdomains, URL paths, or request headers)?

* Do your tenants share a single combined database with a discriminator column, or do they use isolated databases per tenant?

* What identity store (such as ASP.NET Core Identity or a custom database solution) do you use to verify credentials?


[1] [https://tsvillain.medium.com](https://tsvillain.medium.com/json-web-tokens-jwt-refresh-tokens-the-complete-backend-guide-271bdf0e7b49)
[2] [https://afteracademy.com](https://afteracademy.com/article/implement-json-web-token-jwt-authentication-using-access-token-and-refresh-token)
[3] [https://www.scalekit.com](https://www.scalekit.com/blog/json-web-tokens-guide-for-developers)
[4] [https://www.ionos.co.uk](https://www.ionos.co.uk/digitalguide/websites/web-development/json-web-token-jwt/)
[5] [https://www.linkedin.com](https://www.linkedin.com/pulse/jwt-authentication-aspnet-core-80-blazor-server-sameer-ahmed-siddiqui-jr7df)
[6] [https://blog.bytebytego.com](https://blog.bytebytego.com/p/password-session-cookie-token-jwt)
[7] [https://senoritadeveloper.medium.com](https://senoritadeveloper.medium.com/what-is-jwt-json-web-token-e5a3d45e752)
[8] [https://www.linkedin.com](https://www.linkedin.com/pulse/jwt-authentication-aspnet-core-80-blazor-server-sameer-ahmed-siddiqui-jr7df)
[9] [https://zaven.co](https://zaven.co/blog/user-authentication-web-api-2-jwt-token/)
[10] [https://medium.com](https://medium.com/@vaibhavtiwari.945/understanding-jwt-tokens-a-comprehensive-guide-to-modern-authentication-bf7886d70a56)
[11] [https://medium.com](https://medium.com/@stanislousvanderputt/a-deep-dive-on-json-web-tokens-bf39c4bc81ba)
[12] [https://auth0.com](https://auth0.com/blog/asp-dot-net-core-authentication-tutorial/)
[13] [https://www.perforce.com](https://www.perforce.com/blog/aka/what-is-jwt)
[14] [https://medium.com](https://medium.com/@nwonahr/authentication-and-authorization-in-asp-net-core-a-comprehensive-guide-0b0f4a5343a0)
[15] [https://jitsi.support](https://jitsi.support/how-to/authenticate-users-jitsi-meet-jwt-tokens/)
[16] [https://www.linkedin.com](https://www.linkedin.com/pulse/jwt-authentication-refresh-tokens-net-core-web-api-mohd-saeed-ujnvc)
[17] [https://medium.com](https://medium.com/@abhinavsaichoudary30/jwt-authentication-explained-for-java-full-stack-developers-2329f160f6d7)
[18] [https://www.radware.com](https://www.radware.com/cyberpedia/application-security/jwt-authentication/)
[19] [https://skycloak.io](https://skycloak.io/blog/jwt-token-lifecycle-management-expiration-refresh-revocation-strategies/)
[20] [https://jasonwatmore.com](https://jasonwatmore.com/post/2020/05/25/aspnet-core-3-api-jwt-authentication-with-refresh-tokens)
[21] [https://www.linkedin.com](https://www.linkedin.com/top-content/artificial-intelligence/understanding-api-development/understanding-json-web-tokens/)
[22] [https://docs.stardog.com](https://docs.stardog.com/operating-stardog/security/oauth-integration)
[23] [https://fusionauth.io](https://fusionauth.io/blog/securing-golang-microservice)
[24] [https://medium.com](https://medium.com/@ravipatel.it/a-complete-beginners-guide-to-asp-net-core-net-8-middleware-1e35c0eab444)
[25] [https://www.codemag.com](https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5)
[26] [https://frontegg.com](https://frontegg.com/guides/how-to-persist-jwt-tokens-for-your-saas-application)
[27] [https://blog.devgenius.io](https://blog.devgenius.io/secure-jwt-json-web-token-authentication-implementation-in-go-a7ab3737ee7c)
[28] [https://dev.to](https://dev.to/ossan/custom-jwt-authentication-net-5-47p7)
[29] [https://www.themoonlight.io](https://www.themoonlight.io/en/review/agentic-jwt-a-secure-delegation-protocol-for-autonomous-ai-agents)
