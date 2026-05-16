using Main.Common.EnumClasses;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class PagePanel: BaseEntity
    {
        public PagePanel ( )
        {
            PanelTitle = string.Empty;
        }

        
        [Key]
        public int PanelID { get; set; }

        public int PanelPosition
        {
            get; set;
        }


        public string PanelTitle
        {
            get; set;
        }


        [Required]
        public EnumPanelTemplate PanelTemplate { get; set; }


        [Required]
        public int PageContentID { get; set; }


        [ForeignKey("PageContentID")]
        public virtual PageContent? PageContent { get; set; }

        public virtual ICollection<PanelPost> ListPanelPosts { get; set; } = new HashSet<PanelPost>();

        public void CreatePanelPost ( PanelPost panelPost )
        {
            if ( ListPanelPosts == null )
            {
                ListPanelPosts = new List<PanelPost> ( );
            }


            if ( panelPost != null )
            {
                var count = 0;

                count = ListPanelPosts.Count;

                if ( count > 0 )
                {
                    panelPost.PostOrder = count + 1;
                }
                else
                {
                    panelPost.PostOrder = 1;
                }

                ListPanelPosts.Add ( panelPost );
            }
        }

    }
}
