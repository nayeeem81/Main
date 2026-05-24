using DataTransferModel;
using Domain.Model;
using IRepository;
using Main.Common.Enums;

namespace Main.Services;

public class PageService: IPageService
{

    public readonly IPageRepository _pageRepository;

    public PageService ( IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company )
    {

        List<Page> listPageEntity = await _pageRepository.GetAllPages ( company );

        List<PageDataModel> listPageDataModel = new List <PageDataModel> ();

        listPageEntity.ForEach ( pageEntity => 
                                    listPageDataModel
                                    .Add ( new PageDataModel (
                                            pageEntity.PageID,
                                            pageEntity.EnumPublicPage,
                                            company) 
                                    ) 
                                );

        return listPageDataModel.ToList ( );  
    }            
}

