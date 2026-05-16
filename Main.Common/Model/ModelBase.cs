using Main.Common.HelperRelated;

namespace Main.Common.Model;

public enum EnumModelBase
{
    Create = 1 , 
    Update = 2 
}

public class ModelBase
{
    public ModelBase() { }

    public int UserID { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int ModifiedBy { get; set; }

    public EnumCompanyName HostCompanyName { get; set; }

    public EnumCountry Country { get; set; }
}

public interface IModelBaseService
{
    void SetUpModelBase(EnumCompanyName hostCompanyName, EnumCountry country, int userId);

    ModelBase CreateEntity();

    ModelBase UpdateEntity();
}

public class ModelBaseService: IModelBaseService
{
    private int _id {  get; set; }
    private EnumCompanyName _hostCompanyName { get; set; }
    private EnumCountry _country { get; set; }

    public ModelBaseService()
    {
    }

    private DateTime GetDateToday() {
        return DateRelated.GetBangladeshCurrentDateTime();
    }

    public void SetUpModelBase(EnumCompanyName hostCompanyName, EnumCountry country, int userId)
    {
        _hostCompanyName = hostCompanyName;
        _country = country;
        _id = userId;
    }

    public ModelBase CreateEntity() {

        ModelBase objModleBaseLocal = new ModelBase();
        objModleBaseLocal.CreatedDate = GetDateToday();
        objModleBaseLocal.CreatedBy = _id;
        objModleBaseLocal.ModifiedDate = GetDateToday();
        objModleBaseLocal.ModifiedBy = _id;
        objModleBaseLocal.Country = _country;
        objModleBaseLocal.HostCompanyName = _hostCompanyName;

        return objModleBaseLocal;
    }

    public ModelBase UpdateEntity()
    {
        ModelBase objModleBaseLocal = new ModelBase();
       
        objModleBaseLocal.ModifiedDate = GetDateToday();
        objModleBaseLocal.ModifiedBy = _id;
        objModleBaseLocal.Country = _country;
        objModleBaseLocal.HostCompanyName = _hostCompanyName;

        return objModleBaseLocal;
    }
}
