using Main.Common;
using Main.Services;

namespace WebAppCore.ViewModel.Extensions;


public static class AuthExtensions
{
    public static UserAccountDataModel MapToDataModel (RegistrationViewModel? accountDisplayViewModel)
    {
        UserAccountDataModel userAccountDataModel = new ()
        {
            Email = accountDisplayViewModel?.Email!,
            PhoneNumber = accountDisplayViewModel?.Phone!,
            UserName =StringRelated.GetTrimmedRemovedSpaseString(accountDisplayViewModel?.UserName!),

            NormalizedUserName = StringRelated.GetTrimmedRemovedSpaseString(accountDisplayViewModel?.UserName!).ToUpper (),

            Password = accountDisplayViewModel?.Password!,
            ClientName = accountDisplayViewModel?.ClientName!
        };

        return userAccountDataModel;
    }

}
