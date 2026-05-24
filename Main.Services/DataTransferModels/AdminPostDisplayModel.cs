using Main.Common.Enums;

namespace DataTransferModel;

public class AdminPostDisplayModel
{
    public AdminPostDisplayModel ( )
    {
    }

    public int AdminPostID { get; set; }
  
    public string PosterName { get; set; }
    
    public string PostTitle { get; set; }

    public int UserID { get; set; }
   
    public int PostTypeID { get; set; }

    public string DiispayPostType { get; set; } 

    public EnumCompanyName HostCompanyName { get; set; }

    public string DiispayCompanyName
    {
        get; set;
    }
}
