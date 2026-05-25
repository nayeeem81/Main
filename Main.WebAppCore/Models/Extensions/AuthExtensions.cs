using Main.Common.HelperRelated;
using Main.Services;

namespace WebApp.ViewModel.Extensions;


public static class AuthExtensions
{
    public static bool CheckPasswordMatch ( string password,string rePassword )
    {
        if ( password.Length != rePassword.Length )
        {
            return false;
        }

        return string.Compare ( password,rePassword,StringComparison.Ordinal ) == 0;
    }

    public static UserAccountDataModel MapToDataModel ( AccountDisplayViewModel accountDisplayViewModel )
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
