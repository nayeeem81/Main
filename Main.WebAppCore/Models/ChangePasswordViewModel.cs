using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class ChangePasswordViewModel : BaseViewModel
{
    public ChangePasswordViewModel() {
        PageName = "Change Password";
    }

    [Required ( ErrorMessage = "The email address is required." )]
    public string Email { get; set; }


    [Required (ErrorMessage = "The current password is required.")]
    [DataType ( DataType.Password )]
    [Display ( Name = "Current password" )]
    public string CurrentPassword { get; set; } = string.Empty;


    [Required (ErrorMessage = "The new password is required.")]
    [DataType ( DataType.Password )]
    [Display ( Name = "New password" )]
    public string NewPassword { get; set; } = string.Empty;
}
