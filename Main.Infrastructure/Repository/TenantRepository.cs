using Domain.Model;

using IRepository;

using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TenantRepository: ITenantRepository
{
    private readonly ApplicationDbContext _context;

    public TenantRepository ( ApplicationDbContext context )
    {
        _context = context;
    }

    public TenantInfo? CurrentTenant
    {
        get;
        set;
    }

    public async Task FindCurrentTenantAsync ( string? hostName )
    {
        string host = hostName != null ? hostName : "";

        if ( host.Length == 0 )
        {
            CurrentTenant = null;
        }

        CurrentTenant = await _context.Tenants
            .IgnoreQueryFilters ( )
            .FirstOrDefaultAsync<TenantInfo>
             ( tenant => tenant.Domain.Length == host.Length
              && string.Equals ( tenant.Domain,host ) );


    }
}
