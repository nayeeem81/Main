using DataTransferModel;

using Main.Common.Enums;

using WebAppCore.Helper;
namespace WebAppCore.ViewModel.Extensions;

public static class PageMapping
{
    public static List<PageDisplayViewModel> PageDisplayMapping ( List<PageDisplayDataModel> listPageDisplayDataModel,string company )
    {
        List<PageDisplayViewModel> listPageDisplayViewModels = new List<PageDisplayViewModel> ();

        PageDisplayViewModel pageDisplayViewModel;

        listPageDisplayDataModel.ForEach ( dataModel =>
        {
            pageDisplayViewModel = new PageDisplayViewModel
            {
                PageID = dataModel.PageID,

                PageName = ListEnum.GetPageDescription ( dataModel.EnumPublicPage ),

                CompanyName = company
            };

            listPageDisplayViewModels.Add ( pageDisplayViewModel );
        } );

        return listPageDisplayViewModels;
    }

    public static List<PostSelectViewModel> MapSelectPostViewModel ( List<PostDataModel> listSelectProductsDataModels,EnumStoreType categoryFor,EnumCurrency currency )
    {
        if ( listSelectProductsDataModels == null )
        {
            return new List<PostSelectViewModel> ( );
        }

        List<PostSelectViewModel> listPostSelectViewModels =
            new List<PostSelectViewModel> ();

        PostSelectViewModel postSelectViewModel;

        listSelectProductsDataModels.ForEach ( dataModel =>
        {
            postSelectViewModel = new PostSelectViewModel ( dataModel.EnumPostType,
                dataModel.RootID,dataModel.ImageFileID,dataModel.ImageOrderID )
            {
                ImageFileContent = dataModel.ImageFileContent,
                CategoryName = DropDownListItems.GetCategoryText ( categoryFor,
                    dataModel.CategoryID ),

                PostTitle = dataModel.PostTitle,
                Price = dataModel.Price,
                Currency = ListEnum.GetCurrencyDescription ( currency ),
                PanelPostID = dataModel.PanelPostID
            };

            listPostSelectViewModels.Add ( postSelectViewModel );

        } );

        return listPostSelectViewModels;
    }

    public static PageViewModel MapPageViewModel ( PageDataModel pageDataModel )
    {
        List<PanelViewModel>  listPanelViewModel = new List<PanelViewModel> ();

        PanelViewModel panelViewModel;

        pageDataModel.ListPanels.ForEach ( pagePanelDataModel =>
        {
            panelViewModel = new PanelViewModel
            {
                PageID = pagePanelDataModel.PageID,
                PanelID = pagePanelDataModel.PanelID,
                PanelTitle = pagePanelDataModel.PanelTitle ?? "",
                PanelTemplate = pagePanelDataModel.PanelTemplate,
                PageName = ListEnum.GetPageDescription ( pageDataModel.EnumPublicPage ),
                PanelPosition = pagePanelDataModel.PanelPosition
            };

            PostViewModel postViewModel;

            pagePanelDataModel.ListPosts.ForEach ( panelPostDataModel =>
            {
                postViewModel = new PostViewModel
                {
                    PanelPostID = panelPostDataModel.PanelPostID,
                    ImageFileContent = panelPostDataModel.ImageFileContent,
                    ImageFileID = panelPostDataModel.ImageFileID,
                    Price = panelPostDataModel.Price,
                    PageID = panelViewModel.PageID,
                    CategoryID = panelPostDataModel.CategoryID,
                    PanelID = panelViewModel.PanelID
                };

                panelViewModel.CreatePanelPost ( postViewModel );

            } );

            listPanelViewModel.Add ( panelViewModel );

        } );

        PageViewModel pageViewModel = new PageViewModel
        {
            ListPagePanels =
                listPanelViewModel.ToList<PanelViewModel> ( ).OrderBy ( a => a.PanelPosition ).ToList ( )
        };

        return pageViewModel;
    }
}