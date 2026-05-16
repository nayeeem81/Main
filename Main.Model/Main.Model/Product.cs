using Main.Common.EnumClasses;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model
{
    public class Product : BaseEntity
    {
        public Product() {
        } 

        [Key]
        public int ProductID { get; set; }

        [Required]
        public EnumPostType PostType { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required]
        public string ProductName { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int SubCategoryID { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? SaleCommission { get; set; }

        public string? SearchTag { get; set; }

        public virtual ICollection<ProductImageFile> ListImageFiles { get; set; } = new HashSet<ProductImageFile>();

        public virtual ICollection<ProductComment> ListComments { get; set; } = new HashSet<ProductComment>();

    }
}
