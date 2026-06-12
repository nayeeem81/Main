using Main.Common.Enums;
using Main.Common.Model;

namespace Domain.Model;

public class BaseEntity
{
    public BaseEntity ( )
    {
        IsActive = true;
    }

    public void CreateBaseData ( BaseDataModel modelBase )
    {
        IdentityUserId = modelBase.Id.Trim ( );

        CreatedDate = modelBase.CreatedDate;
        CreatedBy = modelBase.CreatedBy;

        HostCompanyName = modelBase.HostCompanyName;
        HostCountry = modelBase.HostCountry;

        ModifiedBy = modelBase.ModifiedBy;
        ModifiedDate = modelBase.ModifiedDate;

        IsActive = true;
    }

    public void ModifyBaseData ( BaseDataModel modelBase )
    {
        ModifiedDate = modelBase.ModifiedDate;
        ModifiedBy = modelBase.ModifiedBy;

        HostCompanyName = modelBase.HostCompanyName;
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

    public EnumCompanyName HostCompanyName
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
}
