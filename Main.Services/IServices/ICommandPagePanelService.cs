using BusinessModel;
using Main.Common.Enums;
using Main.Common.Model;

namespace IService;

public interface ICommandPagePanelService
{
    Task<bool> CreateNewPanels (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
        );
}

