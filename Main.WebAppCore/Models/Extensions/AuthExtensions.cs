using Main.Common;
using Main.Services;

namespace WebAppCore.ViewModel.Extensions;


public static class AuthExtensions
{
    public static bool CheckPasswordMatch (string password,string rePassword)
    {
        if ( password == null || rePassword == null )
        {
            return false;
        }

        if ( password.Length != rePassword.Length )
        {
            return false;
        }

        return string.Compare (password,rePassword,StringComparison.Ordinal) == 0;
    }

    public static UserAccountDataModel MapToDataModel (RegistrationViewModel? accountDisplayViewModel)
    {
        UserAccountDataModel userAccountDataModel = new ()
        {
            Email = accountDisplayViewModel?.Email!,
            PhoneNumber = accountDisplayViewModel?.Phone!,
            UserName =StringRelated.GetUserNameFromEmail (accountDisplayViewModel?.UserName!),
            NormalizedUserName =
            StringRelated.GetUserNameFromEmail (accountDisplayViewModel?.UserName!).ToUpper (),
            Password = accountDisplayViewModel?.Password!,
            ClientName = accountDisplayViewModel?.ClientName!
        };

        return userAccountDataModel;
    }

}
