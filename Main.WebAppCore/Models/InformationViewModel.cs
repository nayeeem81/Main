using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class InformationViewModel : BaseViewModel
{
    public InformationViewModel()
    {
        ListPages = new List<PageViewModel>();
       
    }

    public List<PageViewModel> ListPages { get; set; }

    [Display(Name = "Add Product | Advertisement (PANEL)")]
    public string? AddPanelButtons { get; set; }

    [Display(Name = "Edit | View Page")]
    public string? ViewEditButtons { get; set; }

    [Display(Name = "Company")]
    public string? CompanyLabel { get; set; }

    [Display(Name = "Page")]
    public string? PageLabel { get; set; }
}
