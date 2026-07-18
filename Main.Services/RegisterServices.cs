using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Services;

public static class RegisterServices
{
    public static IServiceCollection AddService (this IServiceCollection services,
    IConfiguration configuration)
    {
        _ = services.AddScoped<ITenancyService,TenancyService> ();
        _ = services.AddScoped<IAccountService,AccountService> ();
        _ = services.AddScoped<IAdminPostService,AdminPostService> ();
        _ = services.AddScoped<IProductService,ProductService> ();
        _ = services.AddScoped<IPageService,PageService> ();

        return services;
    }
}
