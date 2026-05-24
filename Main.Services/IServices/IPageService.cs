using DataTransferModel;
using Main.Common.Enums;

namespace Main.Services;

public interface IPageService
{
    Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company );
}

