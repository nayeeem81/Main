using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class PageDisplayViewModel
{
    public PageDisplayViewModel ( )
    {
    }

    public int PageID
    {
        get; set;
    }


    [Display ( Name = "Page Name" )]
    public string PageName
    {
        get; set;
    }


    [Display ( Name = "Company Name" )]
    public string CompanyName
    {
        get; set;
    }


    [Display ( Name = "Add Product | Ads (PANEL)" )]
    public string AddPanelButtons
    {
        get; set;
    }


    [Display ( Name = "Edit | View Page" )]
    public string ViewEditButtons
    {
        get; set;
    }
}
