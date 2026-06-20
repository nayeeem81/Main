using Main.Common.Enums;

namespace DataTransferModel;

public class PageDataModel
{
    public PageDataModel ( )
    {
        ListPanels = new List<PanelDataModel> ( );
    }

    public int PageID
    {
        get; set;
    }

    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }

    public EnumCompanyName CompanyName
    {
        get; set;
    }

    public List<PanelDataModel> ListPanels
    {
        get; set;
    }

    public void CreatePanel ( PanelDataModel pageDataModel )
    {
        if ( ListPanels == null )
        {
            ListPanels = new List<PanelDataModel> ( );
        }

        if ( pageDataModel != null )
        {
            ListPanels.Add ( pageDataModel );
        }
    }
}
