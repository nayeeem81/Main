using Domain.Model;

namespace Main.IRepository;

public interface ITenantInvitationRepository
{
    Task<TenantInvitation?> GetByTokenAsync (string token,CancellationToken ct = default);

    Task<TenantInvitation?> GetByEmailAndTenantAsync
    (Guid tenantId,string email,CancellationToken ct = default);

    Task AddAsync (TenantInvitation invitation,CancellationToken ct = default);

    Task UpdateAsync (TenantInvitation invitation,CancellationToken ct = default);

    Task<bool> ExistsAsync (Guid tenantId,string email,CancellationToken ct = default);
}
