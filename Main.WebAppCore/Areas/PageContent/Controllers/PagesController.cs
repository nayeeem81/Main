using DataTransferModel;

using Main.Common.Enums;
using Main.Common.Model;
using Main.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

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
        PanelViewModel pagePanelViewModel = new PanelViewModel();



        List<PostDataModel> listSelectProductsDataModel =
            await _pageService.GetSelectProducts(_userContext.EnumCompanyName);

        pagePanelViewModel.ListSelectPosts =
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

            List<PostDataModel> listUserSelectedPosts = new List<PostDataModel>();

            listUserSelectedPosts = listReferencePosts.Where ( obj =>
            {
                return model.Numbers.Contains ( obj.PanelPostID );
            } ).ToList ( );

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


    public async Task<IActionResult> EditPageContent ( int id )
    {
        PageDataModel pagePanelDataModel = await _pageService.GetPageDataModel(id);

        PageViewModel pageViewModel = PageMapping.MapPageViewModel ( pagePanelDataModel );

        return View ( pageViewModel.ListPagePanels.ToList ( ) );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> UpdatePositions ( [FromBody] List<PanelPositionDataModel> listPanelPositionDataModel )
    {
        if ( listPanelPositionDataModel.Count == 0 )
        {
            return BadRequest ( new
            {
                success = false,
                error = "Payload is empty or invalid!"
            } );
        }

        int pageId = listPanelPositionDataModel.Last().PageID;

        try
        {
            foreach ( var item in listPanelPositionDataModel )
            {
                if ( item == null )
                {
                    return Json ( new
                    {
                        success = false,
                        error = "Validation failed!"
                    } );
                }
            }

            BaseDataModel baseDataModel = _userContext.GetUpdateBaseDataModel ();

            bool result = await _pageService.UpdatePanelsOrderAsync ( listPanelPositionDataModel, baseDataModel, pageId );


            return Json ( new
            {
                success = result
            } );
        }
        catch ( Exception ex )
        {
            _logger.LogWarning ( ex,"Error updating panel positions" );
            return Json ( new
            {
                success = false,
                error = ex.Message
            } );
        }
    }



    [HttpDelete]
    [ValidateAntiForgeryToken]
    [Authorize ( Roles = "Admin" )]
    public async Task<IActionResult> DeletePanel ( int panelId,int pageId )
    {
        try
        {
            bool result = await _pageService.DeletePanelAsync ( panelId );


            return Json ( new
            {
                success = result,
                receivedUrl = Url.Action ( "EditPageContent","Pages",new
                {
                    Area = "PageContent",
                    id = pageId
                } )
            } );
        }
        catch ( Exception ex )
        {
            return Json ( new
            {
                success = false,
                error = ex.Message
            } );
        }
    }
}