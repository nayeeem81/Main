using DataTransferModel;

using Domain.Model;

namespace Services.Extensions;

public static class AdminPostServiceMappings
{
    public static List<AdminPostDisplayModel> MapListDataModel (
        List<AdminPost> listadminPostEntities )
    {
        AdminPostDisplayModel objDataModel;

        List<AdminPostDisplayModel> listPostDataModel
            = new List<AdminPostDisplayModel>();

        listadminPostEntities.ForEach ( postEntity =>
        {
            objDataModel = new AdminPostDisplayModel ( );

            objDataModel.AdminPostID = postEntity.AdminPostID;
            objDataModel.PosterName = postEntity.PosterName;
            objDataModel.PostTitle = postEntity.Title;
            objDataModel.PostType = postEntity.PostType;

            listPostDataModel.Add ( objDataModel );

        } );

        return listPostDataModel;
    }

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
            PostType = postEntity.PostType,
            ListAdminPostFileImages = objDMListFiles,
            ListAdminPostComments = objDMListComments
        };

        return objDataModel;
    }

    public static AdminPost MapAdminPostEntity
    (
        AdminPostDataModel from,
        List<AdminImageFileDataModel> fromListImages
    )
    {
        AdminPost adminPostEntity = CreareAdminPostEntity ( from );

        List<AdminImageFile> objListFileEntity = MapAdminFileEntity(from);

        adminPostEntity.ListAdminImageFiles = objListFileEntity;
        adminPostEntity.ListAdminPostComments = new List<AdminPostComment> ( );

        return adminPostEntity;
    }


    private static AdminPost CreareAdminPostEntity ( AdminPostDataModel adminPostDataModel )
    {
        AdminPost adminPost = new AdminPost ( )
        {
            PosterName = adminPostDataModel.PosterName,
            Title = adminPostDataModel.PostTitle,
            PostType =     adminPostDataModel.PostType ,
            WebsiteUrl = adminPostDataModel.WebsiteUrl,
            SearchTag = adminPostDataModel.SearchTag,
            ShortNote = adminPostDataModel.ShortNote,
            ListAdminImageFiles = new List<AdminImageFile> ( ),
            ListAdminPostComments = new List<AdminPostComment> ( ),
            PosterContactNumber = adminPostDataModel.PosterContactNumber
        };

        adminPost.CreateBaseData ( adminPostDataModel.BaseDataModel );

        return adminPost;
    }

    private static List<AdminImageFile> MapAdminFileEntity ( AdminPostDataModel adminPostDataModel )
    {
        List<AdminImageFile> objListFileEntity
        = new List<AdminImageFile>();

        AdminImageFile adminFileEntity;

        adminPostDataModel.ListAdminPostFileImages.ForEach ( fileDataModel =>
        {
            adminFileEntity = new AdminImageFile ( fileDataModel.ImageFileContent );

            adminFileEntity.CreateBaseData ( fileDataModel.BaseDataModel );

            objListFileEntity.Add ( adminFileEntity );

        } );

        return objListFileEntity;
    }

    public static AdminPost UpdateAdminPostEntityMapping ( AdminPost adminPostEntity,AdminPostDataModel adminPostDataModel )
    {
        adminPostEntity.ModifyBaseData ( adminPostDataModel.BaseDataModel );

        List<AdminImageFile> newListFileEntities = new List<AdminImageFile>();
        newListFileEntities.AddRange ( adminPostEntity.ListAdminImageFiles );


        adminPostDataModel.ListAdminPostFileImages.ForEach ( fileDataModel =>
        {
            AdminImageFile adminImageFile = new AdminImageFile(fileDataModel.ImageFileContent);
            adminImageFile.AdminPostID = adminPostDataModel.AdminPostID;

            adminImageFile.CreateBaseData ( fileDataModel.BaseDataModel );

            newListFileEntities.Add ( adminImageFile );
        } );


        List<AdminPostComment> newListcommentEntities = new List<AdminPostComment>();

        adminPostDataModel.ListAdminPostComments.ForEach ( commentDataModel =>
        {
            AdminPostComment adminPostComment = new AdminPostComment();
            adminPostComment.AdminPostID = adminPostDataModel.AdminPostID;
            adminPostComment.Comment = commentDataModel.Comment;

            adminPostComment.CreateBaseData ( commentDataModel.BaseDataModel );

            newListcommentEntities.Add ( adminPostComment );

        } );

        adminPostEntity.PosterName = adminPostDataModel.PosterName;
        adminPostEntity.Title = adminPostDataModel.PostTitle;
        adminPostEntity.PosterContactNumber = adminPostDataModel.PosterContactNumber;
        adminPostEntity.WebsiteUrl = adminPostDataModel.WebsiteUrl;
        adminPostEntity.ShortNote = adminPostDataModel.ShortNote;
        adminPostEntity.SearchTag = adminPostDataModel.SearchTag;
        adminPostEntity.PostType = adminPostDataModel.PostType;
        adminPostEntity.ListAdminPostComments = newListcommentEntities;
        adminPostEntity.ListAdminImageFiles = newListFileEntities;
        adminPostEntity.AdminPostID = adminPostDataModel.AdminPostID;

        return adminPostEntity;
    }


}
