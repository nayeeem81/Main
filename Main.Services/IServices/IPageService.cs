using DataTransferModel;
using Main.Common.Enums;

namespace Application.Service;

public interface IPageService
{
    Task<List<PageDataModel>> GetAllPages ( EnumCompanyName company );
}

