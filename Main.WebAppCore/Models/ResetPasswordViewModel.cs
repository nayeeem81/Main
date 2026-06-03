using ResourceLibrary.Resources;

using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class ResetPasswordViewModel : BaseViewModel
{
    public ResetPasswordViewModel() {
        PageName = "Reset Password";
    }

    [Required]
    public string Token { get; set; } = string.Empty;


    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;


    [Required ( ErrorMessageResourceName = "PasswordRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Password",Prompt = "PasswordPlaceholder",ResourceType = typeof ( SharedResource ) )]
    [DataType ( DataType.Password )]
    public string Password { get; set; } = string.Empty;



    [DataType ( DataType.Password )]
    [Display ( Name = "ConfirmPassword",Prompt = "PasswordPlaceholder",ResourceType = typeof ( SharedResource ) )]
    [Compare ( "Password", ErrorMessageResourceName = "PasswordsDoNotMatch",ErrorMessageResourceType = typeof ( SharedResource ) )]
    public string ConfirmPassword { get; set; } = string.Empty;
}
