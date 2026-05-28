using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Main.Infrastructure; 
namespace Main.Migrator;

class Program
{
    static async Task Main ( string[] args )
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                
                var webAppPath = Path.Combine(AppContext.BaseDirectory, "../../../../Main.WebAppCore");

                config.SetBasePath(Directory.Exists(webAppPath) ? webAppPath : AppContext.BaseDirectory)
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {

                services.AddInfrastructureServices(context.Configuration);
               
                services.AddLogging(logging => logging.AddConsole());

            })
            .Build();

        


        using ( var scope = host.Services.CreateScope ( ) )
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {

                logger.LogInformation ( "Resolving the database initialization pipeline..." );
                
                var initializer = serviceProvider.GetRequiredService<ApplicationDbContext>();

                await initializer.Database.EnsureCreatedAsync ( );

                logger.LogInformation ( "=== Database Processing Completed Successfully ===" );



                logger.LogInformation ( "Resolving the database initialization pipeline..." );
                
                var intBusAppDbCnx = serviceProvider.GetRequiredService<BussinessAppDbContext>();
                
                await intBusAppDbCnx.Database.EnsureCreatedAsync ( );

                logger.LogInformation ( "=== Database Processing Completed Successfully ===" );

            }
            catch ( Exception ex )
            {
                logger.LogCritical ( ex,"A fatal exception halted the migration processing engine." );

                Environment.ExitCode = 1; 
            }
        }
    }
}
