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

    public async Task<bool> CreateNewPanel ( PanelDataModel pagePanelDataModel )
    {
        Panel panelEntity = PageServiceMapping.CreatePanelEntity ( pagePanelDataModel );

        List<Post> listPostEntity = PageServiceMapping.CreateListPostEntity ( pagePanelDataModel );

        var result = await _pageRepository.UpdatePage ( panelEntity, listPostEntity );

        return result;
    }

    public async Task<List<PostDataModel>> GetSelectProducts ( EnumCompanyName company )
    {
        List<Product> listProducts = await _productImageRepository.GetSelectProducts ( company );
        List<PostDataModel> listPanelPostDataModel = PageServiceMapping.GetPostDataModels( listProducts );

        return listPanelPostDataModel;
    }

    public async Task<List<PostDataModel>> GetSelectPosts ( EnumCompanyName company )
    {
        List<AdminPost> listAdminPosts = await _adminPostImageRepository.GetSelectAdminPosts( company );

        List<PostDataModel> listPanelPostDataModel
                                            = PageServiceMapping.GetPostDataModels   ( listAdminPosts );

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

    public async Task<bool> UpdatePanelsOrderAsync
        ( List<PanelPositionDataModel> listPanelPositions )
    {

        List<(int PanelId,int PageId, int PanelPosition, EnumCompanyName company,
            EnumCountry country)> listTuplePanelPositions = new List<(int, int, int, EnumCompanyName, EnumCountry)>();

        listTuplePanelPositions.ForEach ( panelPosition =>
        {
            listTuplePanelPositions.Add (
                (panelPosition.PanelId,panelPosition.PageId,
                panelPosition.PanelPosition,panelPosition.company,panelPosition.country) );
        } );

        bool result = await _pageRepository.UpdatePanelsOrderAsync ( listTuplePanelPositions );

        return result;
    }

    public async Task<bool> DeletePanelAsync ( int panelId,int pageId,EnumCompanyName company,EnumCountry country )
    {
        bool result = await _pageRepository.DeletePanelAsync ( panelId, pageId, company, country );

        return result;
    }
}

