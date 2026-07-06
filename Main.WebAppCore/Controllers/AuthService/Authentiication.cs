using DataTransferModel;
using Main.Services;
using WebAppCore.ViewModel;

namespace Main.WebAppCore.Controllers.AuthService;

public static class AuthentiicationExtension
{
    public static async Task<bool> InvalidApplicationUser (
    IAccountService accountService,
    ApplicationUserDataModel? applicationIdentityUserDataModel,
    LoginViewModel loginDisplayViewModel,string resolvedTenantId)
    {
        if ( applicationIdentityUserDataModel == null )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please check your credentials and try again.";

            return true;
        }


        if ( applicationIdentityUserDataModel?.TenantId != resolvedTenantId )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please, check your email if you have any account in this website.";

            return true;
        }

        if ( !await IsEmailConfirmed (accountService,loginDisplayViewModel) )
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

    public static async Task<bool> IsEmailConfirmed (IAccountService accountService,LoginViewModel loginDisplayViewModel)
    {
        bool result = await accountService.IsEmailConfirmedAsync (loginDisplayViewModel.Email);

        return result;
    }


}
