using DataTransferModel;

using Domain.Model;

using Main.Common.Enums;
using Main.Common.HelperRelated;

namespace Main.Services.Extensions;

public static class PageServiceMapping
{
    public static Panel CreatePanelEntity (PanelDataModel panelDataModel)
    {
        Panel panelEntity = new( panelDataModel.PageID, panelDataModel.PanelTemplate,
            panelDataModel.PanelTitle );

        panelEntity.CreateBaseData (panelDataModel.BaseDataModel);

        return panelEntity;
    }

    public static List<Post> CreateListPostEntity (PanelDataModel panelDataModel)
    {
        List<Post>  listPosts = new();
        Post post;
        int order = 1;
        panelDataModel.ListPosts.ForEach (postDataModel =>
        {
            post =
            new Post (postDataModel.EnumPostType,postDataModel.Price,postDataModel.RootID)
            {
                FileContent = postDataModel.ImageFileContent,
                Title = postDataModel.PostTitle,
                Order = order
            };

            post.CreateBaseData (panelDataModel.BaseDataModel);
            listPosts.Add (post);

            order += 1;
        });

        return listPosts;
    }


    public static List<PostDataModel> GetPostDataModels (List<Product> listProducts)
    {
        if ( listProducts == null )
        {
            return new List<PostDataModel> ();
        }

        List<PostDataModel> listPanelPostDataModel
        = new();

        PostDataModel panelPostDataModel;

        int id = 1;

        listProducts.ForEach (productEntity =>
        {
            productEntity
            .ListImageFiles
            .ToList ()
            .ForEach (file =>
            {
                panelPostDataModel = new PostDataModel
                {
                    CategoryID = productEntity.CategoryID,
                    PanelPostID = id,
                    RootID = productEntity.ProductID,
                    EnumPostType = productEntity.PostType,
                    Price = productEntity.Price,
                    PostTitle = productEntity.ProductName,
                    ImageFileContent = file.ImageFileContent,
                    ImageFileID = file.ProductImageFileID
                };

                id += 1;

                listPanelPostDataModel.Add (panelPostDataModel);
            });
        });

        return listPanelPostDataModel.ToList ();
    }

    public static PageDataModel MapPageDataModel (Page pageEntity)
    {
        if ( pageEntity != null )
        {
            var listPanels = pageEntity.ListPanels;

            PageDataModel pageDataModel = new( );

            List<PanelDataModel> listPanelDataModel
                = new();

            PanelDataModel panelDataModel;

            PostDataModel postDataModel;

            listPanels.ToList<Panel> ().OrderBy (a => a.PanelPosition).ToList ().ForEach (panel =>
            {
                panelDataModel = new PanelDataModel
                {
                    PanelID = panel.PanelID,
                    PanelTemplate = panel.PanelTemplate,
                    PanelTitle = panel.PanelTitle,
                    PageID = panel.PageID,
                    PanelPosition = panel.PanelPosition
                };

                panel.ListPosts.ToList ().ForEach (panelPost =>
                {
                    postDataModel = new PostDataModel
                    {
                        PanelPostID = panelPost.PostID,
                        PostTitle = panelPost.Title ?? "",
                        Price = panelPost.Price,
                        ImageFileContent = panelPost.FileContent,
                        PostOrder = panelPost.Order,
                        PageID = panelDataModel.PageID
                    };


                    panelDataModel.CreatePost (postDataModel);
                });

                int actualCount  = panelDataModel.ListPosts.Count;

                EnumIsValidTemplate validTemplate =
                ValidationRelated.IsValidTemplate ( actualCount, panelDataModel.PanelTemplate );

                if ( validTemplate == EnumIsValidTemplate.ExactMatchValid )
                {
                    pageDataModel.CreatePanel (panelDataModel);
                }

                if ( validTemplate == EnumIsValidTemplate.GreaterMatchValid )
                {
                    int count = ValidationRelated.GetPostCount(panelDataModel.PanelTemplate);
                    List<PostDataModel> listPosts =
                    panelDataModel.ListPosts.Take(count).ToList();

                    panelDataModel.ListPosts = listPosts;

                    pageDataModel.CreatePanel (panelDataModel);
                }
            });

            return pageDataModel;
        }

        return new PageDataModel ();
    }

    public static List<PostDataModel> GetPostDataModels (List<AdminPost> listAdminPosts)
    {
        if ( listAdminPosts == null )
        {
            return new List<PostDataModel> ();
        }

        List<PostDataModel> listPostDataModels = new();

        PostDataModel postDataModel;

        int id = 1;

        listAdminPosts.ForEach (adminPostEntity =>
        {
            adminPostEntity.ListAdminImageFiles.ToList ().ForEach (fileEntity =>
            {
                postDataModel = new PostDataModel
                {
                    PanelPostID = id,
                    RootID = adminPostEntity.AdminPostID,
                    EnumPostType = adminPostEntity.PostType,
                    PostTitle = adminPostEntity.Title,
                    ImageFileContent = fileEntity.ImageFileContent,
                    WebsiteUrl = adminPostEntity.WebsiteUrl
                };

                listPostDataModels.Add (postDataModel);

                id += 1;
            });
        });

        return listPostDataModels.ToList ();
    }
}
