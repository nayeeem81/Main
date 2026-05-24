using DataTransferModel;
using Domain.Model;
using Main.Common.Enums;

namespace WebApp.ViewModel.Extensions;

public static class AdminPostMapping
{
    public static AdminPostDataModel MapAdminPostDataModel ( AdminPost postEntity )
    {
        if ( postEntity == null )
        {
            return new AdminPostDataModel ( );
        }

        List<AdminImageFileDataModel> objDMListFiles
            = new List<AdminImageFileDataModel>();


        if ( postEntity.ListAdminImageFiles != null
                    && postEntity.ListAdminImageFiles.Count > 0 )
        {
            postEntity.ListAdminImageFiles.ToList ( ).ForEach ( fileEntity =>
            {
                AdminImageFileDataModel objFileDM = new AdminImageFileDataModel()
                {
                    AdminImageFileID = fileEntity.AdminImageFileID,
                    ImageFileContent = fileEntity.ImageFileContent,
                    AdminPostID = fileEntity.AdminPostID
                };

                objDMListFiles.Add ( objFileDM );
            } );
        }


        List<AdminPostCommentDataModel> objDMListComments
            = new List<AdminPostCommentDataModel>();


        if ( postEntity.ListAdminPostComments != null
            && postEntity.ListAdminPostComments.Count > 0 )
        {

            postEntity.ListAdminPostComments.ToList ( ).ForEach ( commentEntity =>
            {

                AdminPostCommentDataModel objCommentDM = new AdminPostCommentDataModel()
                {
                    AdminPostCommentID = commentEntity.AdminPostCommentID,
                    Comment = commentEntity.Comment,
                    AdminPostID = commentEntity.AdminPostID
                };

                objDMListComments.Add ( objCommentDM );

            } );
        }

        AdminPostDataModel objDataModel = new AdminPostDataModel()
        {
            AdminPostID = postEntity.AdminPostID,
            PosterName = postEntity.PosterName,
            PostTitle = postEntity.Title,
            PosterContactNumber = postEntity.PosterContactNumber,
            WebsiteUrl = postEntity.WebsiteUrl,
            ShortNote = postEntity.ShortNote,
            SearchTag = postEntity.SearchTag,
            UserID = postEntity.UserID,
            PostTypeID = (int)postEntity.PostType,
            ListAdminPostFileImages = objDMListFiles,
            ListAdminPostComments = objDMListComments
            
        };

        return objDataModel;
    }

    public static AdminPostDataModel MapAdminPostDataModel (AdminPostViewModel objAdminPostVM)
    {
        return new AdminPostDataModel()
        {
            PosterName = objAdminPostVM.PosterName,
            PostTitle = objAdminPostVM.PostTitle,
            PostTypeID = objAdminPostVM.PostTypeID,
            WebsiteUrl = objAdminPostVM.WebsiteUrl,
            SearchTag = objAdminPostVM.SearchTag,
            ShortNote = objAdminPostVM.ShortNote,
            ListAdminPostFileImages = new List<AdminImageFileDataModel>(),
            ListAdminPostComments = new List<AdminPostCommentDataModel>(),
            PosterContactNumber = objAdminPostVM.PosterContactNumber 
        };
    }

    public static List<AdminImageFileDataModel> MapAdminFileDataModel(AdminPostViewModel adminFileVM)
    {
        List<AdminImageFileDataModel> objListFileDM = new List<AdminImageFileDataModel>();
        adminFileVM.ListAdminPostFileImages.ForEach(fileVM =>
        {
            objListFileDM.Add(new AdminImageFileDataModel ( fileVM.ImageFileContent));
        });
        return objListFileDM;
    }

    public static void MapAdminPostViewModel(AdminPostDataModel postDM, AdminPostViewModel postViewModel)
    {
        postViewModel.AdminPostID = postDM.AdminPostID;
        postViewModel.PostTitle = postDM.PostTitle;
        postViewModel.PosterContactNumber = postDM.PosterContactNumber;
        postViewModel.PosterName = postDM.PosterName;
        postViewModel.WebsiteUrl = postDM.WebsiteUrl;
        postViewModel.PostTypeID = postDM.PostTypeID;
        postViewModel.SearchTag = postDM.SearchTag;
        postViewModel.ShortNote = postDM.ShortNote;
        postViewModel.EnumAdminPostTypeDescription = EnumDescription.GetDescription((EnumPostType) postDM.PostTypeID);
    }
}
