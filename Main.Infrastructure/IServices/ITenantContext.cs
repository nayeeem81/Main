using Main.Common;
using System.Security.Claims;

namespace Main.Infrastructure;

public interface ITenantContext
{
    ClaimsPrincipal? User
    {
        get;
    }

    string ApplicationUserId
    {
        get;
    }

    string TenantId
    {
        get;
    }

    string? GetCurrentTenantRole ();

    DateTime GetLocalNow ();

    BaseDataModel GetCreateBaseDataModel ();

    BaseDataModel GetUpdateBaseDataModel ();

    BaseDataModel GetDeleteBaseDataModel ();
}
