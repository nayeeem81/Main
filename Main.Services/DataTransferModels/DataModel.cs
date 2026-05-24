using Main.Common.Model;
namespace DataTransferModel;

public class DataModel
{
    public DataModel ( )
    {
        BaseDataModel = new BaseDataModel ( );
    }

    public BaseDataModel BaseDataModel
    {
        get;
        set;
    }
}
