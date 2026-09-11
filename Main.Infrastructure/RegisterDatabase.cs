using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.Model.Identity;
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
        var identityConnectionString = configuration.GetConnectionString("IdentityAppConnection");
        var tenantConnectionString = configuration.GetConnectionString("TenantConnection");
        var logConnectionString = configuration.GetConnectionString("LogConnection");

        _ = services.AddDbContext<LogDbContext> (options =>
        {
            _ = options.UseLazyLoadingProxies ();
            _ = options.UseSqlServer (logConnectionString);
            _ = options.EnableSensitiveDataLogging ();
            _ = options.EnableDetailedErrors ();
        });

        _ = services.AddDbContext<TenantDbContext> (options =>
        {
            _ = options.UseLazyLoadingProxies ();
            _ = options.UseSqlServer (tenantConnectionString);
            _ = options.EnableSensitiveDataLogging ();
            _ = options.EnableDetailedErrors ();
        });

        _ = services.AddDbContext<IdentityAppDbContext> (options =>
        {
            _ = options.UseLazyLoadingProxies ();
            _ = options.UseSqlServer (identityConnectionString);
            _ = options.EnableSensitiveDataLogging ();
            _ = options.EnableDetailedErrors ();
        }).AddIdentity<ApplicationUser,IdentityRole> (options =>
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
        .AddEntityFrameworkStores<IdentityAppDbContext> ()
        .AddDefaultTokenProviders ();

        _ = services.Configure<DataProtectionTokenProviderOptions>
        (options => options.TokenLifespan = TimeSpan.FromHours (2));

        // Crucial: Register Unit of Work
        _ = services.AddScoped<IUnitOfWork,UnitOfWork> ();

        return services;
    }
}