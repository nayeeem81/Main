namespace Domain.Model;

public class EmailOutboxMessage: BaseEntity
{
    public EmailOutboxMessage ()
    {
    }

    public int Id
    {
        get; set;
    }

    public string ReceiverEmail { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedOnUtc
    {
        get; set;
    }

    public string? Error
    {
        get; set;
    }

    public int RetryCount
    {
        get; set;
    }


}
