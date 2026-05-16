using BusinessModel;

using IRepository;

using IService;                   

using Main.Common;
using Main.Common.Model;
using Main.Model;

namespace Main.Service;

    public class PagePanelDataService: IPagePanelDataService
    {

        public readonly IProductImageRepository _productImageRepository;
        public readonly IAdminPostImageRepository _adminPostsImageRepository;
        public readonly IPageRepository _pageRepository;

        public PagePanelDataService ( IProductImageRepository productImageRepository,
                                    IAdminPostImageRepository adminPostsImageRepository,
                                    IPageRepository pageRepository )
        {
            _productImageRepository = productImageRepository;
            _adminPostsImageRepository = adminPostsImageRepository;
            _pageRepository = pageRepository;
        }

        public async Task<List<PanelPostDataModel>> GetSelectProducts ( EnumCompanyName company )
        {

            var list = await _productImageRepository.GetSelectProducts(company);


            if ( list == null )
            {
                return new List<PanelPostDataModel> ( );
            }


            List<PanelPostDataModel> listSelectPanelPostVM = new List<PanelPostDataModel>();


            PanelPostDataModel objVM;

            int id = 1;

            list.ForEach ( entity => {

                entity.ListImageFiles.ToList ( ).ForEach ( file =>
                {
                    

                    objVM = new PanelPostDataModel ( );


                    objVM.CategoryID = entity.CategoryID;
                    objVM.PanelPostID = id;
                    objVM.RootID = entity.ProductID;
                    objVM.EnumPostType = entity.PostType;
                    objVM.Price = entity.Price;
                    objVM.PostTitle = entity.ProductName;
                    objVM.ImageFileContent = file.ImageFileContent;
                    objVM.ImageFileID = file.ProductImageFileID;

                    id += 1;
                    listSelectPanelPostVM.Add ( objVM );
                } );

            } );

            return listSelectPanelPostVM.OrderBy ( a => a.CategoryID ).ToList ( );

        }

        public async Task<int> CreateNewPanels (

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

                PanelPost panelPost = new PanelPost ( obj.EnumPostType, obj.RootID, obj.PageID )
                {

                    ImageFileContent = obj.ImageFileContent,

                    Price = obj.Price,

                    PostTitle = obj.PostTitle,

                    PostDescription = obj.PostDescription

                };


                panelPost.CreateBaseData ( modelBase );  

                panelEntity.CreatePanelPost ( panelPost );

            } );


            Model.Page objPageEntity =  await _pageRepository.GetSinglePage ( model.PageID );

            
            PageContent objPageCotentEntity = objPageEntity.GetNewOrExistingPageContent( objPageEntity.PageID, modelBase);
    
            
            objPageCotentEntity.Page = null;

            
            objPageCotentEntity.CreatePagePanel ( panelEntity );


            objPageEntity.SavePageContent ( objPageCotentEntity );


            bool result = await _pageRepository.CreateNewContent( objPageEntity );


            int newPanelID = objPageEntity.ListPageContents
                                              .Last<PageContent>()
                                              .ListPagePanels
                                              .Last<PagePanel>().PanelID;


            return newPanelID;

    }

    public async Task<List<PanelPostDataModel>> GetSelectAdminPosts ( EnumCompanyName company )
        {

            var list = await _adminPostsImageRepository.GetSelectAdminPosts(company);


            if ( list == null )
            {
                return new List<PanelPostDataModel> ( );
            }


            List<PanelPostDataModel> listSelectPanelPostVM = new List<PanelPostDataModel>();


            PanelPostDataModel objVM;


            list.ForEach ( entity => {

                entity.ListAdminImageFiles.ToList ( ).ForEach ( file =>
                {
                    objVM = new PanelPostDataModel ( );

                    objVM.RootID = entity.AdminPostID;
                    objVM.EnumPostType = entity.PostType;
                    objVM.PostTitle = entity.Title;
                    objVM.ImageFileContent = file.ImageFileContent;

                    listSelectPanelPostVM.Add ( objVM );
                } );

            } );

            return listSelectPanelPostVM.OrderBy ( a => a.PostTitle ).ToList ( );

        }

        public async Task<PagePanelDataModel> GetPreviewPanel ( int panelId )
        {
            PagePanel panelEntity = await _pageRepository.GetContentPanel(panelId);
            
            if ( panelEntity != null )
            {
                PagePanelDataModel panelVM = new PagePanelDataModel ( );
                panelVM.PanelID = panelEntity.PanelID;
                panelVM.PanelTemplate = panelEntity.PanelTemplate;

                PanelPostDataModel postVM;

                panelEntity.ListPanelPosts.ToList ( ).ForEach ( post =>
                {
                    postVM = new PanelPostDataModel ( );
                    postVM.PanelPostID = post.PanelPostID;
                    postVM.PostTitle = (string)post.PostTitle;  
                    postVM.Price = post.Price;
                    postVM.PostDescription = (string)post.PostDescription;
                    postVM.ImageFileContent = post.ImageFileContent;
                    postVM.ImageOrderID = postVM.ImageOrderID;

                    panelVM.CreatePanelPost ( postVM );

                } );

                return panelVM;

            }

            return new PagePanelDataModel ( );
        }

        public async Task<List<PagePanelDataModel>> GetPanelList ( int pageID )
        {

            Model.Page paeEntity = await _pageRepository.GetSinglePage(pageID);


            if ( paeEntity != null )
            {
                var pageContent = paeEntity.ListPageContents.First<PageContent>();

                var listPanels = pageContent.ListPagePanels.ToList();

                List<PagePanelDataModel> panelListVM = new List<PagePanelDataModel>();

                PagePanelDataModel panelVM;

                PanelPostDataModel postVM;


                listPanels.ForEach ( panel => 
                { 

                    panelVM = new PagePanelDataModel ( );


                    panelVM.PanelID = panel.PanelID;

                    panelVM.PanelTemplate = panel.PanelTemplate;
                
                    panelVM.PanelTitle = panel.PanelTitle;

                    
                    panel.ListPanelPosts.ToList ( ).ForEach ( post =>
                    {

                        postVM = new PanelPostDataModel ( );

                        postVM.PanelPostID = post.PanelPostID;

                        postVM.PostTitle = ( string ) post.PostTitle;

                        postVM.Price = post.Price;

                        postVM.PostDescription = ( string ) post.PostDescription;

                        postVM.ImageFileContent = post.ImageFileContent;

                        postVM.ImageOrderID = post.PostOrder;

                        panelVM.CreatePanelPost ( postVM );

                    } );

                    panelListVM.Add ( panelVM );

                } );

               return panelListVM;
            }

            return new List<PagePanelDataModel> ( );
        }

    }

