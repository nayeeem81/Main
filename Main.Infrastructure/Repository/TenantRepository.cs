using Domain.Model;
using Main.Infrastructure.Database;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantRepository: ITenantRepository
{
    private readonly ApplicationDbContext _context;

    public TenantRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public Tenant? CurrentTenant
    {
        get;
        set;
    }

    public async Task FindCurrentTenantAsync (string? hostName)
    {
        string host = hostName != null ? hostName : "";

        if ( host.Length == 0 )
        {
            CurrentTenant = null;
        }

        CurrentTenant = await _context.Tenants
            .IgnoreQueryFilters ()
            .FirstOrDefaultAsync<Tenant>
             (tenant => tenant.HostName.Length == host.Length
              && string.Equals (tenant.HostName,host));
    }

    public async Task<Tenant?> GetTenantByIdAsync (Guid tenantId)
    {
        Tenant? tenant = await _context.Tenants.FirstOrDefaultAsync (tenant => tenant.MyTenantId == tenantId);

        return tenant;
    }

    public async Task<Tenant?> CreateTenantAsync (Tenant tenant)
    {
        if ( tenant != null )
        {
            _ = _context.Tenants.Add (tenant);
            _ = await _context.SaveChangesAsync ();
        }

        return tenant;
    }
}
