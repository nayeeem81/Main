using BusinessModel;
using IRepository;
using Main.Common.Enums;
using Main.Services.IServices;

namespace Main.Service;

public class PageQueryService: IQueryPageService
{

    public readonly IPageRepository _pageRepository;


    public PageQueryService ( IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }


    public async Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company ) => 
                await _pageRepository
                        .GetAllPages ( company );
                   
}

