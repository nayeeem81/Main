using Main.Common.Enums;

namespace Main.Common.Model;

public class BaseDataModel
{
    public BaseDataModel()
    {
    }

    public EnumCurrency Currency { get; set; }
       
    public EnumCompanyName HostCompanyName { get; set; }
       
    public EnumCountry HostCountry { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public string Id;

}
