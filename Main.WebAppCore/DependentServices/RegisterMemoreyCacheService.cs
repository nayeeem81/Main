namespace Main.WebAppCore.DependentServices;

public static class RegisterMemoreyCacheService
{
    public static IServiceCollection AddSessionMemoryCache (this IServiceCollection services,
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


        return services;
    }
}
