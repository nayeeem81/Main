using System.ComponentModel.DataAnnotations;

namespace WebAppCore.ViewModel;

public class ProductDisplayViewModel
{
    public ProductDisplayViewModel ( )
    {
    }

    public int ProductID { get; set; }


    [Display(Name = "Product Category")]
    public string DisplayCategory { get; set; }


    [Display ( Name = "Sub Category" )]
    public string DisplaySubCategory
    {
        get; set;
    }

    [Display(Name = "Product Name")]
    public string ProductName { get; set; }


    [Display(Name = "Price (Taka)")]
    [DataType(DataType.Currency)]
    public decimal UnitPrice { get; set; }
}






























































