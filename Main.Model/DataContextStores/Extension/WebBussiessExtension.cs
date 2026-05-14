using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Model;

public static class WebBussiessExtensions
{
    public static IServiceCollection AddWebBussiessDbContext (
                           this IServiceCollection services,
                                IConfiguration configuration )
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection")
        
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        services.AddDbContext<WebBusinessEntityContext> ( options =>
        {
            options.UseSqlServer ( connectionString );

        } );


        return services;
    }
}

