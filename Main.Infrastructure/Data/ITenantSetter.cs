namespace Main.Infrastructure;

public interface ITenantSetter
{
    string CurrentTenantId
    {
        get; set;
    }
}
