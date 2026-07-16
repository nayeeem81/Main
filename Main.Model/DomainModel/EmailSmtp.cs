using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class EmailSmtp: BaseEntity
{
    public EmailSmtp ()
    {
    }

    [Key]
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

    public Guid? FkTenantId
    {
        get; set;
    }


    public virtual Tenant? Tenant
    {
        get; set;
    }
}
