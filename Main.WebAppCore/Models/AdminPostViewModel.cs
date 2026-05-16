using Main.Common;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel;

public class AdminPostViewModel : BaseViewModel
{
    public AdminPostViewModel()
    {
        AV_PostType = DropDownSelectListItem.GetPostTypeList();
    }

    public int AdminPostID { get; set; }

    
    [Display(Name = "Poster Name")]
    [Required(ErrorMessage = "Poster name is required!")]
    public string PosterName { get; set; }

    
    
    [Display(Name = "Post Title")]
    [Required(ErrorMessage = "Post Title is required!")]
    public string PostTitle { get; set; }


    [Display(Name = "Contact Number")]
    public string? PosterContactNumber { get; set; }


    [Display(Name = "Website Link")]
    public string? WebsiteUrl { get; set; }


    [Display(Name = "Short Note")]
    [StringLength(4000, ErrorMessage = "Short Note cannot exceed 4000 letters!")]
    public string? ShortNote { get; set; }


    [Display(Name = "Tags (Comma Seperated)")]
    public string? SearchTag { get; set; }


    //Files and Comments
    public List<AdminImageFileViewModel> ListAdminPostFileImages { get; set; } = new List<AdminImageFileViewModel>();

    public List<AdminPostCommentViewModel> ListAdminPostComments { get; set; } = new List<AdminPostCommentViewModel>();

    // References
    [Required]
    public int UserID { get; set; }


    
    [Display(Name = "Post Types")]
    [Required(ErrorMessage = "Post Type is required!")]
    public int PostTypeID { get; set; }

    public IEnumerable<SelectListItem> AV_PostType { get; set; }

    [Display(Name = "Post Type")]
    public string? EnumAdminPostTypeDescription { get; set; } 
}
