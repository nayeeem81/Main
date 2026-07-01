using Main.Common;
using Main.Model.DomainModel;
namespace Domain.Model;

public class BaseEntity: RootBaseEntity, IMustHaveTenant
{
    public BaseEntity ()
    {
        IsActive = true;
    }

    public void CreateParameters (BaseDataModel modelBase)
    {
        CreatedBy = modelBase.CreatedBy;
        CreatedDate = modelBase.CreatedDate;

        ModifiedBy = null;
        ModifiedDate = null;

        DeletedBy = null;
        DeletedDate = null;

        IsActive = modelBase.IsActive;

        AddSessionParameters (modelBase);
    }

    public void ModifyParameters (BaseDataModel modelBase)
    {
        ModifiedBy = modelBase.TenantUserId?.Trim ();
        ModifiedDate = modelBase.ModifiedDate;
        DeletedDate = null;
        DeletedBy = null;
        IsActive = modelBase.IsActive;

        AddSessionParameters (modelBase);
    }

    public void DeleteParameters (BaseDataModel modelBase)
    {
        DeletedBy = modelBase.TenantUserId?.Trim ();
        DeletedDate = modelBase.DeletedDate?.Date;
        IsActive = modelBase.IsActive;

        AddSessionParameters (modelBase);
    }

    public void AddSessionParameters (BaseDataModel modelBase)
    {
        TenantUserId = modelBase.TenantUserId?.Trim ();
        SessionUserId = modelBase.SessionUserId?.Trim ();

        TenantUserRole = modelBase.TenantUserRole?.Trim ();
        GlobalUserRole = modelBase.GlobalUserRole?.Trim ();

        TenantCountry = modelBase.TenantCountry;
        TenantContinent = modelBase.TenantContinent?.Trim ();
    }

    public string TenantId
    {
        get;
        set;
    }

    public string? TenantUserId
    {
        get;
        set;
    }

    public string? TenantUserRole
    {
        get;
        set;
    }

    public Country? TenantCountry
    {
        get; set;
    }

    public string? TenantContinent
    {
        get;
        set;
    }
}
