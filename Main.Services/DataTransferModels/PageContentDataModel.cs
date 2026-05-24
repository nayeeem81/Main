using Main.Common;
using Main.Common.Enums;

namespace DataTransferModel;

public class PageContentDataModel: DataModel
{

    public PageContentDataModel()
    {
        ListPagePanels = new List<PagePanelDataModel>();
       
        ListSelectProducts = new List<PanelPostDataModel>();

        ListSelectAds = new List<PanelPostDataModel>();
    }


    public PageContentDataModel(int pageId)
    {
        ListPagePanels = new List<PagePanelDataModel>();

        ListSelectProducts = new List<PanelPostDataModel>();

        ListSelectAds = new List<PanelPostDataModel>();

        PageID = pageId;
    }

    public int PageContentID { get; set; }

    public int PageID { get; set; }

    public PageDataModel? Page { get; set; }

    public string PanelTitle { get; set; }

    public EnumPanelTemplate EnumPanelTemplate
    {
        get; set;
    }

    public List<PanelPostDataModel> ListSelectProducts { get; set; }


    public List<PanelPostDataModel> ListSelectAds { get; set; }


  
    public List<PagePanelDataModel> ListPagePanels { get; set; }


    public void CreatePagePanel(PagePanelDataModel pagePanel)
    {
        if (ListPagePanels == null)
        {
            ListPagePanels = new List<PagePanelDataModel>();
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


    public void RemovePagePanel(PagePanelDataModel pagePanel)
    {
        if (ListPagePanels == null)
        {
            return;
        }

        if (ListPagePanels.Contains<PagePanelDataModel>(pagePanel))
        {
            ListPagePanels.Remove(pagePanel);
        }
    }
}
