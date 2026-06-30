using Main.Common;
using Main.Infrastructure;
using System.Security.Claims;

namespace Main.WebAppCore.Tenant;

public class TenantContext: ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantSetter _tenantSetter;
    public TenantContext (IHttpContextAccessor httpContextAccessor,ITenantSetter tenantSetter)
    {
        _httpContextAccessor = httpContextAccessor;
        _tenantSetter = tenantSetter;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string IdentityId
                  => User?.FindFirst (ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    ClaimsPrincipal? ITenantContext.User
    {
        get => User;
    }

    public string CurrentUserClainText => User?.FindFirst ("TenantRole")?.Value ?? string.Empty;

    public string TenantId
    {
        get;
        set;
    }

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
            CreatedBy = IdentityId,
            TenantCountry = AppSettings.Current.EnumCountry,
            TenantCurrency = AppSettings.Current.EnumCurrency,
            TenantUserId = IdentityId,
            ApplicationUserId = IdentityId,
            IsActive = true
        };

        return baseDataModel;
    }

    public BaseDataModel GetUpdateBaseDataModel ()
    {
        BaseDataModel baseDataModel = new ()
        {
            ModifiedDate = GetLocalNow ( ),
            ModifiedBy = IdentityId,
            TenantCountry = AppSettings.Current.EnumCountry,
            TenantCurrency = AppSettings.Current.EnumCurrency,
            ApplicationUserId = IdentityId,
            TenantUserId = IdentityId,
            IsActive = true
        };
        return baseDataModel;
    }

    public BaseDataModel GetDeleteBaseDataModel ()
    {
        BaseDataModel baseDataModel = new ()
        {
            DeletedDate = GetLocalNow ( ),
            DeletedBy = IdentityId,
            TenantCountry = AppSettings.Current.EnumCountry,
            TenantCurrency = AppSettings.Current.EnumCurrency,
            ApplicationUserId = IdentityId,
            TenantUserId = IdentityId,
            IsActive = false
        };
        return baseDataModel;
    }
}
