using Domain.Model;

namespace IRepository;

public interface IPageRepository
{
    Task<List<Page>> GetAllPages ( );

    Task<Page> GetSinglePage ( int id );

    Task<bool> PageExists ( int id );

    Task<bool> UpdatePage ( Panel panel,List<Post> listPosts );

    Task<bool> UpdatePage ( Page page,List<Panel> listPanels );

}