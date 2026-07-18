using Main.WebAppCore.DependentServices;
using Main.WebAppCore.DepententServices;
using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.DepententServices;

public static class TenantAuthorizationService
{
    public static IServiceCollection AddAuthorization (this IServiceCollection services,IConfiguration configuration)
    {
        _ = services.AddScoped<IAuthorizationHandler,TenantRoleHandler> ();

        _ = services.AddAuthorization (options =>
        {
            options.AddPolicy ("TenantAdmin",policy => policy.Requirements.Add (new TenantRoleRequirement ("Admin")));

            options.AddPolicy ("TenantContentManager",policy => policy.Requirements.Add (new TenantRoleRequirement ("ContentManager")));

            options.AddPolicy ("TenantMember",policy => policy.Requirements.Add (new TenantRoleRequirement ("Member")));
        });

        return services;
    }
}
