using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class AdminPostComment
    {
        public AdminPostComment()
        {
        }

        [Key]
        public int AdminPostCommentID 
        { 
            get; set;
        }


        [Required]
        public string Comment
        {
            get; set;
        }


        public int AdminPostID { get; set; }


        [ForeignKey("AdminPostID")]
        public virtual AdminPost AdminPost { get; set; }
    }
}

