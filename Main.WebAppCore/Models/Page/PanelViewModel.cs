using Main.Common.Enums;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel;

public class PanelViewModel: BaseViewModel
{
    public PanelViewModel ( )
    {
        ListSelectPosts = new List<PostSelectViewModel> ( );
        ListPosts = new List<PostViewModel> ( );
        AV_PanelTemplate = DropDownListItems.GetPanelTempletList ( );
    }

    public PanelViewModel ( EnumPanelTemplate enumPanelTemplate )
    {
        ListPosts = new List<PostViewModel> ( );
        AV_PanelTemplate = DropDownListItems.GetPanelTempletList ( );
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

    public List<PostSelectViewModel> ListSelectPosts
    {
        get; set;
    }

    public List<PostViewModel> ListPosts
    {
        get; set;
    }

    public void CreatePanelPost ( PostViewModel postViewModel )
    {
        if ( ListPosts == null )
        {
            ListPosts = new List<PostViewModel> ( );
        }

        if ( postViewModel != null )
        {
            ListPosts.Add ( postViewModel );
        }
    }
}
