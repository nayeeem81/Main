using Domain.Model;
using Main.Infrastructure;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantUserRepository: ITenantUserRepository
{
    private readonly ApplicationDbContext _db;

    public TenantUserRepository (ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync (TenantUser membership,CancellationToken ct = default)
    {
        _ = await _db.TenantUsers.AddAsync (membership,ct);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task<bool> ExistsAsync (string tenantId,string userId,CancellationToken ct = default)
        => await _db.TenantUsers.AnyAsync (x => x.TenantId == tenantId && x.UserId == userId);

    public async Task<TenantUser?> GetByUserIdAsync (string userId,string tenantId,CancellationToken ct = default)
        => await _db.TenantUsers.FirstOrDefaultAsync (x => x.UserId == userId && x.TenantId == tenantId,ct);
}
