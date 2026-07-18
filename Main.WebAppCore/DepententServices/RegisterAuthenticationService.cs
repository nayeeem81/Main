using Main.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Main.WebAppCore.DependentServices;

public static class RegisterAuthenticationService
{
    public static IServiceCollection AddAuthentication (this IServiceCollection services,
    IConfiguration configuration)
    {
        _ = services.AddAuthentication (options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer (options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // Your standard key validation rules go here...
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // 1. Resolve your scoped tenant service safely from the request container
                    var tenantSetter = context.HttpContext.RequestServices.GetRequiredService<ITenantSetter>();

                    // 2. Retrieve the tenant ID that your middleware already resolved
                    var tenantId = tenantSetter.CurrentTenantId;

                    if ( !string.IsNullOrEmpty (tenantId.ToString ()) )
                    {
                        // 3. Build your custom dynamic multi-tenant cookie name string
                        var cookieName = $".App.AccessToken.{tenantId}";

                        // 4. Extract token payload from browser cookie
                        if ( context.Request.Cookies.TryGetValue (cookieName,out var token) )
                        {
                            context.Token = token;
                        }
                    }

                    return Task.CompletedTask;
                }
            };
        });


        return services;
    }
}
