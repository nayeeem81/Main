using Common;
using Data;

namespace Main.Service;

    public class PageDataService: IPageDataService
    {

        public readonly IPageRepository _pageRepository;


        public PageDataService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }


        public async Task<List<PageViewModel>> GetAllPages(EnumCompanyName company) 
        {

            List<Model.Page> listPages = await _pageRepository.GetAllPages(company);


            if (listPages == null)
            {
                return new List<PageViewModel>();
            }
                

            List<PageViewModel> listPageVM = new List<PageViewModel>();


            listPages.ForEach(pageEntity => listPageVM
                     .Add(new PageViewModel(pageEntity.PageID, 
                                            pageEntity.EnumPublicPage,
                                            company)));


            return listPageVM.OrderBy(a => a.PageName).ToList();

        }
    }

