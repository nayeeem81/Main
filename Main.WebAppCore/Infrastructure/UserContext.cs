using Main.Common.Model;
using Main.Common.Enums;
using System.Security.Claims;
using Main.Services;

namespace WebApp.Infrastructure;

public class UserContext: IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext ( IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    
    //Current User
    public string UserId => User?.FindFirst ( ClaimTypes.NameIdentifier )?.Value ?? string.Empty;

    public string IdentityId => User?.FindFirst ( "IdentityId" )?.Value ?? string.Empty;

    //Configuration file
    public EnumCategoryFor EnumCategoryFor => ( EnumCategoryFor ) AppSettings.Current.EnumCategoryFor;

    public EnumCompanyName EnumCompanyName => ( EnumCompanyName ) AppSettings.Current.EnumCompanyName;

    public EnumCurrency EnumCurrency => ( EnumCurrency ) AppSettings.Current.EnumCurrency;

    public EnumCountry EnumCountry => ( EnumCountry ) AppSettings.Current.EnumCountry;

    public int SeedUserId => ( int ) AppSettings.Current.SeedUserId;

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

        int userId =  UserId != string.Empty && UserId != null ? Convert.ToInt32 ( UserId ) : SeedUserId;

        baseDataModel.CreatedBy = userId;
        baseDataModel.ModifiedBy = userId;

        return baseDataModel;
    }

    public BaseDataModel GetUpdateBaseDataModel ( )
    {
        BaseDataModel baseDataModel = new BaseDataModel();

        baseDataModel.ModifiedDate = GetLocalNow ( );
        baseDataModel.HostCompanyName = EnumCompanyName;
        baseDataModel.HostCountry = EnumCountry;
        baseDataModel.Currency = EnumCurrency;
        baseDataModel.ModifiedBy = ( int ) Convert.ToUInt32 ( UserId.ToString ( ) );

        return baseDataModel;
    }
}
