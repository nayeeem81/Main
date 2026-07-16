using DataTransferModel;
using Main.Services;
using WebAppCore.ViewModel;

namespace Main.WebAppCore.Controllers.Extensions;

public static class AuthentiicationExtensions
{
    public static async Task<bool> InvalidApplicationUser (
    IAccountService accountService,
    ApplicationUserDataModel? applicationIdentityUserDataModel,
    LoginViewModel loginDisplayViewModel,Guid resolvedTenantId)
    {
        if ( applicationIdentityUserDataModel == null )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please check your credentials and try again.";

            return true;
        }


        if ( applicationIdentityUserDataModel?.MyTenantId.ToString () != resolvedTenantId.ToString () )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please, check your email if you have any account in this website.";

            return true;
        }

        if ( !await EmailExtensions.IsEmailConfirmed (accountService,loginDisplayViewModel) )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please, check your email if you have any account in this website.";

            loginDisplayViewModel.EmailConfirmed = false;

            return true;
        }
        else
        {
            loginDisplayViewModel.EmailConfirmed = true;
        }

        return false;
    }

    public static async Task<bool> PasswordSignInAsync
    (IAccountService accountService,
    string userName,
    string password,
    bool isPersistent,
    bool lockoutOnFailure)
    {
        var result = await accountService.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        return result;
    }
}
