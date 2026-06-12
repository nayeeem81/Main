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
    private readonly ILogger<PagesController> _logger;

    public PagesController ( IPageService pageDataService,
                           IUserContext userContext,ILogger<PagesController> logger )
    {
        _pageService = pageDataService;
        _userContext = userContext;
        _logger = logger;
    }


    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> Index ( )
    {
        try
        {
            EnumCompanyName company = _userContext.EnumCompanyName;

            List<PageDisplayDataModel> listPageDataModel = await _pageService.GetAllPages(company);

            List<PageDisplayViewModel> listPageViewModel = PageMapping.PageDisplayMapping(listPageDataModel);

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



        List<PostDataModel> listSelectProductsDataModel =
            await _pageService.GetSelectProducts(_userContext.EnumCompanyName);

        pagePanelViewModel.ListSelectProducts =
            PageMapping.MapSelectPostViewModel ( listSelectProductsDataModel
                                                ,_userContext.EnumCategoryFor
                                                ,_userContext.EnumCurrency );
        pagePanelViewModel.PageID = id;
        pagePanelViewModel.PanelTitle = "";
        pagePanelViewModel.PanelTemplate = EnumPanelTemplate.ProductQuard;


        return View ( pagePanelViewModel );
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> SaveNewProductPanel ( [FromBody] LocalModel model )
    {
        _logger.LogWarning ( model.PanelTitle );
        _logger.LogWarning ( model.PageID.ToString ( ) );
        _logger.LogWarning ( model.TemplateTypeID.ToString ( ) );
        _logger.LogWarning ( model.Numbers[0].ToString ( ) );

        if ( model == null )
        {
            return Json ( new
            {
                success = false,
                message = "model is null"
            } );
        }

        try
        {
            PanelDataModel pagePanelDataModel
            = new PanelDataModel( ( EnumPanelTemplate ) model.TemplateTypeID,
                                    model.PageID, model.PanelTitle  );

            _logger.LogWarning ( pagePanelDataModel.PanelTitle );
            _logger.LogWarning ( pagePanelDataModel.PageID.ToString ( ) );
            _logger.LogWarning ( pagePanelDataModel.PanelTemplate.ToString ( ) );

            pagePanelDataModel.SetBaseDataModel ( _userContext.GetCreateBaseDataModel ( ) );

            List<PostDataModel> listReferencePosts
                = await _pageService.GetSelectProducts( _userContext.EnumCompanyName );

            _logger.LogWarning ( "listReferencePosts (count):" + listReferencePosts.Count.ToString ( ) );

            List<PostDataModel> listUserSelectedPosts = new List<PostDataModel>();

            listUserSelectedPosts = listReferencePosts.Where ( obj =>
            {
                return model.Numbers.Contains ( obj.PanelPostID );
            } ).ToList ( );

            _logger.LogWarning ( "listUserSelectedPosts (count):" + listUserSelectedPosts.Count.ToString ( ) );

            listUserSelectedPosts.ForEach ( selectedPost =>
            {
                selectedPost.SetBaseDataModel ( _userContext.GetCreateBaseDataModel ( ) );
                pagePanelDataModel.CreatePost ( selectedPost );
            } );

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
            return Json ( new
            {
                success = false,
                message = ex.Message
            } );
        }
    }


    public async Task<IActionResult> PreviewPageContent ( int id )
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pagePanelDataModel );

        return View ( pageViewModel.ListPagePanels.ToList ( ) );
    }
}