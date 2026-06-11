using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class Post: BaseEntity
{
    public Post ( )
    {
        Price = 0;
        WebsiteUrl = "www.dummy.com";
    }

    public Post (
        EnumPostType enumPostType,
        int rootId,
        int panelId )
    {
        EnumPostType = enumPostType;
        RootID = rootId;
        PanelID = panelId;
        Price = 0;
        WebsiteUrl = "www.dummy.com";
    }

    [Key]
    public int PostID
    {
        get; set;
    }

    [Required]
    public int Order
    {
        get; set;
    }

    [Required]
    public EnumPostType EnumPostType
    {
        get; set;
    }

    // Product or Admin Post
    [Required]
    public int RootID
    {
        get; set;
    }

    [Required]
    public byte[] FileContent
    {
        get; set;
    }

    [Required]
    public string Title
    {
        get; set;
    }

    [Required]
    [DataType ( DataType.Currency )]
    public decimal Price
    {
        get; set;
    }

    [Required, DataType ( DataType.Url )]
    public string WebsiteUrl
    {
        get; set;
    }

    [Required]
    public int PanelID
    {
        get; set;
    }


    [ForeignKey ( "PanelID" )]
    public virtual Panel PagePanel
    {
        get; set;
    }
}
