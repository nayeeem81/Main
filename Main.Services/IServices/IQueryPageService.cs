using BusinessModel;
using Main.Common.Enums;

namespace Main.Services.IServices;

public interface IQueryPageService
{
    Task<List<PageDataModel>> GetAllPages(EnumCompanyName company);
}

