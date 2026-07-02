namespace Main.Services;

public interface ITenantInvitationService
{
    Task<string> InviteUserAsync (string tenantId,string email,string tenantRole,string invitedByUserId,CancellationToken ct = default);
    Task<bool> AcceptInvitationAsync (string token,string? fullName,CancellationToken ct = default);
}
