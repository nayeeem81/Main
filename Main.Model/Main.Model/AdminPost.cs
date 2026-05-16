using Main.Common.EnumClasses;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class AdminPost : BaseEntity
    {
        public AdminPost() {

        }

        [Key]
        public int AdminPostID { get; set; }

        [Required]
        public EnumPostType PostType { get; set; }
        
        [Required]
        public string PosterName { get; set; }

        [Required]
        public string Title { get; set; }


        //Nullable
        [StringLength(11)]
        public string? PosterContactNumber { get; set; } 

        public string? WebsiteUrl { get; set; }

        [MaxLength(1000)]
        public string? ShortNote { get; set; }

        public string? SearchTag { get; set; }

        // References
        public int UserID { get; set; } 

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual ICollection<AdminImageFile> ListAdminImageFiles { get; set; } = new HashSet<AdminImageFile>();

        public virtual ICollection<AdminPostComment> ListAdminPostComments { get; set; } = new HashSet<AdminPostComment>();

    }
}
