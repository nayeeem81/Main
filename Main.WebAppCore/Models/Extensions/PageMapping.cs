using DataTransferModel;

using Main.Common.Enums;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel.Extensions;

public static class PageMapping
{
    public static List<PageDisplayViewModel> PageDisplayMapping ( List<PageDisplayDataModel> listPageDisplayDataModel )
    {
        List<PageDisplayViewModel> listPageDisplayViewModels = new List<PageDisplayViewModel> ();

        PageDisplayViewModel pageDisplayViewModel;

        listPageDisplayDataModel.ForEach ( dataModel =>
        {
            pageDisplayViewModel = new PageDisplayViewModel ( );

            pageDisplayViewModel.PageID = dataModel.PageID;

            pageDisplayViewModel.PageName = ListEnum.GetPageDescription ( dataModel.EnumPublicPage );

            pageDisplayViewModel.CompanyName =
                                            ListEnum.GetCompanyDescription ( dataModel.EnumCompanyName );

            listPageDisplayViewModels.Add ( pageDisplayViewModel );
        } );

        return listPageDisplayViewModels;
    }

    public static List<PostSelectViewModel> MapSelectPostViewModel ( List<PostDataModel> listSelectProductsDataModels,EnumCategoryFor categoryFor,EnumCurrency currency )
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
                dataModel.RootID,dataModel.ImageFileID,dataModel.ImageOrderID );

            postSelectViewModel.ImageFileContent = dataModel.ImageFileContent;
            postSelectViewModel.CategoryName = SelectListItemDropDown.GetCategoryText ( categoryFor,
                dataModel.CategoryID );

            postSelectViewModel.PostTitle = dataModel.PostTitle;
            postSelectViewModel.Price = dataModel.Price;
            postSelectViewModel.Currency = ListEnum.GetCurrencyDescription ( currency );
            postSelectViewModel.PanelPostID = dataModel.PanelPostID;

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
            panelViewModel = new PanelViewModel ( );
            panelViewModel.PageID = pagePanelDataModel.PageID;
            panelViewModel.PanelID = pagePanelDataModel.PanelID;
            panelViewModel.PanelTitle = pagePanelDataModel.PanelTitle ?? "";
            panelViewModel.PanelTemplate = pagePanelDataModel.PanelTemplate;
            panelViewModel.PageName = ListEnum.GetPageDescription ( pageDataModel.EnumPublicPage );
            panelViewModel.PanelPosition = pagePanelDataModel.PanelPosition;

            PostViewModel postViewModel;

            pagePanelDataModel.ListPosts.ForEach ( panelPostDataModel =>
            {
                postViewModel = new PostViewModel ( );
                postViewModel.PanelPostID = panelPostDataModel.PanelPostID;
                postViewModel.ImageFileContent = panelPostDataModel.ImageFileContent;
                postViewModel.ImageFileID = panelPostDataModel.ImageFileID;
                postViewModel.Price = panelPostDataModel.Price;
                postViewModel.PageID = panelViewModel.PageID;
                postViewModel.CategoryID = panelPostDataModel.CategoryID;
                postViewModel.PanelID = panelViewModel.PanelID;

                panelViewModel.CreatePanelPost ( postViewModel );

            } );

            listPanelViewModel.Add ( panelViewModel );

        } );

        PageViewModel pageViewModel = new PageViewModel ( );

        pageViewModel.ListPagePanels =
            listPanelViewModel.ToList<PanelViewModel> ( ).OrderBy ( a => a.PanelPosition ).ToList ( );

        return pageViewModel;
    }
}