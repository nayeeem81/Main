using Main.Common;
using Main.Common.EnumClasses;

namespace BusinessModel;

public class PageContentDataModel: BaseDataModel
{

    public PageContentDataModel()
    {
        ListPagePanels = new List<PagePanelDataModel>();

        AV_PanelTemplate = DropDownSelectListItem.GetPanelTempletList();

        ListSelectProducts = new List<PanelPostDataModel>();

        ListSelectAds = new List<PanelPostDataModel>();
    }


    public PageContentDataModel(int pageId)
    {
        ListPagePanels = new List<PagePanelViewModel>();

        AV_PanelTemplate = DropDownSelectListItem.GetPanelTempletList();

        ListSelectProducts = new List<PanelPostDataModel>();

        ListSelectAds = new List<PanelPostDataModel>();

        PageID = pageId;
    }


    public int PageContentID { get; set; }



    public int PageID { get; set; }



    public PageDataModel? Page { get; set; }


    [Display(Name = "Panel Title")]
    public string PanelTitle { get; set; }

    [Display(Name = "Panel Template")]
    public EnumPanelTemplate EnumPanelTemplate { get; set; }


    public IEnumerable<SelectListItem> AV_PanelTemplate { get; set; }


    public List<PanelPostDataModel> ListSelectProducts { get; set; }


    public List<PanelPostDataModel> ListSelectAds { get; set; }


  
    public List<PagePanelViewModel> ListPagePanels { get; set; }


    public void CreatePagePanel(PagePanelViewModel pagePanel)
    {
        if (ListPagePanels == null)
        {
            ListPagePanels = new List<PagePanelViewModel>();
        }

        if (pagePanel != null)
        {
            ListPagePanels.Add(pagePanel);
        }
    }


    public string TemplateText { get; set; }


    public string SetTemplateProductsCount()
    {

        if(EnumPanelTemplate == EnumPanelTemplate.ProductTriangle)
        {
            return "Triangle Template requires 3 products!";
        }
        else if(EnumPanelTemplate == EnumPanelTemplate.ProductQuard)
        {
            return "Quard Template requires 4 products!";
        }
        else if (EnumPanelTemplate == EnumPanelTemplate.ProductSixer)
        {
            return "Sixer Template requires 6 products!";
        }
        else
        {
            return "Product Template is not selected!";
        }
    }


    public void RemovePagePanel(PagePanelViewModel pagePanel)
    {
        if (ListPagePanels == null)
        {
            return;
        }

        if (ListPagePanels.Contains<PagePanelViewModel>(pagePanel))
        {
            ListPagePanels.Remove(pagePanel);
        }
    }
}
