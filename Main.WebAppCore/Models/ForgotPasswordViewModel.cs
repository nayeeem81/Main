using ResourceLibrary.Resources;

using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class ForgotPasswordViewModel : BaseViewModel
{
    public ForgotPasswordViewModel() {
        PageName = "Reset Password";
    }

   
    [EmailAddress ( ErrorMessage = "Invalid email format" )]
    [Required ( ErrorMessageResourceName = "EmailRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Email", Prompt = "EmailPlaceholder", ResourceType = typeof ( SharedResource ) )]
    public string Email { get; set; } = string.Empty;
}

