using Main.Common.Enums;

namespace DataTransferModel;

public class PanelPositionDataModel
{
    public PanelPositionDataModel ( )
    {
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

    public EnumCompanyName Company
    {
        get; set;
    }

    public EnumCountry Country
    {
        get; set;
    }
}
