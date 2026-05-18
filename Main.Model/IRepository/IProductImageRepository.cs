using BusinessModel;
using Main.Common.Enums;

namespace IRepository;

public interface IProductImageRepository
{
    Task<List<PanelPostDataModel>> GetSelectProducts ( EnumCompanyName company );
}
