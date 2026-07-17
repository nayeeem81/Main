namespace Main.Model.DomainModel;

public class RootBaseEntity
{
    public RootBaseEntity ()
    {
    }

    public string? SessionUserId
    {
        get;
        set;
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

    public bool IsActive
    {
        get; set;
    }
}
