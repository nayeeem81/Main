using Main.Common;

namespace Domain.Model;

public interface IMustHaveTenant
{
    public string TenantId
    {
        get; set;
    }

    EnumCountry HostCountry
    {
        get; set;
    }

    public BaseDataModel BaseData
    {
        get; set;
    }

    public bool IsActive
    {
        get; set;
    }

    void ModifyBaseData (BaseDataModel modelBase);

    void CreateBaseData (BaseDataModel modelBase);

}
