using Main.Common.EnumClasses;
using Main.Model;
namespace IRepository;

public interface IProductImageRepository
{
    Task<List<Product>> GetSelectProducts(EnumCompanyName company);
}
