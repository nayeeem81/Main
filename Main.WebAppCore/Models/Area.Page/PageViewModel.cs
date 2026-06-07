using Main.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class PageViewModel : BaseViewModel
{
    public PageViewModel() {
        ListPagePanels = new List<PagePanelViewModel>();
    }


    public PageViewModel(int id, 
                        EnumPublicPage enumPublicPage, 
                        EnumCompanyName enumCompany)
    {
        ListPagePanels = new List<PagePanelViewModel>();

        PageID = id;
        
        EnumPublicPage = enumPublicPage;

        PageName = ListEnum.GetPageDescription ( enumPublicPage );

        CompanyName = ListEnum.GetCompanyDescription(enumCompany);
    }


    public int PageID { get; set; }


    public EnumPublicPage EnumPublicPage { get; set; }


    [Display(Name = "Configurable Page Name")]
    public string? PublicPageName { get; set; }


    [Display(Name = "Company Name")]
    public string? CompanyName { get; set; }


    public List<PagePanelViewModel> ListPagePanels { get; set; }


    public void CreatePageContent(PagePanelViewModel pageContentViveModel)
    {
        if (ListPagePanels == null)
        {
            ListPagePanels = new List<PagePanelViewModel>();
        }

        if (pageContentViveModel != null)
        {
            ListPagePanels.Add(pageContentViveModel);
        }
    }
}
