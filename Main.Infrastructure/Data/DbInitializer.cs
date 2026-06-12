using global::Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data;
 
public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<DbInitializer> _logger;
    private readonly IDatabaseSeeder _seeder;

    public DbInitializer (
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<DbInitializer> logger,
        IDatabaseSeeder seeder)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _seeder = seeder;
    }

    public async Task InitializeAsync ( )
    {
        try
        {
            _logger.LogInformation ( "Checking for pending database migrations..." );

            
            if ( ( await _context.Database.GetPendingMigrationsAsync ( ) ).Any ( ) )
            {
                _logger.LogInformation ( "Applying pending migrations..." );
                await _context.Database.MigrateAsync ( );
                _logger.LogInformation ( "Database successfully migrated." );
            }
            else
            {
                _logger.LogInformation ( "Database is already up to date." );
            }

            await _seeder.SeedAsync ( );
        }
        catch ( Exception ex )
        {
            _logger.LogError ( ex,"An error occurred while initializing the database." );
            throw;
        }
    }
}


