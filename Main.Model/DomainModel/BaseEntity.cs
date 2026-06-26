using Main.Common;

namespace Domain.Model;

public class BaseEntity: IMustHaveTenant
{
    public BaseEntity ()
    {
        IsActive = true;
    }

    public void CreateBaseData (BaseDataModel modelBase)
    {
        IdentityUserId = modelBase.Id.Trim ();

        CreatedDate = modelBase.CreatedDate;
        CreatedBy = modelBase.CreatedBy;

        HostCountry = modelBase.HostCountry;

        ModifiedBy = modelBase.ModifiedBy;
        ModifiedDate = modelBase.ModifiedDate;

        IsActive = true;
    }

    public void ModifyBaseData (BaseDataModel modelBase)
    {
        ModifiedDate = modelBase.ModifiedDate;
        ModifiedBy = modelBase.ModifiedBy;

        HostCountry = modelBase.HostCountry;

        IsActive = true;
    }

    public string CreatedBy
    {
        get; set;
    }

    public DateTime CreatedDate
    {
        get; set;
    }

    public string ModifiedBy
    {
        get; set;
    }

    public DateTime ModifiedDate
    {
        get; set;
    }

    public EnumCountry HostCountry
    {
        get; set;
    }

    public bool IsActive
    {
        get; set;
    }

    public string IdentityUserId
    {
        get; set;
    }


    public string TenantId
    {
        get;
        set;
    }

    public BaseDataModel BaseData
    {
        get;
        set;
    }
}
