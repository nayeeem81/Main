namespace WebAppCore.ViewModel;

public class IdentityUserViewModel
{
    public IdentityUserViewModel ( ) { }

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
