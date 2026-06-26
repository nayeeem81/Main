using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Tenant;

public static class TenantAuthorizationService
{
    public static IServiceCollection AddAuthorization (this IServiceCollection services,IConfiguration configuration)
    {
        _ = services.AddTransient<IAuthorizationHandler,TenantRoleHandler> ();

        _ = services.AddAuthorization (options =>
        {
            options.AddPolicy ("TenantAdmin",policy => policy.Requirements.Add (new TenantRoleRequirement ("Admin")));
            options.AddPolicy ("TenantContentManager",policy => policy.Requirements.Add (new TenantRoleRequirement ("ContentManager")));
            options.AddPolicy ("TenantMember",policy => policy.Requirements.Add (new TenantRoleRequirement ("Member")));
        });

        return services;
    }
}
