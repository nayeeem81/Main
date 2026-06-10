using DataTransferModel;
using Main.Common.Enums;

namespace Main.Services;

public interface IPageService
{
    Task<bool> CreateNewPanel ( PagePanelDataModel pagePanelDataModel,
                                List<PanelPostDataModel> listPanelPostDataModel );

    Task<List<PanelPostDataModel>> GetSelectProducts ( EnumCompanyName company );

    Task<List<PanelPostDataModel>> GetSelectPosts ( EnumCompanyName company );
         
    Task<PageDataModel> GetPageDataModel ( int pageID );

    Task<List<PageDisplayDataModel>> GetAllPages ( EnumCompanyName company );
}

