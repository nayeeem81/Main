using Main.Common.EnumClasses;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class PanelPost : BaseEntity
    {
        public PanelPost() { }

        public PanelPost( EnumPostType enumPostType, int rootId, int panelId ) {
            EnumPostType = enumPostType;
            RootID = rootId;
            PanelID = panelId;
        }

        [Key]
        public int PanelPostID {  get; set; }

        public int PostOrder { get; set; }

        [Required]
        public EnumPostType EnumPostType { get; set; } = EnumPostType.Product;


        [Required]
        public int RootID { get; set; } // Admin or Company (Key) of EnumPostType

        [Required]
        public byte[]? ImageFileContent { get; set; } = null;

        [Required]
        public string? PostTitle { get; set; }

        public void CreatePanelPost(EnumPostType enumPostType, string title, byte[] imageFile)
        {
            EnumPostType = enumPostType;
            PostTitle = title;
            ImageFileContent = imageFile;
        }

        public string? PostDescription { get; set; }

        public void SetOptions( string description )
        {
            PostDescription = description;
        }

        public decimal? Price { get; set; }


        public void SetOptions(EnumPostType enumPostType, decimal price)
        {
            if(enumPostType == EnumPostType.Product)
            {
                Price = price;
            }              
        }

        public string? WebsiteUrl { get; set; }

        public void SetUrl(string url)
        {
            WebsiteUrl = url;
        }

        [Required]
        public int PanelID { get; set; }

        [ForeignKey("PanelID")]
        public virtual PagePanel? PagePanel { get; set; }
    }
}
