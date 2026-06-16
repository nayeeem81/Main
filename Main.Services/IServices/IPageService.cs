using DataTransferModel;

using Main.Common.Enums;
using Main.Common.Model;

namespace Main.Services;

public interface IPageService
{
    Task<bool> CreateNewPanel ( PanelDataModel pagePanelDataModel );

    Task<List<PostDataModel>> GetSelectProducts ( EnumCompanyName company );

    Task<List<PostDataModel>> GetSelectPosts ( EnumCompanyName company );

    Task<PageDataModel> GetPageDataModel ( int pageID );

    Task<List<PageDisplayDataModel>> GetAllPages ( EnumCompanyName company );

    Task<bool> UpdatePanelsOrderAsync ( List<PanelPositionDataModel> listPanelPositionDataModel,BaseDataModel baseDataModel,int pageId );

    Task<bool> DeletePanelAsync ( int panelId );
}

