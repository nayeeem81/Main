using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class UserRefreshToken: BaseEntity
{
    public UserRefreshToken ()
    {
    }

    public Guid Id
    {
        get; set;
    }

    [Required]
    public string UserId
    {
        get; set;
    }

    [ForeignKey (nameof (UserId))]
    public virtual ApplicationUser? User
    {
        get;
        set;
    }

    // The actual unique token string/hash
    public string Token
    {
        get; set;
    }

    public DateTime ExpiresAt
    {
        get; set;
    }

    public bool IsRevoked
    {
        get; set;
    }

    public DateTime CreatedAt
    {
        get; set;
    }

    // Optional: For token rotation tracking
    public string? ReplacedByToken
    {
        get; set;
    }
}

