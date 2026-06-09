using DataTransferModel;
using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel;

public class ProductViewModel : BaseViewModel
{
    public ProductViewModel()
    {
        AV_Category = SelectListItemDropDown.GetCategoryList
                ( ( EnumCategoryFor ) AppSettings.Current.EnumCategoryFor );

        AV_SubCategory = SelectListItemDropDown.GetSubCategoryList
                ( ( EnumCategoryFor ) AppSettings.Current.EnumCategoryFor );

    }

    public int ProductID { get; set; }


    [Display(Name = "Product Category ")]
    [Required(ErrorMessage ="Category is required!")]
    public int CategoryID { get; set; }


    [Display(Name = "Product Sub Category")]
    [Required(ErrorMessage = "Sub category is required!")]
    public int SubCategoryID { get; set; }


    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Product Name is required!")]
    public string ProductName { get; set; }


    [Display ( Name = "Description" )]
    [StringLength(4000)]
    public string? Description { get; set; }


    [Display(Name = "Price (Taka)")]
    [Required(ErrorMessage = "Price is required!")]
    [DataType ( DataType.Currency )]
    public decimal UnitPrice { get; set; }


    [Display(Name = "Discount")]
    [DataType ( DataType.Currency )]
    public decimal Discount { get; set; }


    [Display(Name = "Sales Commission")]
    [DataType ( DataType.Currency )]
    public decimal SaleCommission { get; set; }


    public bool? IsBrandNew { get; set; }


    public int? LikeCount { get; set; }


    [Display(Name = "Search Tags (Comma Seoerated)")]
    public string? SearchTag { get; set; }


    [Display(Name = "Product Category")]
    public string? CategoryText { get; set; }


    [Display(Name = "Sub Category")]
    public string? SubCategoryText { get; set; }

    public void SetDisplaytext ( EnumCategoryFor enumCategoryFor)
    {
        CategoryText = SelectListItemDropDown.GetCategoryText ( ( EnumCategoryFor )  enumCategoryFor, CategoryID );

        SubCategoryText = SelectListItemDropDown.GetCategoryText ( ( EnumCategoryFor ) enumCategoryFor, SubCategoryID );
    }

    public IEnumerable<SelectListItem> AV_Category { get; set; }

    public IEnumerable<SelectListItem> AV_SubCategory { get; set; }

    public List<ImageFile> ImageFiles { get; set; } = new List<ImageFile>();

}






























































