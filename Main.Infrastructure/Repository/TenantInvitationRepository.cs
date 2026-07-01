using Domain.Model;
using Main.Common;
using Main.Infrastructure;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantInvitationRepository: ITenantInvitationRepository
{
    private readonly ApplicationDbContext _db;

    public TenantInvitationRepository (ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TenantInvitation?> GetByTokenAsync (string token,CancellationToken ct = default)
        => await _db.TenantInvitations.FirstOrDefaultAsync (x => x.Token == token,ct);

    public async Task<TenantInvitation?> GetByEmailAndTenantAsync (string tenantId,string email,CancellationToken ct = default)
        => await _db.TenantInvitations.FirstOrDefaultAsync (x => x.TenantId == tenantId && x.Email == email,ct);

    public async Task AddAsync (TenantInvitation invitation,CancellationToken ct = default)
    {
        _ = await _db.TenantInvitations.AddAsync (invitation,ct);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task UpdateAsync (TenantInvitation invitation,CancellationToken ct = default)
    {
        _ = _db.TenantInvitations.Update (invitation);
        _ = await _db.SaveChangesAsync (ct);
    }

    public async Task<bool> ExistsAsync (string tenantId,string email,CancellationToken ct = default)
        => await _db.TenantInvitations.AnyAsync (x => x.TenantId == tenantId && x.Email == email && x.Status == InvitationStatus.Pending,ct);
}
