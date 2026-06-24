using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class PageViewModel: BaseViewModel
{
    public PageViewModel ( )
    {
        ListPagePanels = new List<PanelViewModel> ( );
    }

    public int PageID
    {
        get; set;
    }


    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }


    [Display ( Name = "Configurable Page Name" )]
    public string? PublicPageName
    {
        get; set;
    }


    [Display ( Name = "Company Name" )]
    public string? CompanyName
    {
        get; set;
    }


    public List<PanelViewModel> ListPagePanels
    {
        get; set;
    }


    public void CreatePageContent ( PanelViewModel pageContentViveModel )
    {
        if ( ListPagePanels == null )
        {
            ListPagePanels = new List<PanelViewModel> ( );
        }

        if ( pageContentViveModel != null )
        {
            ListPagePanels.Add ( pageContentViveModel );
        }
    }
}
