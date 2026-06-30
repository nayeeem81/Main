using Main.Common;
namespace Domain.Model;

public interface IMustHaveTenant
{
    string TenantId
    {
        get; set;
    }
    EnumCountry? TenantCountry
    {
        get; set;
    }

    EnumCurrency? TenantCurrency
    {
        get; set;
    }

    string? Continent
    {
        get; set;
    }
    BaseDataModel BaseData
    {
        get; set;
    }
    bool IsActive
    {
        get; set;
    }

    string? TenantUserId
    {
        get;
        set;
    }

    void ModifyBaseData (BaseDataModel modelBase);

    void CreateBaseData (BaseDataModel modelBase);

    void DeleteBaseData (BaseDataModel modelBase);
}

