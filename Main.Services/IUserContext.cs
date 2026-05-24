using Main.Common.Enums;
using Main.Common.Model;

namespace Main.Services;

public interface IUserContext
{
    string UserId
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

    EnumCategoryFor EnumCategoryFor
    {
        get;
    }

    int SeedUserId
    {
        get;
    }


    DateTime GetLocalNow ( );

    BaseDataModel GetCreateBaseDataModel ( );

    BaseDataModel GetUpdateBaseDataModel ( );


}
