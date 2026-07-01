using Main.Model.DomainModel;

namespace Domain.Model;

public class EmailSmtp: RootBaseEntity
{
    public EmailSmtp ()
    {
    }

    public int Id
    {
        get; set;
    }

    public string FromName { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 587;

    public string FromEmail { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool EnableSsl { get; set; } = true;

    public string? FkTenantId
    {
        get; set;
    } = string.Empty;

    public virtual Tenant? Tenant
    {
        get; set;
    }
}
