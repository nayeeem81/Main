using Domain.Model;

using Main.Common.Enums;

namespace IRepository;

public interface IPageRepository
{
    Task<List<Page>> GetAllPages ( EnumCompanyName company );

    Task<Page> GetSinglePage ( int id );

    Task<bool> PageExists ( int id );

    Task<bool> UpdatePage ( Panel panel,List<Post> listPosts );

    Task<bool> UpdatePanelsOrderAsync (
        List<(int PageId,int PanelId,int PanelPosition,
            EnumCompanyName company,EnumCountry country)> listPanelPositions );

    Task<bool> DeletePanelAsync (
        int panelId,int pageId,
        EnumCompanyName company,
        EnumCountry country );
}