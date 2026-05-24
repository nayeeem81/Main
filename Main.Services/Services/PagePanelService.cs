using DataTransferModel;
using IRepository;
using Main.Common.Enums;

namespace Main.Services;

public class PagePanelService: IPagePanelService
{

    public readonly IProductImageRepository _productImageRepository;

    public readonly IAdminPostImageRepository _adminPostsImageRepository;

    public readonly IPageRepository _pageRepository;

    public PagePanelService ( 
        IProductImageRepository productImageRepository,
        IAdminPostImageRepository adminPostsImageRepository,
        IPageRepository pageRepository )
    {
        _productImageRepository = productImageRepository;
        _adminPostsImageRepository = adminPostsImageRepository;
        _pageRepository = pageRepository;
    }

    public async Task<bool> CreateNewPanels 
    ( 
          LocalModel model, EnumCompanyName enumCompany, List<PanelPostDataModel> listUserSelectedPosts, ModelBase modelBase
    )
    {
        return await _pageRepository.CreateNewContent (
                                            model,
                                            enumCompany,
                                            listUserSelectedPosts,
                                            modelBase );
    }

    public async Task<List<PanelPostDataModel>> GetSelectProducts ( 
                                                EnumCompanyName company )
    {

        return await _productImageRepository.GetSelectProducts ( company );
    }

    public async Task<PageDataModel> GetPanelList ( int pageID )
    {
        return await _pageRepository.GetSinglePage ( pageID );
    }
}
                                                             
