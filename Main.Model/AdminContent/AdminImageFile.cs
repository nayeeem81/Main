using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class AdminImageFile
    {
        public AdminImageFile()
        {
            
        }

        public AdminImageFile ( byte[] ImageContent )
        {            
            ImageFileContent = ImageContent;
        }

        [Key]
        public int AdminImageFileID { get; set; }


        [Required]
        public byte[] ImageFileContent { get; set; }



        // Foreign key to AdminPost
        public int AdminPostID { get; set; }


        [ForeignKey("AdminPostID")]
        public virtual AdminPost AdminPost { get; set; }
    }
}
