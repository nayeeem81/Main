using Main.Common.Enums;
using Main.Common.Model;

using System.Security.Claims;
namespace Main.Services;

public interface IUserContext
{
    ClaimsPrincipal? User
    {
        get;
    }

    string IdentityId
    {
        get;
    }

    EnumCurrency EnumCurrency
    {
        get;
    }

    EnumCountry EnumCountry
    {
        get;
    }


    DateTime GetLocalNow ( );

    BaseDataModel GetCreateBaseDataModel ( );

    BaseDataModel GetUpdateBaseDataModel ( );
}
