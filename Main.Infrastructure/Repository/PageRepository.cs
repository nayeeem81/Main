using BusinessModel;

using Data;
using Entity.Model;
using IRepository;
using Main.Common.Enums;
using Main.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PageRepository: IPageRepository
{
    private readonly BussinessAppDbContext _context;

    public PageRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company )
    {
        var listPages = await _context.Pages
            .Where ( a => a.HostCompanyName == company )
            .ToListAsync<Page> ( );

        List<PageDataModel> objListPageDM 
                            = new List<PageDataModel> ();

        listPages.ForEach ( 
            pageEntity => objListPageDM
                     .Add ( new PageDataModel ( 
                         pageEntity.PageID,
                         pageEntity.EnumPublicPage,
                         company ) ) );


        return objListPageDM.OrderBy ( a => a.PageName ).ToList ( );

    }

    public async Task<PageDataModel?> GetSinglePage ( int id ) 
    {
        var paeEntity = await _context.Pages
                                      .FirstOrDefaultAsync 
                                       (m => m.PageID == id); 

        if ( paeEntity != null )
        {
            var pageContent = paeEntity
                            .ListPageContents
                            .First<PageContent>();

            var listPanels = pageContent.ListPagePanels.ToList();

            PageDataModel objPageDataModel = new PageDataModel ( );

            List<PagePanelDataModel> panelListDM 
                = new List<PagePanelDataModel>();

            PagePanelDataModel panelDM;
            PanelPostDataModel postDM;

            listPanels.ForEach ( panel =>
            {
                panelDM = new PagePanelDataModel ( );

                panelDM.PanelID = panel.PanelID;
                panelDM.PanelTemplate = panel.PanelTemplate;
                panelDM.PanelTitle = panel.PanelTitle;

                panel.ListPanelPosts.ToList ( ).ForEach ( post =>
                {
                    postDM = new PanelPostDataModel ( );

                    postDM.PanelPostID = post.PanelPostID;
                    postDM.PostTitle = post.PostTitle;
                    postDM.Price = post.Price;
                    postDM.PostDescription = post.PostDescription;
                    postDM.ImageFileContent = post.ImageFileContent;
                    postDM.ImageOrderID = post.PostOrder;

                    panelDM.CreatePanelPost ( postDM );
                } );

                objPageDataModel.CreatePageContent ( panelDM );
            } );

            return objPageDataModel;
        }

        return new PageDataModel ( );
    }

    //public async Task<bool> UpdatePage ( Page page )
    //{
    //    if ( page == null )
    //    {
    //        return false;
    //    }

    //    _context.Update ( page );

    //    int result = await _context.SaveChangesAsync();

    //    return result > 0;
    //}


    public async Task<bool> PageExists ( int id )
    {
        return await _context.Pages.AnyAsync ( e => e.PageID == id );
    }

    public async Task<bool> CreateNewContent 
    ( 
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase 
    )
    {
        PagePanel panelEntity = new PagePanel();

        panelEntity.PanelTemplate = ( EnumPanelTemplate ) model.TemplateTypeID;

        panelEntity.PanelTitle = model.PanelTitle;

        panelEntity.CreateBaseData ( modelBase );

        listUserSelectedPosts.ForEach ( obj => {

            PanelPost panelPost = new PanelPost ( 
                obj.EnumPostType, 
                obj.RootID, 
                obj.PageID )
            {
                ImageFileContent = obj.ImageFileContent,
                Price = obj.Price,
                PostTitle = obj.PostTitle,
                PostDescription = obj.PostDescription
            };

            panelPost.CreateBaseData ( modelBase );

            panelEntity.CreatePanelPost ( panelPost );

        } );

        var objPageEntity = await _context
                            .Pages
                            .FirstOrDefaultAsync
                            (m => m.PageID ==  model.PageID);

        PageContent objPageCotentEntity = objPageEntity != null
                    
                    ? objPageEntity.GetNewOrExistingPageContent
                                    (model.PageID, modelBase)
                    : new PageContent();

        objPageCotentEntity.Page = null;

        objPageCotentEntity.CreatePagePanel ( panelEntity );


        if ( objPageEntity != null )
        {
            objPageEntity.SavePageContent ( objPageCotentEntity );

            _context.Pages.Update( objPageEntity );
        }

        int result = await _context.SaveChangesAsync ( );

        return result > 0;
    }


    //public async Task<PagePanelDataModel> GetContentPanel ( int paneId)
    //{

    //    var pagePanel = await _context.PagePanels.FirstOrDefaultAsync<PagePanel> ( a => a.PanelID == paneId );

    //    if ( pagePanel != null ) 
    //        return pagePanel;

    //    return new PagePanel ( );
    //}
}

