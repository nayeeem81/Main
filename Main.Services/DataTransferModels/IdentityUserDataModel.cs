namespace DataTransferModel;

public class ApplicationUserDataModel
{
    public ApplicationUserDataModel ()
    {
    }

    public string Id
    {
        get; set;
    }

    public string? UserName
    {
        get; set;
    }

    public string? Email
    {
        get; set;
    }

    public DateTimeOffset? LockoutEnd
    {
        get; set;
    }

    public Guid MyTenantId
    {
        get; set;
    }
}
