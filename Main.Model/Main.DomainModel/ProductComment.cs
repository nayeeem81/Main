using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class ProductComment : BaseEntity
{
    public ProductComment() { }

    [Key] 
    public int ProductCommentID { get; set; }

    [Required]
    public string Comment { get; set; }

    public int ProductID { get; set; }

    [ForeignKey("ProductID")]
    public virtual Product Product { get; set; }
}
