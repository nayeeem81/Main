using Main.Common;
namespace Domain.Model;

public class TenantInvitation: BaseEntity
{
    public TenantInvitation ()
    {
    }

    public Guid InviteId
    {
        get; set;
    }

    public string Email
    {
        get; set;
    } = string.Empty;

    public string? InvitedByUserId
    {
        get; set;
    }

    public string? TenantRole
    {
        get; set;
    }

    public string Token
    {
        get; set;
    } = string.Empty;

    public InvitationStatus Status
    {
        get; set;
    } = InvitationStatus.Pending;

    public DateTime CreatedOn
    {
        get; set;
    } = DateTime.UtcNow;

    public DateTime ExpiresOn
    {
        get; set;
    } = DateTime.UtcNow.AddDays (7);

    public DateTime? AcceptedOn
    {
        get; set;
    }
}
