using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class ProductImageFile : BaseEntity
    {
        public ProductImageFile()
        {
        }

        public ProductImageFile(byte[] imageFileContent) {
            ImageFileContent = imageFileContent;
        }

        [Key]
        public int ProductImageFileID { get; set; }

        [Required]
        public byte[] ImageFileContent { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

    }
}
