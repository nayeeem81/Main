using Main.Common.Enums;
using Main.Common.Model;
using Main.Services;

using System.Security.Claims;
namespace WebAppCore.Helper;

public class UserContext: IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext ( IHttpContextAccessor httpContextAccessor )
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string IdentityId
                  => User?.FindFirst ( ClaimTypes.NameIdentifier )?.Value ?? string.Empty;

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
        BaseDataModel baseDataModel = new BaseDataModel
        {
            ModifiedDate = GetLocalNow ( ),
            CreatedDate = GetLocalNow ( ),
            HostCountry = EnumCountry,
            Currency = EnumCurrency,
            CreatedBy = IdentityId,
            ModifiedBy = IdentityId,
            Id = IdentityId
        };

        return baseDataModel;
    }

    public BaseDataModel GetUpdateBaseDataModel ( )
    {
        BaseDataModel baseDataModel = new BaseDataModel
        {
            ModifiedDate = GetLocalNow ( ),
            HostCountry = EnumCountry,
            Currency = EnumCurrency,
            ModifiedBy = IdentityId
        };

        return baseDataModel;
    }
}
