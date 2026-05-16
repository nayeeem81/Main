using Main.Common.EnumClasses;

using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class PageViewModel : BaseViewModel
{
    public PageViewModel() {
       
        ListPagePanels = new List<PagePanelViewModel>();
    }


    public PageViewModel(int id, EnumPublicPage enumPublicPage, EnumCompanyName enumCompany)
    {
        ListPagePanels = new List<PagePanelViewModel>();

        PageID = id;
        
        EnumPublicPage = enumPublicPage;

        PageName = ListEnumObjects.GetPageDescription(enumPublicPage);

        CompanyName = ListEnumObjects.GetCompanyDescription(enumCompany);
    }


    public int PageID { get; set; }


    public EnumPublicPage EnumPublicPage { get; set; }


    [Display(Name = "Configurable Page Name")]
    public string PageName { get; set; }


    [Display(Name = "Company Name")]
    public string? CompanyName { get; set; }


    public List<PagePanelViewModel> ListPagePanels { get; set; }


    public void CreatePageContent(PagePanelViewModel pageContentVm)
    {
        if (ListPagePanels == null)
        {
            ListPagePanels = new List<PagePanelViewModel>();
        }

        if (pageContentVm != null)
        {
            ListPagePanels.Add(pageContentVm);
        }
    }
}
