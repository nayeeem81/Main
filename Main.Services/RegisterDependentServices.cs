
using Main.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Services;

public static class RegisterDependentServices
{
    public static IServiceCollection AddServiceDependencies (
                                this IServiceCollection services,
                                IConfiguration configuration )
    {


        services.AddEmailService ( configuration );

        services.AddDataInfrastructureServices ( configuration );

        services.AddUserSessionService ( configuration );

        services.AddDatabaseDeveloperPageExceptionFilter ( );


        // Registering Services
        services.AddScoped<IAccountService,AccountService> ( );
        services.AddScoped<IAdminPostService,AdminPostService> ( );
        services.AddScoped<IProductService,ProductService> ( );
        services.AddScoped<IPageService,PageService> ( );
        services.AddScoped<IEmailSenderService,EmailSenderService> ( );

        return services;
    }
}
