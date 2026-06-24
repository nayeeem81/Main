using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;
namespace Domain.Model;

public class Page: BaseEntity
{
    public Page ( )
    {
        ListPanels = new List<Panel> ( );
    }

    public Page ( EnumPublicPage enumPublicPage )
    {
        ListPanels = new List<Panel> ( );
        EnumPublicPage = enumPublicPage;
    }

    public Page ( EnumPublicPage enumPublicPage,string tenantId,bool isSeed )
    {
        ListPanels = new List<Panel> ( );
        EnumPublicPage = enumPublicPage;
        TenantId = tenantId;
        ModifiedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedDate = DateTime.MinValue;
        ModifiedDate = DateTime.MinValue;

        HostCountry = EnumCountry.Bangladesh;
        IsActive = true;

        IdentityUserId = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
    }


    [Key]
    public int PageID
    {
        get; set;
    }


    [Required]
    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }

    public virtual ICollection<Panel> ListPanels { get; set; } = new HashSet<Panel> ( );


    public void CreatePanel ( Panel panel )
    {
        if ( ListPanels == null )
        {
            ListPanels = new List<Panel> ( );
        }

        if ( panel != null )
        {
            if ( ListPanels.Any<Panel> ( ) )
            {
                int position = ListPanels.OrderBy ( a => a.PanelPosition ).Last().PanelPosition;
                panel.PanelPosition = position + 1;
                panel.PageID = PageID;
            }
            else
            {
                panel.PanelPosition = 1;
                panel.PageID = PageID;
            }

            ListPanels.Add ( panel );
        }
    }
}
