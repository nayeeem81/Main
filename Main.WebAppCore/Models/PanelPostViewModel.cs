using Main.Common;
using Main.Common.EnumClasses;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModel;

public class PanelPostViewModel : BaseViewModel
{
    public PanelPostViewModel() {
        AV_Category = DropDownSelectListItem.GetCategoryList(StaticAppSettings.CategoryFor);
    }


    public PanelPostViewModel(EnumPostType enumPostType, int rootId, int imageId, int order)
    {
        AV_Category = DropDownSelectListItem.GetCategoryList(StaticAppSettings.CategoryFor);
        EnumPostType = enumPostType;
        RootID = rootId;
        ImageFileID = imageId;
        ImageOrderID = order;
    }
  
    public int PanelPostID { get; set; }

   
    public EnumPostType EnumPostType { get; set; }

    
    public int RootID { get; set; } // Admin or Company (Key) of EnumPostType


    public int ImageOrderID { get; set; }


    public int ImageFileID { get; set; }


    public int CategoryID { get; set; }


    public string GetTextCategory()
    {
        var CategoryText = string.Empty;

        AV_Category.ToList().ForEach(x =>
        {
            if (x.Value == CategoryID.ToString())
            {
                CategoryText = x.Text;
            }
        });

        return CategoryText;
    }


    public IEnumerable<SelectListItem> AV_Category { get; set; }


    public string CategoryName { 
        get
        {
            return GetTextCategory();
        } 
    }


    public byte[]? ImageFileContent { get; set; } = null;

    
    public string PostTitle { get; set; }


    public string? PostDescription { get; set; }


    public decimal? Price { get; set; }


    public string? WebsiteUrl { get; set; }

   
    public int PanelID { get; set; }


    public PagePanelViewModel? PagePanel { get; set; }


    public int PageID { get; set; }


    public int ImageArea { get; set; }
}
