using Main.Common.Enums;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel;

public class PagePanelViewModel: BaseViewModel
{
    public PagePanelViewModel ( )
    {
        ListSelectProducts = new List<PanelPostSelectViewModel> ( );
        ListPanelPosts = new List<PanelPostViewModel> ( );
        AV_PanelTemplate = SelectListItemDropDown.GetPanelTempletList ( );
    }

    public PagePanelViewModel ( EnumPanelTemplate enumPanelTemplate )
    {
        ListPanelPosts = new List<PanelPostViewModel> ( );
        AV_PanelTemplate = SelectListItemDropDown.GetPanelTempletList ( );
        PanelTemplate = enumPanelTemplate;
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


    [Display ( Name = "Panel Title" )]
    [Required ( ErrorMessage = "Panel title is required!" )]
    public string PanelTitle
    {
        get; set;
    }


    [Display ( Name = "Panel Template" )]
    [Required ( ErrorMessage = "Select a template!" )]
    public EnumPanelTemplate PanelTemplate
    {
        get; set;
    }

    public IEnumerable<SelectListItem> AV_PanelTemplate
    {
        get; set;
    }

    public List<PanelPostSelectViewModel> ListSelectProducts
    {
        get; set;
    }

    public List<PanelPostViewModel> ListPanelPosts
    {
        get; set;
    }

    public void CreatePanelPost ( PanelPostViewModel panelPost )
    {
        if ( ListPanelPosts == null )
        {
            ListPanelPosts = new List<PanelPostViewModel> ( );
        }

        if ( panelPost != null )
        {
            ListPanelPosts.Add ( panelPost );
        }
    }
}
