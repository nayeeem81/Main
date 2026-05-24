using DataTransferModel;
using Domain.Model;
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

    public async Task<bool> CreateNewPanel 
    (
          PagePanelDataModel pagePanelDataModel, 
          List<PanelPostDataModel> listPanelPostDataModel
    )
    {
        PagePanel panelEntity = new PagePanel();

        panelEntity.PanelTemplate = pagePanelDataModel.PanelTemplate;

        panelEntity.PanelTitle = pagePanelDataModel.PanelTitle;

        panelEntity.CreateBaseData ( pagePanelDataModel.BaseDataModel );


        listPanelPostDataModel.ForEach ( objPost => {

            PanelPost panelPost = new PanelPost ( )
            {
                ImageFileContent = objPost.ImageFileContent,
                Price = objPost.Price,
                PostTitle = objPost.PostTitle,
                PostDescription = objPost.PostDescription
            };

            panelPost.CreateBaseData ( objPost.BaseDataModel );

            panelEntity.CreatePanelPost ( panelPost );

        } );

        Page pageEntity = await _pageRepository.GetSinglePage       
            (pagePanelDataModel.PageID);

        PageContent objPageCotentEntity = pageEntity != null

                    ? pageEntity.GetNewOrExistingPageContent
                                    (pagePanelDataModel.PageID, pagePanelDataModel.BaseDataModel)
                    : new PageContent();


        objPageCotentEntity.Page = null;

        objPageCotentEntity.CreatePagePanel ( panelEntity );


        if ( pageEntity != null )
        {
            pageEntity.SavePageContent ( objPageCotentEntity );
        }

        var result = await _pageRepository.UpdatePage ( pageEntity );

        return result;
    }

    

    public async Task<List<PanelPostDataModel>> GetSelectProducts ( 
                                                EnumCompanyName company )
    {

        return await _productImageRepository.GetSelectProducts ( company );
    }

    public async Task<PageDataModel> GetPageDataModel ( int pageID )
    {
        Page pageEntity =  await _pageRepository.GetSinglePage ( pageID );

        if ( pageEntity != null )
        {
            var pageContent = pageEntity.ListPageContents.First<PageContent>();

            var listPanels = pageContent.ListPagePanels.ToList();

            PageDataModel pageDataModel = new PageDataModel( );

            List<PagePanelDataModel> listPanelDataModel 
                = new List<PagePanelDataModel>();

            PagePanelDataModel panelDataModel;

            PanelPostDataModel panelPostDataModel;

            listPanels.ForEach ( panel =>
            {
                panelDataModel = new PagePanelDataModel ( );

                panelDataModel.PanelID = panel.PanelID;
                panelDataModel.PanelTemplate = panel.PanelTemplate;
                panelDataModel.PanelTitle = panel.PanelTitle;

                panel
                .ListPanelPosts
                .ToList ( )
                .ForEach ( panelPost =>
                {
                    panelPostDataModel = new PanelPostDataModel ( );

                    panelPostDataModel.PanelPostID = panelPost.PanelPostID;
                    panelPostDataModel.PostTitle = panelPost.PostTitle;
                    panelPostDataModel.Price = panelPost.Price;
                    panelPostDataModel.PostDescription = panelPost.PostDescription;
                    panelPostDataModel.ImageFileContent = panelPost.ImageFileContent;
                    panelPostDataModel.PostOrder = panelPost.PostOrder;

                    panelDataModel.CreatePanelPost ( panelPostDataModel );
                } );

                pageDataModel.CreatePageContent ( panelDataModel );
            } );

            return pageDataModel;
        }

        return new PageDataModel ( );
    }
}
                                                             
