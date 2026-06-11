using DataTransferModel;

using Main.Common.Enums;

namespace Main.Services;

public interface IPageService
{
    Task<bool> CreateNewPanel ( PanelDataModel pagePanelDataModel );

    Task<List<PostDataModel>> GetSelectProducts ( EnumCompanyName company );

    Task<List<PostDataModel>> GetSelectPosts ( EnumCompanyName company );

    Task<PageDataModel> GetPageDataModel ( int pageID );

    Task<List<PageDisplayDataModel>> GetAllPages ( EnumCompanyName company );
}

