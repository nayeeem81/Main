using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using IRepository;

namespace Main.Infrastructure;

public static class RegisterDatafrastructure
{
    public static IServiceCollection AddDataInfrastructureServices ( 
                  this IServiceCollection services,IConfiguration configuration )
    {

        
        services.AddDatabases ( configuration );

        services.AddIdentity ( configuration );


        //Register Repository
        services.AddScoped<IUserRepository,UserRepository> ( );
        services.AddScoped<IAdminPostImageRepository,AdminPostImageRepository> ( );
        services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );
        services.AddScoped<IProductImageRepository,ProductImageRepository> ( );
        services.AddScoped<IProductRepository,ProductRepository> ( );
        services.AddScoped<IPageRepository,PageRepository> ( );

        return services;

    }
}

