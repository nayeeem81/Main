using DataTransferModel;
using Main.Common.Enums;

namespace Main.Services;

public interface IPagePanelService
{
    Task<bool> CreateNewPanels (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
        );

    Task<List<PanelPostDataModel>>
        GetSelectProducts ( EnumCompanyName company );

    Task<PageDataModel> GetPanelList ( int pageID );
}

