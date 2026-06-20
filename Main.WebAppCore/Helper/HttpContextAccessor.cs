using Main.Common.Enums;
using Main.Common.Model;
using Main.Services;

using System.Security.Claims;

namespace WebAppCore.Helper;

public class HttpContextAccessor: IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextAccessor ( IHttpContextAccessor httpContextAccessor )
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    //Current User
    public string IdentityId => User?.FindFirst ( ClaimTypes.NameIdentifier )?.Value ?? string.Empty;

    //Configuration file
    public EnumShopType EnumShopType => AppSettings.Current.EnumShopType;

    public EnumCompanyName EnumCompanyName => AppSettings.Current.EnumCompanyName;

    public EnumCurrency EnumCurrency => AppSettings.Current.EnumCurrency;

    public EnumCountry EnumCountry => AppSettings.Current.EnumCountry;

    public int PostImageSize => AppSettings.Current.PostImageSize;

    ClaimsPrincipal? IUserContext.User
    {
        get => User;
    }

    public DateTime GetLocalNow ( )
    {
        string timeZoneId = "Bangladesh Standard Time";

        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        return TimeZoneInfo.ConvertTimeFromUtc ( DateTime.UtcNow,userTimeZone );
    }

    public BaseDataModel GetCreateBaseDataModel ( )
    {
        BaseDataModel baseDataModel = new BaseDataModel();

        baseDataModel.ModifiedDate = GetLocalNow ( );
        baseDataModel.CreatedDate = GetLocalNow ( );
        baseDataModel.HostCompanyName = EnumCompanyName;
        baseDataModel.HostCountry = EnumCountry;
        baseDataModel.Currency = EnumCurrency;
        baseDataModel.CreatedBy = IdentityId;
        baseDataModel.ModifiedBy = IdentityId;

        baseDataModel.Id = IdentityId;

        return baseDataModel;
    }

    public BaseDataModel GetUpdateBaseDataModel ( )
    {
        BaseDataModel baseDataModel = new BaseDataModel();

        baseDataModel.ModifiedDate = GetLocalNow ( );
        baseDataModel.HostCompanyName = EnumCompanyName;
        baseDataModel.HostCountry = EnumCountry;
        baseDataModel.Currency = EnumCurrency;
        baseDataModel.ModifiedBy = IdentityId;

        return baseDataModel;
    }
}
