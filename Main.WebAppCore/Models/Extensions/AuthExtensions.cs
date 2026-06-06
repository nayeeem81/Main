using Main.Common.HelperRelated;
using Main.Services;
using WebAppCore.ViewModel;

namespace WebAppCore.ViewModel.Extensions;


public static class AuthExtensions
{
    public static bool CheckPasswordMatch ( string password,string rePassword )
    {
        if ( password == null || rePassword == null )
        {
            return false;
        }

        if ( password.Length != rePassword.Length )
        {
            return false;
        }

        return string.Compare ( password,rePassword,StringComparison.Ordinal ) == 0;
    }

    public static UserAccountDataModel MapToDataModel ( RegistrationViewModel accountDisplayViewModel )
    {
        UserAccountDataModel userAccountDataModel
            = new UserAccountDataModel();

        userAccountDataModel.Email = accountDisplayViewModel.Email;

        userAccountDataModel.PhoneNumber = accountDisplayViewModel.Phone;

        userAccountDataModel.UserName =
            StringRelated.GetUserNameFromEmail ( accountDisplayViewModel.Email );

        userAccountDataModel.NormalizedUserName
            = accountDisplayViewModel.Email.ToUpper ( );

        userAccountDataModel.Password = accountDisplayViewModel.Password;

        return userAccountDataModel;
    }

}
