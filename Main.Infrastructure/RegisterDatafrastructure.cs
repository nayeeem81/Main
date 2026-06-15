using IRepository;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Repository;

namespace Main.Infrastructure;

public static class RegisterDatafrastructure
{
    public static IServiceCollection AddDataInfrastructureServices (
                  this IServiceCollection services,IConfiguration configuration )
    {

        services.AddDatabases ( configuration );

        services.AddIdentitySecurity ( configuration );


        //Register Repository

        services.AddScoped<IAdminPostImageRepository,AdminPostImageRepository> ( );

        services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );

        services.AddScoped<IProductImageRepository,ProductImageRepository> ( );

        services.AddScoped<IProductRepository,ProductRepository> ( );

        services.AddScoped<IPageRepository,PageRepository> ( );

        services.AddScoped<IPanelRepository,PanelRepository> ( );

        return services;

    }
}

