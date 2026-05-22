using BusinessModel;
using Main.Common.Enums;

namespace Main.Services.IServices;

public interface IQueryPagePanelService
{
    Task<List<PanelPostDataModel>>
        GetSelectProducts ( EnumCompanyName company );

    Task<PageDataModel> GetPanelList ( int pageID );

}

