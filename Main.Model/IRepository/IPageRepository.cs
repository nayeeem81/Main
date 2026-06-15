using Domain.Model;

using Main.Common.Enums;
using Main.Common.Model;

namespace IRepository;

public interface IPageRepository
{
    Task<List<Page>> GetAllPages ( EnumCompanyName company );

    Task<Page> GetSinglePage ( int id );

    Task<bool> PageExists ( int id );

    Task<bool> UpdatePage ( Panel panel,List<Post> listPosts );

    Task<bool> UpdatePanelsOrderAsync (
        List<(int PageID,int PanelID,int PanelPosition)> listPanelPositions,BaseDataModel baseDataModel );

    Task<bool> DeletePanelAsync (
        int panelId,int pageId,
        EnumCompanyName company,
        EnumCountry country );
}