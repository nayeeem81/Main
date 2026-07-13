using Main.Common;
using Main.Infrastructure;
using System.Security.Claims;

namespace Main.WebAppCore.Tenant;

public class TenantContext: ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _tenantId;

    public TenantContext (IHttpContextAccessor httpContextAccessor,
    ITenantSetter tenantSetter)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantId = tenantSetter.CurrentTenantId;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    ClaimsPrincipal? ITenantContext.User
    {
        get => User;
    }

    public string GetCurrentTenantRole ()
    {
        return User?.FindFirst ("TenantRole")?.Value ?? "";
    }

    public string TenantId => GetTenantId ();

    public string ApplicationUserId => GetCurrentUserId ();

    public DateTime GetLocalNow ()
    {
        string timeZoneId = "Bangladesh Standard Time";
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc (DateTime.UtcNow,userTimeZone);
    }

    public BaseDataModel GetCreateBaseDataModel ()
    {
        BaseDataModel baseDataModel = new ()
        {
            CreatedDate = GetLocalNow ( ),

            CreatedBy = GetCurrentUserId (),
            TenantUserId = GetCurrentUserId (),
            SessionUserId = GetCurrentUserId (),

            TenantUserRole = GetCurrentTenantRole (),
            IentityRole = GetIdentityRole() ,

            TenantCountry = AppSettings.Current.EnumCountry,
            IsActive = true
        };

        return baseDataModel;
    }



    public BaseDataModel GetUpdateBaseDataModel ()
    {
        BaseDataModel baseDataModel = new ()
        {
            ModifiedDate = GetLocalNow ( ) ,

            ModifiedBy = GetCurrentUserId (),
            SessionUserId = GetCurrentUserId (),
            TenantUserId = GetCurrentUserId (),

            TenantUserRole = GetCurrentTenantRole (),
            IentityRole = GetIdentityRole (),

            IsActive = true,
            TenantCountry = AppSettings.Current.EnumCountry
        };

        return baseDataModel;
    }

    public BaseDataModel GetDeleteBaseDataModel ()
    {
        BaseDataModel baseDataModel = new ()
        {
            DeletedDate = GetLocalNow ( ),

            DeletedBy = GetCurrentUserId (),
            SessionUserId = GetCurrentUserId (),
            TenantUserId = GetCurrentUserId (),

            TenantCountry = AppSettings.Current.EnumCountry,
            IsActive = false,

            TenantUserRole = GetCurrentTenantRole (),
            IentityRole = GetIdentityRole ()
        };

        return baseDataModel;
    }

    private string GetTenantId ()
    {
        return User?.FindFirst ("TenantId")?.Value ?? "";
    }

    private string GetCurrentUserId ()
    {
        return User?.FindFirst (ClaimTypes.NameIdentifier)?.Value ?? "";
    }

    private string GetIdentityRole ()
    {
        return User?.IsInRole ("User") != null ? "User" : "GlobalAdmin";
    }
}
