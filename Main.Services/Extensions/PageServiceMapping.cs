using DataTransferModel;

using Domain.Model;

namespace Main.Services.Extensions;

public static class PageServiceMapping
{
    public static Panel CreatePanelEntity ( PanelDataModel panelDataModel )
    {
        Panel panelEntity = new Panel(panelDataModel.PageID);

        panelEntity.PanelTemplate = panelDataModel.PanelTemplate;
        panelEntity.PanelTitle = panelDataModel.PanelTitle;
        panelEntity.CreateBaseData ( panelDataModel.BaseDataModel );

        Post panelPost;

        panelDataModel.ListPosts.ForEach ( objPost =>
        {
            panelPost = new Post ( objPost.EnumPostType,objPost.RootID,objPost.PanelPostID )
            {
                FileContent = objPost.ImageFileContent,
                Price = objPost.Price,
                Title = objPost.PostTitle
            };

            panelPost.CreateBaseData ( panelDataModel.BaseDataModel );

            panelEntity.CreatePost ( panelPost );

        } );

        return panelEntity;
    }


    public static List<PostDataModel> GetPostDataModels ( List<Product> listProducts )
    {
        if ( listProducts == null )
        {
            return new List<PostDataModel> ( );
        }

        List<PostDataModel> listPanelPostDataModel
        = new List<PostDataModel>();

        PostDataModel panelPostDataModel;

        int id = 1;

        listProducts.ForEach ( productEntity =>
        {
            productEntity
            .ListImageFiles
            .ToList ( )
            .ForEach ( file =>
            {
                panelPostDataModel = new PostDataModel ( );

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
            var listPanels = pageEntity.ListPanels.ToList();

            PageDataModel pageDataModel = new PageDataModel( );

            List<PanelDataModel> listPanelDataModel
                = new List<PanelDataModel>();

            PanelDataModel panelDataModel;

            PostDataModel postDataModel;

            listPanels.ForEach ( panel =>
            {
                panelDataModel = new PanelDataModel ( );

                panelDataModel.PanelID = panel.PanelID;
                panelDataModel.PanelTemplate = panel.PanelTemplate;
                panelDataModel.PanelTitle = panel.PanelTitle;
                panelDataModel.PageID = panel.PageID;
                panelDataModel.PanelPosition = panel.PanelPosition;

                panel.ListPosts.ToList ( ).ForEach ( panelPost =>
                {
                    postDataModel = new PostDataModel ( );

                    postDataModel.PanelPostID = panelPost.PostID;
                    postDataModel.PostTitle = panelPost.Title ?? "";
                    postDataModel.Price = panelPost.Price;
                    postDataModel.ImageFileContent = panelPost.FileContent;
                    postDataModel.PostOrder = panelPost.Order;

                    panelDataModel.CreatePost ( postDataModel );
                } );

                pageDataModel.CreatePanel ( panelDataModel );

            } );

            return pageDataModel;
        }

        return new PageDataModel ( );
    }

    public static List<PostDataModel> GetPostDataModels ( List<AdminPost> listAdminPosts )
    {
        if ( listAdminPosts == null )
        {
            return new List<PostDataModel> ( );
        }

        List<PostDataModel> listPostDataModels = new List<PostDataModel>();

        PostDataModel postDataModel;

        int id = 1;

        listAdminPosts.ForEach ( adminPostEntity =>
        {
            adminPostEntity.ListAdminImageFiles.ToList ( ).ForEach ( fileEntity =>
            {
                postDataModel = new PostDataModel ( );

                postDataModel.PanelPostID = id;
                postDataModel.RootID = adminPostEntity.AdminPostID;
                postDataModel.EnumPostType = adminPostEntity.PostType;
                postDataModel.PostTitle = adminPostEntity.Title;
                postDataModel.ImageFileContent = fileEntity.ImageFileContent;
                postDataModel.WebsiteUrl = adminPostEntity.WebsiteUrl;

                listPostDataModels.Add ( postDataModel );

                id += 1;
            } );
        } );

        return listPostDataModels
            .ToList<PostDataModel> ( );
    }
}
