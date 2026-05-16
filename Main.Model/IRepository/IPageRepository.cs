using Main.Common.EnumClasses;
using Main.Model;
namespace IRepository;

public interface IPageRepository            
{
    Task<List<Page>> GetAllPages();

    Task<List<Page>> GetAllPages(EnumCompanyName company);

    Task<Page?> GetSinglePage(int? id);

    Task<bool> UpdatePage(Page page);

    Task<bool> PageExists(int id);

    Task<bool> CreateNewContent ( Page pageContent );

    Task<PagePanel> GetContentPanel ( int pageId );
}
