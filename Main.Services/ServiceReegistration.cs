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
        //Register Data Infrastructure 
        services.AddInfrastructureServices ( configuration );

        //Gives update about database exceptions in development environment 
        services.AddDatabaseDeveloperPageExceptionFilter ( );

        //Register Main Services
        services.AddScoped<IAccountService,AccountService> ( );
        services.AddScoped<IAdminPostService,AdminPostService> ( );
        services.AddScoped<IProductService,ProductService> ( );
        services.AddScoped<IPageService,PageService> ( );
        services.AddTransient<IEmailSenderService,EmailSenderService> ( );

        return services;
    }
}
