using Domain.Model;

namespace Main.IRepository;

public interface ITenantUserRepository
{
    Task AddAsync (TenantUser membership,CancellationToken ct = default);

    Task<bool> ExistsAsync (string tenantId,string userId,CancellationToken ct = default);

    Task<TenantUser?> GetByUserIdAsync (string userId,string tenantId,
    CancellationToken ct = default);
}
