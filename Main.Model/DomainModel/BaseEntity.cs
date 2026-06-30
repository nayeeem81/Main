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
        IdentityUserId = modelBase.ApplicationUserId?.Trim ();
        TenantUserId = modelBase.TenantUserId?.Trim ();

        TenantCountry = modelBase.TenantCountry;
        TenantCurrency = modelBase.TenantCurrency;

        CreatedBy = modelBase.CreatedBy;
        CreatedDate = modelBase.CreatedDate;

        ModifiedBy = null;
        ModifiedDate = null;

        DeletedBy = null;
        DeletedDate = null;

        IsActive = modelBase.IsActive;
    }

    public void ModifyBaseData (BaseDataModel modelBase)
    {
        IdentityUserId = modelBase.ApplicationUserId?.Trim ();
        TenantUserId = modelBase.TenantUserId?.Trim ();

        ModifiedBy = modelBase.TenantUserId?.Trim ();
        ModifiedDate = modelBase.ModifiedDate;

        DeletedDate = null;
        DeletedBy = null;

        IsActive = modelBase.IsActive;
    }

    public void DeleteBaseData (BaseDataModel modelBase)
    {
        IdentityUserId = modelBase.ApplicationUserId?.Trim ();
        TenantUserId = modelBase.TenantUserId?.Trim ();

        DeletedBy = modelBase.TenantUserId?.Trim ();
        DeletedDate = modelBase.DeletedDate?.Date;

        IsActive = modelBase.IsActive;
    }

    public string CreatedBy
    {
        get; set;
    }

    public string? ModifiedBy

    {
        get; set;
    }

    public string? DeletedBy
    {
        get; set;
    }

    public DateTime CreatedDate
    {
        get; set;
    }

    public DateTime? ModifiedDate
    {
        get; set;
    }

    public DateTime? DeletedDate
    {
        get; set;
    }

    public EnumCountry? TenantCountry
    {
        get; set;
    }

    public bool IsActive
    {
        get; set;
    }

    public string? IdentityUserId
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
    public string? Continent
    {
        get;
        set;
    }

    public string? TenantUserId
    {
        get;
        set;
    }

    public EnumCurrency? TenantCurrency
    {
        get;
        set;
    }
}
