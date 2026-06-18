using Domain.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterDatabase
{
    public static IServiceCollection AddDatabase (
        this IServiceCollection services,IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext> ( options =>
        {
            options.UseSqlServer ( connectionString );
        } );

        services.AddIdentity<ApplicationUser,ApplicationRole> ( options =>
        {
            var identitySettings = configuration.GetSection("IdentitySettings");
            var password = identitySettings.GetSection("Password");
            var lockOut = identitySettings.GetSection("Lockout");
            var signIn = identitySettings.GetSection("SignIn");
            var user = identitySettings.GetSection("User");

            options.SignIn.RequireConfirmedEmail = signIn.GetValue<bool> ( "RequireConfirmedEmail" );
            options.Password.RequireDigit = password.GetValue<bool> ( "RequireDigit" );
            options.Password.RequireLowercase = password.GetValue<bool> ( "RequireLowercase" );
            options.Password.RequireUppercase = password.GetValue<bool> ( "RequireUppercase" );
            options.Password.RequireNonAlphanumeric = password.GetValue<bool>
                                                               ( "RequireNonAlphanumeric" );
            options.Password.RequiredLength = password.GetValue<int> ( "RequiredLength" );
            options.Lockout.DefaultLockoutTimeSpan = lockOut.GetValue<TimeSpan> ( "DefaultLockoutTimeSpan" );
            options.Lockout.MaxFailedAccessAttempts = lockOut.GetValue<int> ( "MaxFailedAccessAttempts" );
            options.Lockout.AllowedForNewUsers = lockOut.GetValue<bool> ( "AllowedForNewUsers" );
            options.User.RequireUniqueEmail = user.GetValue<bool> ( "RequireUniqueEmail" );
        } )
        .AddEntityFrameworkStores<ApplicationDbContext> ( )
        .AddDefaultTokenProviders ( )
        .AddSignInManager ( );

        services.AddScoped<IUserValidator<ApplicationUser>,TenantAwareUserValidator> ( );

        services.AddAuthorization ( options =>
        {
            options.AddPolicy ( "RequireAdminRole",policy => policy.RequireRole ( "Admin" ) );
        } );

        return services;
    }
}

