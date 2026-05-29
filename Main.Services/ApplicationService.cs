using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Services;

public static class ApplicationService
{
    public static IServiceCollection AddUserSessionService ( 
                                this IServiceCollection services,
                                IConfiguration configuration )
    {
        services.AddMemoryCache ( options =>
        {
            options.SizeLimit = 1024;
            options.CompactionPercentage = 0.25;
        } );

        services.AddSession ( options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes ( 20 );
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true; 
        } );

        services.ConfigureApplicationCookie ( options =>
        {
            options.LoginPath = "/Auth/Login";
            options.AccessDeniedPath = "/Auth/AccessDenied";
            
            options.ExpireTimeSpan = TimeSpan.FromMinutes ( 30 );
            options.SlidingExpiration = true;

            options.Cookie.Name = "AuthCookie";
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        } );

        return services;
    }
}
