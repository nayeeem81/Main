using Main.Common.EnumClasses;

namespace BusinessModel;

public class PagePanelDataModel: BaseDataModel
{
    public PagePanelDataModel()
    {
        ListPanelPosts = new List<PanelPostDataModel>();
    }

    public PagePanelDataModel(EnumPanelTemplate enumPanelTemplate)
    {
        ListPanelPosts = new List<PanelPostDataModel>();
        
        PanelTemplate = enumPanelTemplate;
    }


    public int PanelID { get; set; }

    public int PageID { get; set; }

    public int PanelOrderID { get; set; }

    public string PanelTitle { get; set; }

    public EnumPanelTemplate PanelTemplate { get; set; }

    public List<PanelPostDataModel> ListSelectProducts { get; set; }

    public List<PanelPostDataModel> ListPanelPosts { get; set; }

    public void CreatePanelPost(PanelPostDataModel panelPost)
    {
        if (ListPanelPosts == null)
        {
            ListPanelPosts = new List<PanelPostDataModel>();
        }

        if (panelPost != null)
        {
            ListPanelPosts.Add(panelPost);
        }
    }
}
