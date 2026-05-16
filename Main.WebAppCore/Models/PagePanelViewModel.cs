using Main.Common;
using Main.Common.EnumClasses;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class PagePanelViewModel: BaseViewModel
{
    public PagePanelViewModel()
    {
        ListPanelPosts = new List<PanelPostViewModel>();
        AV_PanelTemplate = DropDownSelectListItem.GetPanelTempletList();
        
    }


    public PagePanelViewModel(EnumPanelTemplate enumPanelTemplate)
    {
        ListPanelPosts = new List<PanelPostViewModel>();
        AV_PanelTemplate = DropDownSelectListItem.GetPanelTempletList();
        PanelTemplate = enumPanelTemplate;
    }


    public int PanelID { get; set; }


    public int PageID { get; set; }


    public int PanelOrderID { get; set; }


    [Display(Name = "Panel Title")]
    [Required ( ErrorMessage = "Panel Time is required!" )]
    public string PanelTitle { get; set; }


    [Display(Name = "Panel Templet")]
    [Required(ErrorMessage = "Select a Tamplate!")]
    public EnumPanelTemplate PanelTemplate { get; set; }


    public IEnumerable<SelectListItem> AV_PanelTemplate { get; set; }


    public List<PanelPostViewModel> ListSelectProducts { get; set; }


    public List<PanelPostViewModel> ListPanelPosts { get; set; }

    public void CreatePanelPost(PanelPostViewModel panelPost)
    {
        if (ListPanelPosts == null)
        {
            ListPanelPosts = new List<PanelPostViewModel>();
        }

        if (panelPost != null)
        {
            ListPanelPosts.Add(panelPost);
        }
    }
}
