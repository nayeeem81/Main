using Main.Common.Enums;
using Main.Common.Model;
using System.Security.Claims;

namespace Main.Services;

public interface IUserContext
{
    public ClaimsPrincipal? User
    {
        get;
    }

    string IdentityId
    {
        get;
    }

    EnumCompanyName EnumCompanyName
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

    EnumShopType EnumShopType
    {
        get;
    }


    DateTime GetLocalNow ( );

    BaseDataModel GetCreateBaseDataModel ( );

    BaseDataModel GetUpdateBaseDataModel ( );

}
