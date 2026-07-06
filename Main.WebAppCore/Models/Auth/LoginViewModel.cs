using ResourceLibrary.Resources;
using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class LoginViewModel: BaseViewModel
{
    public LoginViewModel ()
    {
        EmailConfirmed = false;
    }

    public LoginViewModel (string pageName)
    {
        PageName = pageName;
    }

    [Required (ErrorMessageResourceName = "EmailRequired",ErrorMessageResourceType = typeof (SharedResource))]
    [Display (Name = "Email",Prompt = "EmailPlaceholder",ResourceType = typeof (SharedResource))]
    public string Email
    {
        get; set;
    }


    [Required (ErrorMessageResourceName = "PasswordRequired",ErrorMessageResourceType = typeof (SharedResource))]
    [Display (Name = "Password",Prompt = "PasswordPlaceholder",ResourceType = typeof (SharedResource))]
    [DataType (DataType.Password)]
    public string Password
    {
        get; set;
    }

    public string Message { get; set; } = string.Empty;

    public bool? EmailConfirmed
    {
        get; set;
    }
}
