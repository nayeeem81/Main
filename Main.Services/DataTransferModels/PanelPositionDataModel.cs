using Main.Common.Model;

namespace DataTransferModel;

public class PanelPositionDataModel: DataModel
{
    public PanelPositionDataModel ( )
    {
        BaseDataModel = new BaseDataModel ( );
    }

    public int PanelID
    {
        get; set;
    }

    public int PageID
    {
        get; set;
    }

    public int PanelPosition
    {
        get; set;
    }
}
