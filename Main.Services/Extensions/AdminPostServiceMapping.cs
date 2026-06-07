using DataTransferModel;
using Domain.Model;
using Main.Common.Enums;

namespace Services.Extensions;

public static class AdminPostServiceMappings
{
    public static List<AdminPostDisplayModel> MapListDataModel ( List<AdminPost> listadminPostEntities )
    {
        AdminPostDisplayModel objDataModel;

        List<AdminPostDisplayModel> listPostDataModel
            = new List<AdminPostDisplayModel>();

        listadminPostEntities.ForEach ( postEntity =>
        {
            objDataModel = new AdminPostDisplayModel ( );

            objDataModel.AdminPostID = postEntity.AdminPostID;
            objDataModel.PosterName = postEntity.PosterName;
            objDataModel.HostCompanyName = postEntity.HostCompanyName;

            objDataModel.PostTitle = postEntity.Title;
            objDataModel.PostTypeID = ( int ) postEntity.PostType;

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
            PostTypeID = (int)postEntity.PostType,
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

        adminPostEntity.CreateBaseData ( from.BaseDataModel );

        List<AdminImageFile> objListFileEntity = MapAdminFileEntity(from);

        adminPostEntity.ListAdminImageFiles = objListFileEntity;
        adminPostEntity.ListAdminPostComments = new List<AdminPostComment> ( );

        return adminPostEntity;
    }


    private static AdminPost CreareAdminPostEntity ( AdminPostDataModel adminPostDataModel )
    {
        return new AdminPost ( )
        {
            PosterName = adminPostDataModel.PosterName,
            Title = adminPostDataModel.PostTitle,
            PostType = ( EnumPostType ) adminPostDataModel.PostTypeID,
            WebsiteUrl = adminPostDataModel.WebsiteUrl,
            SearchTag = adminPostDataModel.SearchTag,
            ShortNote = adminPostDataModel.ShortNote,
            ListAdminImageFiles = new List<AdminImageFile> ( ),
            ListAdminPostComments = new List<AdminPostComment> ( ),
            PosterContactNumber = adminPostDataModel.PosterContactNumber
        };
    }

    private static List<AdminImageFile> MapAdminFileEntity ( AdminPostDataModel adminPostDataModel )
    {
        List<AdminImageFile> objListFileEntity
        = new List<AdminImageFile>();

        AdminImageFile adminFileEntity;
        adminPostDataModel.ListAdminPostFileImages.ForEach ( fileDataModel =>
        {
            adminFileEntity = new AdminImageFile ( fileDataModel.ImageFileContent );
            
            objListFileEntity.Add ( adminFileEntity );

        } );

        return objListFileEntity;
    }

    public static AdminPost UpdateAdminPostEntityMapping ( AdminPost adminPostEntity,AdminPostDataModel adminPostDataModel )
    {
        adminPostEntity.ModifyBaseData ( adminPostDataModel.BaseDataModel );

        List<AdminImageFile> imageFieEntityList = new List<AdminImageFile>();

        imageFieEntityList.AddRange ( adminPostEntity.ListAdminImageFiles );


        adminPostDataModel.ListAdminPostFileImages.ForEach ( fileVM =>
        {
            var objFile = new AdminImageFile(fileVM.ImageFileContent);

            objFile.AdminPostID = adminPostDataModel.AdminPostID;

            imageFieEntityList.Add ( objFile );
        } );


        List<AdminPostComment> commentEntityList = new List<AdminPostComment>();

        adminPostDataModel.ListAdminPostComments.ForEach ( commentDM =>
        {
            var objComment = new AdminPostComment();
            objComment.AdminPostID = adminPostDataModel.AdminPostID;
            objComment.Comment = commentDM.Comment;
            commentEntityList.Add ( objComment );
        } );


        adminPostEntity.PosterName = adminPostDataModel.PosterName;
        adminPostEntity.Title = adminPostDataModel.PostTitle;
        adminPostEntity.PosterContactNumber = adminPostDataModel.PosterContactNumber;
        adminPostEntity.WebsiteUrl = adminPostDataModel.WebsiteUrl;
        adminPostEntity.ShortNote = adminPostDataModel.ShortNote;
        adminPostEntity.SearchTag = adminPostDataModel.SearchTag;
        adminPostEntity.PostType = ( EnumPostType ) adminPostDataModel.PostTypeID;
        adminPostEntity.ListAdminPostComments = commentEntityList;
        adminPostEntity.ListAdminImageFiles = imageFieEntityList;
        adminPostEntity.AdminPostID = adminPostDataModel.AdminPostID;
        

        return adminPostEntity;
    }


}
