using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Services;

public static class RegisterServices
{
    public static IServiceCollection AddService (this IServiceCollection services,
    IConfiguration configuration)
    {


        _ = services.AddMemoryCache (options =>
        {
            options.SizeLimit = 1024;
            options.CompactionPercentage = 0.25;
        });

        _ = services.AddSession (options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes (20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        _ = services.AddScoped<ITenancyService,TenancyService> ();
        _ = services.AddScoped<IAccountService,AccountService> ();
        _ = services.AddScoped<IAdminPostService,AdminPostService> ();
        _ = services.AddScoped<IProductService,ProductService> ();
        _ = services.AddScoped<IPageService,PageService> ();
        _ = services.AddScoped<IEmailSenderService,EmailSenderService> ();

        return services;
    }
}
