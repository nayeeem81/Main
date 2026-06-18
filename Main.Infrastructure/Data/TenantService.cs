
namespace Main.Infrastructure;

public class TenantService: ITenantSetter
{
    public string CurrentTenantId
    {
        get;
        set;
    }
}
