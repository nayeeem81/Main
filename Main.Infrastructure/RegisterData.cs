using IRepository;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Repository;

namespace Main.Infrastructure;

public static class RegisterData
{
    public static IServiceCollection AddRepository (
                  this IServiceCollection services,IConfiguration configuration )
    {
        services.AddScoped<TenantService> ( );

        services.AddScoped<ITenantSetter> ( provider => provider.GetRequiredService<TenantService> ( ) );

        services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );

        services.AddScoped<IProductRepository,ProductRepository> ( );

        services.AddScoped<IPageRepository,PageRepository> ( );

        services.AddScoped<IPanelRepository,PanelRepository> ( );

        return services;

    }
}

