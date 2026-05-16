using BusinessModel;
using Main.Common;

namespace IService;

public interface IPageDataService
{
    Task<List<PageDataModel>> GetAllPages(EnumCompanyName company);
}

