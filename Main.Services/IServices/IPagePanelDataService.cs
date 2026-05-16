using BusinessModel;
using Main.Common;
using Main.Common.Model;

namespace IService;

public interface IPagePanelDataService
{
    Task<List<PanelPostDataModel>> GetSelectProducts(EnumCompanyName company);

    Task<List<PanelPostDataModel>> GetSelectAdminPosts(EnumCompanyName company);

    Task<int> CreateNewPanels (
            LocalModel model,

            EnumCompanyName enumCompany,

            List<PanelPostDataModel> listUserSelectedPosts,

            ModelBase modelBase

    );

    Task<PagePanelDataModel> GetPreviewPanel ( int panelId );

    Task<List<PagePanelDataModel>> GetPanelList ( int pageID );
}

