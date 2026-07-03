
namespace Main.Common;

public class TenantVariableModel
{
    public TenantVariableModel ()
    {
    }

    public int ValueID
    {
        get; set;
    }

    public int ParentID
    {
        get; set;
    }

    public EnumTenantVariable Variable
    {
        get; set;
    }

    public string Text
    {
        get; set;
    }

    public StoreType TenantStore
    {
        get; set;
    }

    public string TenantId
    {
        get; set;
    }
}
