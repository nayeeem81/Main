using DataTransferModel;

using Main.Common.Enums;
using Main.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

public class LocalModel
{
    public LocalModel ( )
    {
        Numbers = new List<int> ( );
    }

    public string PanelTitle
    {
        get; set;
    }

    public int TemplateTypeID
    {
        get; set;
    }

    public int PageID
    {
        get; set;
    }

    public List<int> Numbers
    {
        get; set;
    }
}


[Area ( "PageContent" )]
[Authorize ( Roles = "Admin" )]
public class PagesController: BaseController
{
    private readonly IPageService _pageService;
    private readonly IUserContext _userContext;

    public PagesController ( IPageService pageDataService,
                           IUserContext userContext )
    {
        _pageService = pageDataService;
        _userContext = userContext;
    }


    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> Index ( )
    {
        try
        {
            EnumCompanyName company = _userContext.EnumCompanyName;

            List<PageDisplayDataModel> listPageDataodel = await _pageService.GetAllPages(company);

            List<PageDisplayViewModel> listPageViewModel = PageMapping.PageDisplayMapping(listPageDataodel);

            return View ( listPageViewModel );

        }
        catch ( Exception ex )
        {
            {
                return BadRequest ( ex.Message );
            }
        }
    }


    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> NewProductPanel ( int id )
    {
        PagePanelViewModel pagePanelViewModel = new PagePanelViewModel();

        pagePanelViewModel.PageID = id;

        List<PostDataModel> listSelectProductsDataModel =
            await _pageService.GetSelectProducts(_userContext.EnumCompanyName);

        pagePanelViewModel.ListSelectProducts =
            PageMapping.MapSelectPostViewModel ( listSelectProductsDataModel
                                                ,_userContext.EnumCategoryFor
                                                ,_userContext.EnumCurrency );


        return View ( pagePanelViewModel );
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> SaveNewProductPanel ( LocalModel model )
    {
        if ( model == null )
        {
            return Json ( new
            {
                success = false,
                message = "model is null"
            } );
        }

        //try
        //{
        PanelDataModel pagePanelDataModel = new PanelDataModel();
        pagePanelDataModel.PanelTitle = model.PanelTitle;
        pagePanelDataModel.PageID = model.PageID;
        pagePanelDataModel.PanelTemplate
            = ( EnumPanelTemplate ) model.TemplateTypeID;

        pagePanelDataModel.SetBaseDataModel ( _userContext.GetCreateBaseDataModel ( ) );

        List<PostDataModel> listReferencePosts
                = await _pageService.GetSelectProducts( _userContext.EnumCompanyName );

        List<PostDataModel> listUserSelectedPosts = listReferencePosts.Where(obj =>
        {
            return model.Numbers.Contains(obj.PanelPostID);
        } ).ToList();

        listUserSelectedPosts.ForEach ( selectedPost =>
        {
            selectedPost.SetBaseDataModel ( _userContext.GetCreateBaseDataModel ( ) );
        } );


        pagePanelDataModel.ListPosts = listUserSelectedPosts;

        bool result  = await _pageService.CreateNewPanel ( pagePanelDataModel );


        return Json ( new
        {
            success = result,
            receivedUrl = Url.Action ( "Index","Pages",new
            {
                Area = "PageContent"
            } )
        } );

        //}
        //catch ( Exception ex )
        //{
        //    throw ex;
        //    //return Json ( new
        //    //{
        //    //    success = false,
        //    //    message = ex.Message
        //    //} );
        //}
    }


    public async Task<IActionResult> PreviewPageContent ( int id )
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pagePanelDataModel );

        return View ( pageViewModel.ListPagePanels.ToList ( ) );
    }
}