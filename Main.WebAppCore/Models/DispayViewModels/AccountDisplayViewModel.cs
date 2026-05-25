using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class AccountDisplayViewModel: BaseViewModel
{
    public AccountDisplayViewModel ( string pageName)
    {
        PageName = pageName;
    }

    public int UserID { get; set; }


    [Display(Name = "User Name")]
    public string UserName { get; set; }


    [Display(Name = "Email")]
    public string Email { get; set; }


    [Display(Name = "Phone Number")]
    public string Phone { get; set; }


    [Display(Name = "Password")]
    public string Password { get; set; }


    [Display(Name = "Re-Password")]
    public string RePassword { get; set; }


    [Display(Name = "Client Name")]
    public string ClientName { get; set; }

}
