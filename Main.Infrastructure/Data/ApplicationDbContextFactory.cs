using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Main.Infrastructure;

public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext (string[] args)
    {
        // 1. Point to your Main.WebAppCore folder to find appsettings.json
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Main.WebAppCore");

        // Fallback: If running the tool directly from within the WebAppCore directory
        if ( !Directory.Exists (basePath) )
        {
            basePath = Directory.GetCurrentDirectory ();
        }

        // 2. Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if ( string.IsNullOrEmpty (connectionString) )
        {
            throw new InvalidOperationException ("Could not find 'DefaultConnection' string in your appsettings.json.");
        }



        // 3. Configure your database provider (e.g., UseSqlServer, UseNpgsql, etc.)
        _ = optionsBuilder.UseSqlServer (connectionString);

        return new ApplicationDbContext (optionsBuilder.Options);
    }
}