using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterIdentity
{
    public static IServiceCollection AddIdentity ( 
        this IServiceCollection services,IConfiguration configuration )
    {

        services.AddIdentity<IdentityUser,IdentityRole> ( options =>
        {
            //SignIn
            options.SignIn.RequireConfirmedAccount = false;

            //Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes ( 5 );
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            //User
            options.User.RequireUniqueEmail = true;
        } )
        .AddEntityFrameworkStores<ApplicationDbContext> ( )
        .AddDefaultTokenProviders( )
        .AddSignInManager ( );

        return services;

    }
}

