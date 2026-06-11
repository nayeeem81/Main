using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class Panel: BaseEntity
{
    public Panel ( )
    {
        PanelTitle = string.Empty;
    }

    public Panel ( int pageId )
    {
        PanelTitle = string.Empty;
        PageID = pageId;
    }


    [Key]
    public int PanelID
    {
        get; set;
    }

    [Required]
    public int PanelPosition
    {
        get; set;
    }

    [Required]
    public string PanelTitle
    {
        get; set;
    }

    [Required]
    public EnumPanelTemplate PanelTemplate
    {
        get; set;
    }

    [Required]
    public int PageID
    {
        get; set;
    }


    [ForeignKey ( "PageID" )]
    public virtual Page Page
    {
        get; set;
    }

    public virtual ICollection<Post> ListPosts { get; set; } = new HashSet<Post> ( );

    public void CreatePost ( Post post )
    {
        if ( post == null )
        {
            return;
        }

        if ( ListPosts == null )
        {
            ListPosts = new List<Post> ( );
        }


        if ( ListPosts.Any ( ) )
        {
            int order = ListPosts.ToList<Post>().OrderBy (a=> a.Order).Last<Post>().Order;
            post.Order = order + 1;
            post.PanelID = 0;
        }
        else
        {
            post.Order = 1;
            post.PanelID = 0;
        }

        ListPosts.Add ( post );
    }
}
