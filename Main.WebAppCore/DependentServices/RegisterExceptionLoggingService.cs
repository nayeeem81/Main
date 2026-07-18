using Main.Infrastructure.CrosscuttingHelperServices;

namespace Main.WebAppCore.DependentServices;

public static class RegisterExceptionLoggingService
{
    public static IServiceCollection AddExceptionLogging (this IServiceCollection services,
    IConfiguration configuration)
    {

        _ = services.AddScoped<IExceptionLoggingService,ExceptionLoggingService> ();

        return services;
    }
}
