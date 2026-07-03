namespace Main.Common;

public class BaseDataModel
{
    public BaseDataModel ()
    {
    }

    public bool IsActive
    {
        get; set;
    }

    public string? SessionUserId
    {
        get; set;
    }

    public string? TenantUserId
    {
        get; set;
    }

    public string GlobalUserRole
    {
        get; set;
    }

    public string? TenantUserRole
    {
        get; set;
    }

    public Country? TenantCountry
    {
        get; set;
    }

    public string? TenantContinent
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
}
