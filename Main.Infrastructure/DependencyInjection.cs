using Main.Infrastructure.Repository;
using Main.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices (
        this IServiceCollection services,
        IConfiguration configuration )
    {

        services.AddDbContext<WebBusinessEntityContext> ( options =>
            options.UseSqlServer (
                configuration.GetConnectionString ( "DefaultConnection" ),
                b => b.MigrationsAssembly ( typeof ( WebBusinessEntityContext ).Assembly.FullName ) ) );


        services.AddDbContext<ApplicationDbContext> ( options => 
            options.UseSqlServer (
                configuration.GetConnectionString ( "DefaultConnection" ),
                b => b.MigrationsAssembly ( typeof ( ApplicationDbContext ).Assembly.FullName ))) ;

           
        services.AddScoped<IAdminPostImageRepository,AdminPostImageRepository> ( );
        services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );
        services.AddScoped<IProductImageRepository,ProductImageRepository> ( );
        services.AddScoped<IProductRepository,ProductRepository> ( );
        services.AddScoped<IPageRepository,PageRepository> ( );

        return services;
    }
}

