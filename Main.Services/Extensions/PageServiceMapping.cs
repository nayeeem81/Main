using DataTransferModel;
using Domain.Model;

namespace Main.Services.Extensions;

public static class PageServiceMapping
{
    public static Page CreatePageContent ( Page pageEntity,PagePanelDataModel pagePanelDataModel,PagePanel panelEntity )
    {
        PageContent pageCotentEntity = pageEntity != null

                ? pageEntity.GetNewOrExistingPageContent
                                (pagePanelDataModel.PageID, pagePanelDataModel.BaseDataModel)
                : new PageContent();


        pageCotentEntity.Page = null;

        pageCotentEntity.CreatePagePanel ( panelEntity );


        if ( pageEntity != null )
        {
            pageEntity.SavePageContent ( pageCotentEntity );
            return pageEntity;
        }

        return new Page ( );
    }

    public static PagePanel CreatePanelEntity ( PagePanelDataModel pagePanelDataModel,List<PanelPostDataModel> listPanelPostDataModel )
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

        return panelEntity;
    }


    public static List<PanelPostDataModel> GetPanelPostDataModels ( List<Product> listProducts )
    {
        if ( listProducts == null )
        {
            return new List<PanelPostDataModel> ( );
        }

        List<PanelPostDataModel> listPanelPostDataModel
        = new List<PanelPostDataModel>();

        PanelPostDataModel panelPostDataModel;

        int id = 1;

        listProducts.ForEach ( productEntity =>
        {
            productEntity
            .ListImageFiles
            .ToList ( )
            .ForEach ( file =>
            {
                panelPostDataModel = new PanelPostDataModel ( );

                panelPostDataModel.CategoryID = productEntity.CategoryID;
                panelPostDataModel.PanelPostID = id;
                panelPostDataModel.RootID = productEntity.ProductID;
                panelPostDataModel.EnumPostType = productEntity.PostType;
                panelPostDataModel.Price = productEntity.Price;
                panelPostDataModel.PostTitle = productEntity.ProductName;
                panelPostDataModel.ImageFileContent = file.ImageFileContent;
                panelPostDataModel.ImageFileID = file.ProductImageFileID;

                id += 1;

                listPanelPostDataModel.Add ( panelPostDataModel );
            } );
        } );

        return listPanelPostDataModel
            .ToList ( );
    }

    public static PageDataModel MapPageDataModel ( Page pageEntity )
    {
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

    public static List<PanelPostDataModel> GetPanelPostDataModels ( List<AdminPost> listAdminPosts )
    {
        if ( listAdminPosts == null )
        {
            return new List<PanelPostDataModel> ( );
        }

        List<PanelPostDataModel> listAdminPostDataModels
            = new List<PanelPostDataModel>();

        PanelPostDataModel panelPostDataModel;

        listAdminPosts.ForEach ( adminPostEntity =>
        {
            adminPostEntity.ListAdminImageFiles.ToList ( ).ForEach ( fileEntity =>
            {
                panelPostDataModel = new PanelPostDataModel ( );

                panelPostDataModel.RootID = adminPostEntity.AdminPostID;
                panelPostDataModel.EnumPostType = adminPostEntity.PostType;
                panelPostDataModel.PostTitle = adminPostEntity.Title;
                panelPostDataModel.ImageFileContent
                                = fileEntity.ImageFileContent;

                listAdminPostDataModels.Add ( panelPostDataModel );
            } );
        } );

        return listAdminPostDataModels
            .ToList<PanelPostDataModel> ( );
    }
}
