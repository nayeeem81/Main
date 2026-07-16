using Domain.Model;

namespace Main.IRepository;

public interface ITenantUserRepository
{
    Task AddAsync (TenantUser membership,CancellationToken ct = default);

    Task<bool> ExistsAsync (Guid tenantId,string userId,CancellationToken ct = default);

    Task<TenantUser?> GetByUserIdAsync (string userId,Guid tenantId,
    CancellationToken ct = default);
}
