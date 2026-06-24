using DataTransferModel;

using Main.Common.Enums;

namespace WebAppCore.ViewModel.Extensions;

public static class AdminPostMapping
{
    public static AdminPostDataModel MapNewDataModel ( AdminPostViewModel adminPostViewModel )
    {
        AdminPostDataModel adminPostDataModel = new AdminPostDataModel
        {
            AdminPostID = adminPostViewModel.AdminPostID ?? 0,
            PostTitle = adminPostViewModel.PostTitle,
            PosterName = adminPostViewModel.PosterName,
            PosterContactNumber = adminPostViewModel.PosterContactNumber,
            WebsiteUrl = adminPostViewModel.WebsiteUrl,
            ShortNote = adminPostViewModel.ShortNote,
            SearchTag = adminPostViewModel.SearchTag,
            PostType = adminPostViewModel.PostType
        };

        return adminPostDataModel;
    }

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
            PostType = adminPostViewModel.PostType
        };

        return adminPostDataModel;
    }

    public static List<AdminImageFileDataModel> MapAdminFileDataModel ( AdminPostViewModel adminFileViewModel )
    {
        List<AdminImageFileDataModel> listAdminImageFileDataModel = new List<AdminImageFileDataModel>();

        adminFileViewModel.ListAdminPostFileImages.ForEach ( fileViewModel =>
        {
            listAdminImageFileDataModel.Add ( new AdminImageFileDataModel ( fileViewModel.FileContent ) );
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
        adminPostViewModel.PostType = adminPostDatatModel.PostType;
        adminPostViewModel.SearchTag = adminPostDatatModel.SearchTag;
        adminPostViewModel.ShortNote = adminPostDatatModel.ShortNote;
        adminPostViewModel.DisplayPostType = EnumDescription.GetDescription ( adminPostDatatModel.PostType );
    }

    public static List<AdminPostDisplayViewModel> MapAdminPostDisplayViewModelList ( List<AdminPostDisplayModel> adminPostDisplayModelList,string company )
    {
        var displayViewModels = new List<AdminPostDisplayViewModel>();

        foreach ( var model in adminPostDisplayModelList )
        {
            displayViewModels.Add ( new AdminPostDisplayViewModel
            {
                AdminPostID = model.AdminPostID,
                PosterName = model.PosterName,
                PostTitle = model.PostTitle,
                DiispayPostType = EnumDescription.GetDescription ( model.PostType ),
                DisplayCompanyName = company
            } );
        }

        return displayViewModels;
    }

    public static List<ImageFile> MapAdminImageFileViewModelList ( List<AdminImageFileDataModel> adminImageFileList )
    {
        var imageFileViewModels = new List<ImageFile>();

        foreach ( var model in adminImageFileList )
        {
            imageFileViewModels.Add ( new ImageFile
            {
                FileContent = model.ImageFileContent,
                FileID = model.AdminImageFileID,
                PostID = model.AdminPostID
            } );
        }

        return imageFileViewModels;
    }
}
