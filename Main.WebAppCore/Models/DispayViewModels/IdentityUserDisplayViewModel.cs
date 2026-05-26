namespace WebApp.ViewModel;

public class IdentityUserDisplayViewModel
{
    public IdentityUserDisplayViewModel ( ) { }

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
