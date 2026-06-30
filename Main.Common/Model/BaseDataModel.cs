namespace Main.Common;

public class BaseDataModel
{
    public BaseDataModel ()
    {
        IsActive = true;
    }

    public bool IsActive
    {
        get; set;
    }

    public string? TenantUserId
    {
        get; set;
    }

    public string ApplicationUserId
    {
        get; set;
    }

    public EnumCurrency TenantCurrency
    {
        get; set;
    }

    public EnumCountry TenantCountry
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
