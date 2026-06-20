using DataTransferModel;

using Main.Common.Model;
namespace Main.Services;

public interface IPageService
{
    Task<bool> CreateNewPanel ( PanelDataModel pagePanelDataModel );

    Task<List<PostDataModel>> GetSelectProducts ( );

    Task<List<PostDataModel>> GetSelectPosts ( );

    Task<PageDataModel> GetPageDataModel ( int pageID );

    Task<List<PageDisplayDataModel>> GetAllPages ( string company );

    Task<bool> UpdatePanelsOrderAsync ( List<PanelPositionDataModel> listPanelPositionDataModel,BaseDataModel baseDataModel,int pageId );

    Task<bool> DeletePanelAsync ( int panelId );
}

