using BusinessModel;
using Main.Common.Enums;
using Main.Common.Model;

namespace IRepository;

public interface IPageRepository            
{
    Task<List<PageDataModel>> GetAllPages(EnumCompanyName company);

    Task<PageDataModel?> GetSinglePage(int id);

    //Task<bool> UpdatePage(PageDataModel page);

    Task<bool> PageExists(int id);

    Task<bool> CreateNewContent
    (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
    );

    //Task<PagePanelDataModel> GetContentPanel ( int pageId );
}
