using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Main.Model.Identity;

public class EmailOutboxMessage: BaseEntity
{
    public EmailOutboxMessage ()
    {
    }

    [Key]
    public int Id
    {
        get; set;
    }

    public string? ReceiverEmail
    {
        get; set;
    }

    public string? Subject
    {
        get; set;
    }

    public string? Body
    {
        get; set;
    }

    public DateTime? CreatedOnUtc
    {
        get; set;
    }

    public DateTime? ProcessedOnUtc
    {
        get; set;
    }

    public string? Error
    {
        get; set;
    }

    public int? RetryCount
    {
        get; set;
    }
}
