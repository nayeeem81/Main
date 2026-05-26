namespace DataTransferModel;

public class IdentityUserDataModel
{
    public IdentityUserDataModel() { }

    public string UserId
    {
        get; set;   
    }

    public string? UserName
    {
        get; set;
    }

    public DateTimeOffset? LockoutEnd
    {
        get; set;   
    }
}
