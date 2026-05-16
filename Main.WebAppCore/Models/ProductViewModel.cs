using Main.Common;
using Main.Common.EnumClasses;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            AV_Category = DropDownSelectListItem.GetCategoryList(StaticAppSettings.CategoryFor);
            AV_SubCategory = DropDownSelectListItem.GetSubCategoryList(StaticAppSettings.CategoryFor);
            AV_PostType = DropDownSelectListItem.GetPostTypeList();
        }

        public int ProductID { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage ="Category is required!")]
        public int CategoryID { get; set; }

        public string Category { get { return GetTextCategory(); } }

        [Display(Name = "Sub Category Name")]
        [Required(ErrorMessage = "Sub Category is required!")]
        public int SubCategoryID { get; set; }


        public string SubCategory { get { return GetTextSubCategory(); } }


        public EnumPostType PostType { get; set; } = EnumPostType.Product;


        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is required!")]
        public string ProductName { get; set; }


        [StringLength(4000)]
        public string? Description { get; set; }

        [Display(Name = "Price (Taka)")]
        [Required(ErrorMessage = "Price is required!")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Sales Commission")]
        public decimal? SaleCommission { get; set; }


        public bool? IsBrandNew { get; set; }


        public int? LikeCount { get; set; }

        [Display(Name = "Search Tags (Comma Seoerated)")]
        public string? SearchTag { get; set; }


        public int? UserID { get; set; }

        [Display(Name = "Product Category")]
        public string? CategoryText { get; set; }

        [Display(Name = "Sub Category")]
        public string? SubCategoryText { get; set; }


        public string GetTextCategory()
        {
            var CategoryText = string.Empty;
            AV_Category.ToList().ForEach(x =>
            {
                if (x.Value == CategoryID.ToString())
                {
                    CategoryText = x.Text;
                }
            });
            return CategoryText;
        }


        public string GetTextSubCategory()
        {
            var SubCategoryText = string.Empty;
            AV_SubCategory.ToList().ForEach(x =>
            {
                if (x.Value == SubCategoryID.ToString())
                {
                    SubCategoryText = x.Text;
                }
            });
            return SubCategoryText;
        }

        public IEnumerable<SelectListItem> AV_Category { get; set; }

        public IEnumerable<SelectListItem> AV_SubCategory { get; set; }

        public IEnumerable<SelectListItem> AV_PostType { get; set; }

        public List<ProductFileViewModel> ImageFiles { get; set; } = new List<ProductFileViewModel>();

        public List<ProductCommentViewModel> ListComments { get; set; } = new List<ProductCommentViewModel>();

    }
}






























































