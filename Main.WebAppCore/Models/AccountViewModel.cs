using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class AccountViewModel : BaseViewModel
{
    public AccountViewModel()
    {
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


    [Display(Name = "Password")]
    public string CurrentPassword { get; set; }

    [Display(Name = "New Password")]
    public string NewPassword { get; set; }


    [Display(Name = "Client Name")]
    public string ClientName { get; set; }

    public string CompanyWebsite { get; set; }

    public string FacebookEmail { get; set; }

    public bool IsUser { get; set; }

    public bool IsCompany { get; set; }

    public bool IsAdminUser { get; set; }

    public string AdminUserEmail { get; set; }

    public string Remarks { get; set; }

    public string FBUserID { get; set; }

    public string CompanyDomainUrl { get; set; }
}
