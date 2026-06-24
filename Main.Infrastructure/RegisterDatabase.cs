using Domain.Model;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterDatabase
{
    public static IServiceCollection AddDatabase (
        this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _ = services.AddDbContext<ApplicationDbContext> (options =>
        {
            _ = options.UseSqlServer (connectionString);
        });

        _ = services.AddIdentity<ApplicationUser,IdentityRole> (options =>
        {
            var identitySettings = configuration.GetSection("IdentitySettings");
            var password = identitySettings.GetSection("Password");
            var lockOut = identitySettings.GetSection("Lockout");
            var signIn = identitySettings.GetSection("SignIn");
            var user = identitySettings.GetSection("User");

            options.SignIn.RequireConfirmedEmail = signIn.GetValue<bool> ("RequireConfirmedEmail");
            options.Password.RequireDigit = password.GetValue<bool> ("RequireDigit");
            options.Password.RequireLowercase = password.GetValue<bool> ("RequireLowercase");
            options.Password.RequireUppercase = password.GetValue<bool> ("RequireUppercase");
            options.Password.RequireNonAlphanumeric = password.GetValue<bool>
            ("RequireNonAlphanumeric");
            options.Password.RequiredLength = password.GetValue<int> ("RequiredLength");
            options.Lockout.DefaultLockoutTimeSpan = lockOut.GetValue<TimeSpan> ("DefaultLockoutTimeSpan");
            options.Lockout.MaxFailedAccessAttempts = lockOut.GetValue<int> ("MaxFailedAccessAttempts");
            options.Lockout.AllowedForNewUsers = lockOut.GetValue<bool> ("AllowedForNewUsers");
            options.User.RequireUniqueEmail = user.GetValue<bool> ("RequireUniqueEmail");
        })
        .AddEntityFrameworkStores<ApplicationDbContext> ()
        .AddRoles<IdentityRole> ()
        .AddDefaultTokenProviders ();

        _ = services.Configure<DataProtectionTokenProviderOptions>
        (options => options.TokenLifespan = TimeSpan.FromHours (2));

        _ = services.AddAuthorization (options =>
        {
            options.AddPolicy ("TenantAdmin",policy => policy.Requirements.Add (new TenantRoleRequirement ("Admin")));
            options.AddPolicy ("TenantContentManager",policy => policy.Requirements.Add (new TenantRoleRequirement ("ContentManager")));
            options.AddPolicy ("TenantMember",policy => policy.Requirements.Add (new TenantRoleRequirement ("Member")));
        });

        return services;
    }
}

public class TenantRoleRequirement: IAuthorizationRequirement
{
    public string AllowedRole
    {
        get;
    }
    public TenantRoleRequirement (string allowedRole) => AllowedRole = allowedRole;
}