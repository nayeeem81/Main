using Main.Common.Model;

namespace IRepository;

public interface IPanelRepository
{
    Task<bool> UpdatePanelsOrderAsync (
        List<(int PageID,int PanelID,int PanelPosition)> listPanelPositions,BaseDataModel baseDataModel );

    Task<bool> DeletePanelAsync ( int panelId );
}