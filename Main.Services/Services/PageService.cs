using DataTransferModel;

using Domain.Model;

using IRepository;

using Main.Common.Enums;
using Main.Services.Extensions;

namespace Main.Services;

public class PageService: IPageService
{

    public readonly IProductImageRepository _productImageRepository;

    public readonly IAdminPostImageRepository _adminPostImageRepository;

    public readonly IAdminPostImageRepository _adminPostsImageRepository;

    public readonly IPageRepository _pageRepository;

    public PageService (
        IProductImageRepository productImageRepository,
        IAdminPostImageRepository adminPostsImageRepository,
        IPageRepository pageRepository,
        IAdminPostImageRepository adminPostImageRepository )
    {
        _productImageRepository = productImageRepository;
        _adminPostsImageRepository = adminPostsImageRepository;
        _pageRepository = pageRepository;
        _adminPostImageRepository = adminPostImageRepository;
    }

    public async Task<bool> CreateNewPanel ( PagePanelDataModel pagePanelDataModel )
    {
        Page pageEntity = await _pageRepository.GetSinglePage (pagePanelDataModel.PageID);

        PagePanel panelEntity = PageServiceMapping.CreatePanelEntity(pagePanelDataModel);

        pageEntity = PageServiceMapping.CreatePageContent ( pagePanelDataModel,pageEntity,panelEntity );

        var result = await _pageRepository.UpdatePage ( pageEntity );

        return result;
    }

    public async Task<List<PanelPostDataModel>>
        GetSelectProducts ( EnumCompanyName company )
    {
        List<Product> listProducts
            = await _productImageRepository.GetSelectProducts ( company );

        List<PanelPostDataModel> listPanelPostDataModel
            = PageServiceMapping.GetPanelPostDataModels( listProducts );

        return listPanelPostDataModel;
    }

    public async Task<List<PanelPostDataModel>>
        GetSelectPosts ( EnumCompanyName company )
    {
        List<AdminPost> listPAdminPosts
            = await _adminPostImageRepository.GetSelectAdminPosts( company );

        List<PanelPostDataModel> listPanelPostDataModel
            = PageServiceMapping.GetPanelPostDataModels( listPAdminPosts );

        return listPanelPostDataModel;
    }

    public async Task<PageDataModel> GetPageDataModel ( int pageID )
    {
        Page pageEntity =  await _pageRepository.GetSinglePage ( pageID );

        PageDataModel pageDataModel =  PageServiceMapping.MapPageDataModel(pageEntity);

        return pageDataModel;
    }

    public async Task<List<PageDisplayDataModel>> GetAllPages ( EnumCompanyName company )
    {

        List<Page> listPageEntity = await _pageRepository.GetAllPages ( company );

        List<PageDisplayDataModel> listPageDisplayDataModel = new List <PageDisplayDataModel> ();

        listPageEntity.ForEach ( pageEntity =>
        {
            listPageDisplayDataModel.Add ( new PageDisplayDataModel
                                               ( pageEntity.PageID,
                                                 pageEntity.EnumPublicPage,
                                                 pageEntity.HostCompanyName ) );

        } );

        return listPageDisplayDataModel.ToList ( );
    }
}

