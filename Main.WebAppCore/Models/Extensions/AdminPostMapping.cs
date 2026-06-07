using DataTransferModel;

using Main.Common.Enums;

namespace WebAppCore.ViewModel.Extensions;

public static class AdminPostMapping
{
    public static AdminPostDataModel MapAdminPostDataModel ( AdminPostViewModel adminPostViewModel )
    {
        if ( adminPostViewModel == null )
        {
            return new AdminPostDataModel ( );
        }

        AdminPostDataModel adminPostDataModel = new AdminPostDataModel()
        {
            AdminPostID = adminPostViewModel.AdminPostID ?? 0,
            PosterName = adminPostViewModel.PosterName,
            PostTitle = adminPostViewModel.PostTitle,
            PosterContactNumber = adminPostViewModel.PosterContactNumber,
            WebsiteUrl = adminPostViewModel.WebsiteUrl,
            ShortNote = adminPostViewModel.ShortNote,
            SearchTag = adminPostViewModel.SearchTag,
            PostTypeID = adminPostViewModel.PostTypeID
        };

        return adminPostDataModel;
    }

    public static List<AdminImageFileDataModel> MapAdminFileDataModel ( AdminPostViewModel adminFileViewModel )
    {
        List<AdminImageFileDataModel> listAdminImageFileDataModel = new List<AdminImageFileDataModel>();

        adminFileViewModel.ListAdminPostFileImages.ForEach ( fileViewModel =>
        {
            listAdminImageFileDataModel.Add ( new AdminImageFileDataModel ( fileViewModel.ImageFileContent ) );
        } );

        return listAdminImageFileDataModel;
    }

    public static void MapAdminPostViewModel ( AdminPostDataModel adminPostDatatModel,AdminPostViewModel adminPostViewModel )
    {
        adminPostViewModel.AdminPostID = adminPostDatatModel.AdminPostID;
        adminPostViewModel.PostTitle = adminPostDatatModel.PostTitle;
        adminPostViewModel.PosterContactNumber = adminPostDatatModel.PosterContactNumber;
        adminPostViewModel.PosterName = adminPostDatatModel.PosterName;
        adminPostViewModel.WebsiteUrl = adminPostDatatModel.WebsiteUrl;
        adminPostViewModel.PostTypeID = adminPostDatatModel.PostTypeID;
        adminPostViewModel.SearchTag = adminPostDatatModel.SearchTag;
        adminPostViewModel.ShortNote = adminPostDatatModel.ShortNote;
        adminPostViewModel.DisplayEnumAdminPostType = EnumDescription.GetDescription ( ( EnumPostType ) adminPostDatatModel.PostTypeID );
    }

    public static List<AdminPostDisplayViewModel> MapAdminPostDisplayViewModelList ( List<AdminPostDisplayModel> adminPostDisplayModelList )
    {
        var displayViewModels = new List<AdminPostDisplayViewModel>();

        foreach ( var model in adminPostDisplayModelList )
        {
            displayViewModels.Add ( new AdminPostDisplayViewModel
            {
                AdminPostID = model.AdminPostID,
                PosterName = model.PosterName,
                PostTitle = model.PostTitle,
                UserID = model.UserID,
                PostTypeID = model.PostTypeID,
                DiispayPostType = EnumDescription.GetDescription ( ( EnumPostType ) model.PostTypeID ),
                HostCompanyName = model.HostCompanyName,
                DiispayCompanyName = EnumDescription.GetDescription ( model.HostCompanyName )
            } );
        }

        return displayViewModels;
    }

    public static List<AdminImageFileViewModel> MapAdminImageFileViewModelList ( List<AdminImageFileDataModel> adminImageFileList )
    {
        var imageFileViewModels = new List<AdminImageFileViewModel>();

        foreach ( var model in adminImageFileList )
        {
            imageFileViewModels.Add ( new AdminImageFileViewModel
            {
                ImageFileContent = model.ImageFileContent,
                AdminImageFileID = model.AdminImageFileID,
                AdminPostID = model.AdminPostID
            } );
        }

        return imageFileViewModels;
    }

    public static List<AdminImageFileViewModel> GetAdminPostViewModelImages ( List<AdminImageFileDataModel> listAdminPostFileImages )
    {
        var imageFileViewModels = new List<AdminImageFileViewModel>();

        listAdminPostFileImages.ForEach ( imgFile =>
        {
            imageFileViewModels.Add ( new AdminImageFileViewModel
            {
                AdminImageFileID = imgFile.AdminImageFileID,
                ImageFileContent = imgFile.ImageFileContent
            } );
        } );

        return imageFileViewModels;
    }
}
