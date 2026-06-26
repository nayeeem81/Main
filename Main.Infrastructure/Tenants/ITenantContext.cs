using Main.Common;
using System.Security.Claims;

namespace Main.Infrastructure;

public interface ITenantContext
{
    ClaimsPrincipal? User
    {
        get;
    }

    string IdentityId
    {
        get;
    }

    string TenantId
    {
        get;
    }

    string CurrentUserClainText
    {
        get;
    }

    DateTime GetLocalNow ();

    BaseDataModel GetCreateBaseDataModel ();

    BaseDataModel GetUpdateBaseDataModel ();
}
