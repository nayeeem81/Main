using Domain.Model;
using Main.Common.Enums;

namespace IRepository;

public interface IProductImageRepository
{
    Task<List<Product>> GetSelectProducts ( EnumCompanyName company );
}
