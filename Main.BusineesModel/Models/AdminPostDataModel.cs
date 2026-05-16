using Main.Common.Model;

namespace BusinessModel;

public class AdminPostDataModel : BaseDataModel
{
    public AdminPostDataModel()
    {
        ModelBase = new ModelBase();
    }

    public int AdminPostID { get; set; }
  
    public string PosterName { get; set; }
    
    public string PostTitle { get; set; }

    public string? PosterContactNumber { get; set; }
   
    public string? WebsiteUrl { get; set; }

    public string? ShortNote { get; set; }

    public string? SearchTag { get; set; }

    public List<AdminImageFileDataModel> ListAdminPostFileImages { get; set; } = new List<AdminImageFileDataModel>();

    public List<AdminPostCommentDataModel> ListAdminPostComments { get; set; } = new List<AdminPostCommentDataModel>();

    public int UserID { get; set; }
   
    public int PostTypeID { get; set; }

    public string? EnumAdminPostTypeDescription { get; set; } 

    public ModelBase ModelBase { get; set; }

}
