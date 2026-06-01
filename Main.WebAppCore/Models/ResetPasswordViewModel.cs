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


    [Required]
    [StringLength ( 100, ErrorMessage = "The password must be at least {2} characters long.",MinimumLength = 8 )]
    [DataType ( DataType.Password )]
    public string Password { get; set; } = string.Empty;



    [DataType ( DataType.Password )]
    [Display ( Name = "Confirm password" )]
    [Compare ( "Password", ErrorMessage = "The password and confirmation password do not match." )]
    public string ConfirmPassword { get; set; } = string.Empty;
}
