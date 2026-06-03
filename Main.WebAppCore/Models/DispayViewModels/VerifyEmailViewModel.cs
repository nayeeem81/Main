using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.ViewModel;

public class VerifyEmailViewModel: BaseViewModel
{
    public VerifyEmailViewModel ( )                          
    {
        PageName = "Email Verification";
        Subject = "Please, first Verify Your Email to Login.";
    }


    public VerifyEmailViewModel ( string pageName)
    {
        PageName = pageName;
        Subject = "Please, first Verify Your Email to Login.";
    }


    public string Email { get; set; }


    public string Subject
    {
        get; set;
    }   


    public string Token { get; set; }


    public string Message { get; set; }


    public string VerifyLink
    {
        get; set;
    }

}
