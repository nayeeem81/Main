using Main.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Services;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceDependencies ( 
                                this IServiceCollection services,
                                IConfiguration configuration )
    {
        //Register Infrastructure (repository & Data Contexts)
        services.AddInfrastructureServices ( configuration );

        //Register Business Services
        services.AddScoped<IAccountService,AccountService> ( );
        services.AddScoped<IAdminPostService,AdminPostService> ( );
        services.AddScoped<IProductService,ProductService> ( );
        services.AddScoped<IPageService,PageService> ( );
        services.AddScoped<IPagePanelService,PagePanelService> ( );
        
        return services;
    }
}
