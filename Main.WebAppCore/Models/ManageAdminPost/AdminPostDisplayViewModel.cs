using Main.Common.Enums;
using System.ComponentModel;

namespace WebAppCore.ViewModel;

public class AdminPostDisplayViewModel
{
    public AdminPostDisplayViewModel ( )
    {
    }

    public int AdminPostID { get; set; }


    [DisplayName ( "Poster Name" )]
    public string PosterName { get; set; }


    [DisplayName ( "Post Title" )]
    public string PostTitle { get; set; }


    public int UserID { get; set; }

   
    public int PostTypeID { get; set; }


    [DisplayName ( "Post Type" )]
    public string DiispayPostType { get; set; }


    [DisplayName ( "Host Company" )]
    public EnumCompanyName HostCompanyName { get; set; }


    [DisplayName ( "Host Company" )]
    public string DiispayCompanyName
    {
        get; set;
    }
}
