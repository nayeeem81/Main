using ResourceLibrary.Resources;
using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class ChangePasswordViewModel : BaseViewModel
{
    public ChangePasswordViewModel() {
        PageName = "Change Password";
    }

    [Required ( ErrorMessage = "The email address is required." )]
    public string Email { get; set; }


    [Required ( ErrorMessageResourceName = "PasswordRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "CurrentPassword",Prompt = "CurrentPasswordPlaceholder",ResourceType = typeof ( SharedResource ) )]
    [DataType ( DataType.Password )]
    public string CurrentPassword { get; set; } = string.Empty;


    [Required (ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof ( SharedResource ) )]
    [DataType ( DataType.Password )]
    [Display ( Name = "NewPassword", Prompt = "NewPasswordPlaceholder", ResourceType = typeof ( SharedResource ) )]
    public string NewPassword { get; set; } = string.Empty;
}
