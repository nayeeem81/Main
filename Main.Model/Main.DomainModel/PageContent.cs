using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class PageContent : BaseEntity
{
    public PageContent()
    {
        ListPagePanels = new List<PagePanel>();
    }


    public PageContent(int pageId)
    {       
        ListPagePanels = new List<PagePanel>();

        PageID = pageId;
    }


    [Key]
    public int PageContentID { get; set; }


    [Required]
    public int PageID { get; set; }


    [ForeignKey("PageID")]
    public virtual Page? Page { get; set; } 


    public virtual ICollection<PagePanel> ListPagePanels { get; set; }


    public void CreatePagePanel(PagePanel pagePanel)
    {
        if (ListPagePanels == null)
        {
            ListPagePanels = new List<PagePanel>();
        }

        if(pagePanel != null)
        {
            var count = 0;
            count = ListPagePanels.Count;
            if ( count > 0 )
            {
                pagePanel.PanelPosition = count + 1;
            }
            else
            {
                pagePanel.PanelPosition = 1;
            }

            ListPagePanels.Add ( pagePanel );
        }
    }


    public void RemovePagePanel(PagePanel pagePanel)
    {
        if (ListPagePanels == null) {
            return;
        }

        if(ListPagePanels.Contains<PagePanel>(pagePanel))
        {
            ListPagePanels.Remove(pagePanel);
        }
    }

}
