using DataTransferModel;

using Domain.Model;

using IRepository;

using Main.Common.Model;
using Main.Services.Extensions;
namespace Main.Services;

public class PageService: IPageService
{
    public readonly IProductRepository _productRepository;
    public readonly IAdminPostRepository _adminPostRepository;
    public readonly IPageRepository _pageRepository;
    public readonly IPanelRepository _panelRepository;

    public PageService (
        IProductRepository productRepository,
        IAdminPostRepository adminPostRepository,
        IPageRepository pageRepository,
        IPanelRepository panelRepository)
    {
        _productRepository = productRepository;
        _pageRepository = pageRepository;
        _adminPostRepository = adminPostRepository;
        _panelRepository = panelRepository;
    }

    public async Task<bool> CreateNewPanel (PanelDataModel pagePanelDataModel)
    {
        Panel panelEntity = PageServiceMapping.CreatePanelEntity ( pagePanelDataModel );

        List<Post> listPostEntity = PageServiceMapping.CreateListPostEntity ( pagePanelDataModel );

        var result = await _pageRepository.UpdatePage ( panelEntity, listPostEntity );

        return result;
    }

    public async Task<List<PostDataModel>> GetSelectProducts ()
    {
        List<Product> listProducts = await _productRepository.GetSelectProducts (  );
        List<PostDataModel> listPanelPostDataModel = PageServiceMapping.GetPostDataModels( listProducts );

        return listPanelPostDataModel;
    }

    public async Task<List<PostDataModel>> GetSelectPosts ()
    {
        List<AdminPost> listAdminPosts = await _adminPostRepository.GetSelectAdminPosts(  );

        List<PostDataModel> listPanelPostDataModel
                                            = PageServiceMapping.GetPostDataModels   ( listAdminPosts );

        return listPanelPostDataModel;
    }

    public async Task<PageDataModel> GetPageDataModel (int pageID)
    {
        Page pageEntity =  await _pageRepository.GetSinglePage ( pageID );

        PageDataModel pageDataModel =  PageServiceMapping.MapPageDataModel(pageEntity);

        return pageDataModel;
    }

    public async Task<List<PageDisplayDataModel>> GetAllPages (string company)
    {
        List<Page> listPageEntity = await _pageRepository.GetAllPages (  );

        List<PageDisplayDataModel> listPageDisplayDataModel = [];

        listPageEntity.ForEach (pageEntity =>
        {
            listPageDisplayDataModel.Add (new PageDisplayDataModel
                                               (pageEntity.PageID,
                                                pageEntity.EnumPublicPage,
                                                company));

        });

        return listPageDisplayDataModel.ToList ();
    }

    public async Task<bool> UpdatePanelsOrderAsync (List<PanelPositionDataModel> listPanelPositionDataModel,BaseDataModel baseDataModel,int pageId)
    {
        ArgumentNullException.ThrowIfNull (listPanelPositionDataModel);

        List<int> listPanelIds = listPanelPositionDataModel.Select(x => x.PanelID)
            .ToList();

        Page page = await _pageRepository.GetSinglePage ( pageId );

        List<Panel> listPanels = page.ListPanels.ToList<Panel> () ?? [];

        listPanels.Where (panel =>
        {
            return listPanelIds.Contains (panel.PanelID);
        }).ToList ().ForEach (updatePanel =>
        {
            updatePanel.ModifyBaseData (baseDataModel);
            updatePanel.PanelPosition = listPanelPositionDataModel.First (a => a.PanelID == updatePanel.PanelID).PanelPosition;

        });

        page.ModifyBaseData (baseDataModel);
        bool result = await _pageRepository.UpdatePage ( page, listPanels );
        return result;
    }

    public async Task<bool> DeletePanelAsync (int panelId)
    {
        bool result = await _panelRepository.DeletePanelAsync ( panelId );
        return result;
    }
}

