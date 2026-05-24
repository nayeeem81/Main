using Main.Common;
using Main.Common.Enums;
using Main.Common.Model;

namespace DataTransferModel
{
    public class PageDataModel : ModelBase
    {
        public PageDataModel() {
           
            ListPagePanels = new List<PagePanelDataModel>();
        }

        public PageDataModel(int id, EnumPublicPage enumPublicPage, EnumCompanyName enumCompany)
        {
            ListPagePanels = new List<PagePanelDataModel>();
            PageID = id;
            EnumPublicPage = enumPublicPage;
        }

        public int PageID { get; set; }

        public EnumPublicPage EnumPublicPage { get; set; }

        public string PageName { get; set; }

        public string? CompanyName { get; set; }

        public List<PagePanelDataModel> ListPagePanels { get; set; }

        public void CreatePageContent(PagePanelDataModel pageContentDM)
        {
            if (ListPagePanels == null)
            {
                ListPagePanels = new List<PagePanelDataModel>();
            }

            if (pageContentDM != null)
            {
                ListPagePanels.Add(pageContentDM);
            }
        }
    }
}
