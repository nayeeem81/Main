using BusinessModel;
using IRepository;
using IService;                   
using Main.Common.Enums;
using Main.Common.Model;

namespace Main.Service;

public class PagePanelCommandService: ICommandPagePanelService
{

    public readonly IProductImageRepository 
        _productImageRepository;

    public readonly IAdminPostImageRepository       _adminPostsImageRepository;

    public readonly IPageRepository _pageRepository;


    public PagePanelCommandService ( 
        IProductImageRepository productImageRepository,
        IAdminPostImageRepository adminPostsImageRepository,
        IPageRepository pageRepository )
    {
        _productImageRepository = productImageRepository;
        _adminPostsImageRepository = adminPostsImageRepository;
        _pageRepository = pageRepository;
    }

    public async Task<bool> CreateNewPanels (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
        )
    {
        return await _pageRepository.CreateNewContent (
            model,
            enumCompany,
            listUserSelectedPosts,
            modelBase );
    }
}

