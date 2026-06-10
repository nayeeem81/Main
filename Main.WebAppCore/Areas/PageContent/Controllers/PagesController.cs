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
    public LocalModel() { }

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
        //[Authorize ( Roles = "Admin" )]
        //public async Task<IActionResult> NewProductPanel(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    PagePanelViewModel pageContentVM = new PagePanelViewModel();

        //    pageContentVM.PageID = id.Value;

        //    List<PanelPostViewModel> listSelectProductsVM = await _pagePanelService.GetSelectProducts(StaticAppSettings.CompanyName);

        //    pageContentVM.ListSelectProducts = listSelectProductsVM;

        //    return View(pageContentVM);
        //}


        //[HttpPost]
        //[Authorize ( Roles = "Admin" )]
        //public async Task<IActionResult> SaveNewProductPanel ( [FromBody] LocalModel model )
        //{
        //    if ( model == null )
        //        return BadRequest ( "Invalid data." );

        //    try
        //    {
        //        PagePanelDataModel pagePanelDataModel = new PagePanelDataModel();
        //        pagePanelDataModel.PanelTitle = model.PanelTitle;
        //        pagePanelDataModel.PageID = model.PageID;
        //        pagePanelDataModel.PanelTemplate 
        //            = (EnumPanelTemplate) model.TemplateTypeID;

        //        pagePanelDataModel.BaseDataModel 
        //            = _userContext.GetCreateBaseDataModel();


        //        List<PanelPostDataModel> listReferencePosts 
        //            = _pagePanelService
        //            .GetSelectProducts((EnumCompanyName)_userContext.EnumCompanyName);

        //        List<PanelPostDataModel> listUserSelectedPosts = listReferencePosts.Where(obj => model.Numbers.Contains(obj.PanelPostID)).ToList();


        //          int newPanelId  = await _pagePanelService.CreateNewPanel ( pagePanelDataModel, listUserSelectedPosts);


        //        return Json ( new
        //        {
        //            success = newPanelId > 0 ? true : false,

        //            receivedUrl = Url.Action ( "Index" , "Pages", new
        //            {
        //                Area = "PageContent"
        //            } )
        //        } );

        //    }
        //    catch ( Exception ex ) 
        //    {
        //        throw ex;
        //    } 
        //}


        //public async Task<IActionResult> PreviewPageContent ( int? id )
        //{
        //    if ( id == null )
        //    {
        //        return NotFound ( );
        //    }

        //    List<PagePanelViewModel> listPagePanelVM = await _pagePanelService.GetPanelList(id.Value);

        //    return View ( listPagePanelVM );
        //}



        // GET: PageContent/Pages/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var page = await _context.Pages
        //        .FirstOrDefaultAsync(m => m.PageID == id);
        //    if (page == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(page);
        //}

        //// GET: PageContent/Pages/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PageContent/Pages/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PageID,EnumPublicPage,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,HostCompanyName,HostCountry,IsActive")] Page page)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(page);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(page);
        //}

        //// GET: PageContent/Pages/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var page = await _context.Pages.FindAsync(id);
        //    if (page == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(page);
        //}

        //// POST: PageContent/Pages/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PageID,EnumPublicPage,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,HostCompanyName,HostCountry,IsActive")] Page page)
        //{
        //    if (id != page.PageID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(page);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PageExists(page.PageID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(page);
        //}

        //// GET: PageContent/Pages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var page = await _context.Pages
        //        .FirstOrDefaultAsync(m => m.PageID == id);
        //    if (page == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(page);
        //}

        //// POST: PageContent/Pages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var page = await _context.Pages.FindAsync(id);
        //    if (page != null)
        //    {
        //        _context.Pages.Remove(page);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PageExists(int id)
        //{
        //    return _context.Pages.Any(e => e.PageID == id);
        //}
    
}