using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Services;

public static class RegisterServices
{
    public static IServiceCollection AddService ( this IServiceCollection services,
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

        services.AddEmailService ( configuration );
        services.AddScoped<ITenancyService,TenancyService> ( );
        services.AddScoped<IAccountService,AccountService> ( );
        services.AddScoped<IAdminPostService,AdminPostService> ( );
        services.AddScoped<IProductService,ProductService> ( );
        services.AddScoped<IPageService,PageService> ( );
        services.AddScoped<IEmailSenderService,EmailSenderService> ( );

        return services;
    }
}
