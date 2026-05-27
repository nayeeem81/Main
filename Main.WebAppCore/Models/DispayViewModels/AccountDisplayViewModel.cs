using System.ComponentModel.DataAnnotations;
using ResourceLibrary.Resources;

namespace WebApp.ViewModel;

public class AccountDisplayViewModel: BaseViewModel
{
    public AccountDisplayViewModel ( string pageName)
    {
        PageName = pageName;
    }

    public int UserID { get; set; }


    [Required ( ErrorMessage = "Please enter your user name!" )]
    [Display(Name = "User Name:")]
    public string UserName { get; set; }


    [Required ( ErrorMessage = "Please enter your email!" )]
    [Display(Name = "Email:")]
    public string Email { get; set; }


    [Required ( ErrorMessage = "Please enter your phone number!" )]
    [Display(Name = "Phone Number:")]
    public string Phone { get; set; }

    [Required ( ErrorMessage = "Please enter your password!" )]
    [Display(Name = "Password:")]
    [DataType ( DataType.Password )]
    public string Password { get; set; }


    [Required ( ErrorMessage = "Please re-enter your password!" )] 
    [Display(Name = "Re-Password:")]
    [DataType ( DataType.Password )]
    public string RePassword { get; set; }



    [Required ( ErrorMessageResourceName = "ClientNameRequired", ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "ClientName", Prompt = "ClientNamePlaceholder", ResourceType = typeof ( SharedResource ) )]
    public string ClientName { get; set; }

}
