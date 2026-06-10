using DataTransferModel;

using Main.Common.Enums;
using Main.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAppCore.Helper;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

public class LocalModel
{
    public LocalModel ( )
    {
    }

    public string? PanelTitle
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

        List<PanelPostDataModel> listSelectProductsDataModel =
            await _pageService.GetSelectProducts(AppSettings.Current.EnumCompanyName);

        pagePanelViewModel.ListSelectProducts =
            PageMapping.MapSelectPostViewModel ( listSelectProductsDataModel
            ,AppSettings.Current.EnumCategoryFor
            ,AppSettings.Current.EnumCurrency );


        return View ( pagePanelViewModel );
    }


    [HttpPost]
    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> SaveNewProductPanel ( [FromBody] LocalModel model )
    {
        if ( model == null )
            return BadRequest ( "Invalid data." );

        try
        {
            PagePanelDataModel pagePanelDataModel = new PagePanelDataModel();
            pagePanelDataModel.PanelTitle = model.PanelTitle;
            pagePanelDataModel.PageID = model.PageID;
            pagePanelDataModel.PanelTemplate
                = ( EnumPanelTemplate ) model.TemplateTypeID;

            pagePanelDataModel.BaseDataModel
                = _userContext.GetCreateBaseDataModel ( );



            List<PanelPostDataModel> listReferencePosts
                = await _pageService.GetSelectProducts( _userContext.EnumCompanyName );

            List<PanelPostDataModel> listUserSelectedPosts = listReferencePosts.Where(obj => model.Numbers.Contains(obj.PanelPostID)).ToList();

            listUserSelectedPosts.ForEach ( selectedPost =>
            {
                selectedPost.BaseDataModel = _userContext.GetCreateBaseDataModel ( );
            } );


            pagePanelDataModel.ListPanelPosts = listUserSelectedPosts;

            bool result  = await _pageService.CreateNewPanel ( pagePanelDataModel );


            return Json ( new
            {
                success = result,
                receivedUrl = Url.Action ( "Index","Pages",new
                {
                    Area = "PageContent"
                } )
            } );

        }
        catch ( Exception ex )
        {
            return BadRequest ( ex.Message );
        }
    }


    public async Task<IActionResult> PreviewPageContent ( int id )
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pagePanelDataModel );

        return View ( pageViewModel.ListPagePanels.ToList ( ) );
    }
}