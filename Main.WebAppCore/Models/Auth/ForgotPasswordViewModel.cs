using ResourceLibrary.Resources;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class ForgotPasswordViewModel : BaseViewModel
{
    public ForgotPasswordViewModel() {
        PageName = "ForgotPassword";
    }

   
    [EmailAddress ( ErrorMessageResourceName = "InvalidEmailFormat", ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Required ( ErrorMessageResourceName = "EmailRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Email", Prompt = "EmailPlaceholder", ResourceType = typeof ( SharedResource ) )]
    public string Email { get; set; } = string.Empty;
}

