using DataTransferModel;
using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppCore.Helper;

namespace WebAppCore.ViewModel;

public class AdminPostViewModel : BaseViewModel
{
    public AdminPostViewModel()
    {
        AV_PostType = SelectListItemDropDown.GetAdminPostTypeList ( );

        ListAdminPostFileImages = new List<ImageFile> ( );
    }

    public int? AdminPostID { get; set; }

    
    [Display(Name = "Poster Name")]
    [Required(ErrorMessage = "Poster name is required!")]
    public string PosterName { get; set; }

    
    
    [Display(Name = "Post Title")]
    [Required(ErrorMessage = "Post title is required!")]
    public string PostTitle { get; set; }


    [Required ( ErrorMessage = "Contact number is required!" )]
    [Display(Name = "Contact Number")]
    public string? PosterContactNumber { get; set; }


    [Display(Name = "Website Link")]
    public string? WebsiteUrl { get; set; }


    [Display(Name = "Short Note")]
    [StringLength(4000, ErrorMessage = "Short Note cannot exceed 4000 letters!")]
    public string? ShortNote { get; set; }


    [Display(Name = "Tags (Comma Seperated)")]
    public string? SearchTag { get; set; }  

    public List<ImageFile> ListAdminPostFileImages { get; set; } = new List<ImageFile> ();

    
    [Display(Name = "Post Type")]
    [Required(ErrorMessage = "Post type is required!")]
    public EnumPostType PostType { get; set; }

    public IEnumerable<SelectListItem> AV_PostType { get; set; }


    [Display(Name = "Post Type")]
    public string? DisplayPostType { get; set; } 
}
