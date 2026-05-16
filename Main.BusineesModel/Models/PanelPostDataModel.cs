using Main.Common.EnumClasses;

namespace BusinessModel;

public class PanelPostDataModel : BaseDataModel
{
    public PanelPostDataModel() {
    }

    public PanelPostDataModel(EnumPostType enumPostType, int rootId, int imageId, int order)
    {
        EnumPostType = enumPostType;
        RootID = rootId;
        ImageFileID = imageId;
        ImageOrderID = order;
    }
  
    public int PanelPostID { get; set; }
   
    public EnumPostType EnumPostType { get; set; }
    
    public int RootID { get; set; } 

    public int ImageOrderID { get; set; }

    public int ImageFileID { get; set; }

    public int CategoryID { get; set; }

    public byte[]? ImageFileContent { get; set; } = null;
    
    public string PostTitle { get; set; }

    public string? PostDescription { get; set; }

    public decimal? Price { get; set; }

    public string? WebsiteUrl { get; set; }
   
    public int PanelID { get; set; }

    public PagePanelDataModel? PagePanel { get; set; }

    public int PageID { get; set; }

    public int ImageArea { get; set; }
}
