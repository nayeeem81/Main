using DataTransferModel;
using Main.Common.Enums;

namespace Main.Services;

public interface IPagePanelService
{
    Task<bool> CreateNewPanel
    (
          PagePanelDataModel pagePanelDataModel,List<PanelPostDataModel> listPanelPostDataModel
    );

    Task<List<PanelPostDataModel>> GetSelectProducts ( EnumCompanyName company );

    Task<PageDataModel> GetPageDataModel ( int pageID );
}

