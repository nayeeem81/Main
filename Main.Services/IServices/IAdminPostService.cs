using DataTransferModel;

namespace Main.Services;

public interface IAdminPostService
{
    Task<List<AdminPostDisplayModel>> GetAllAdminPosts ( );

    Task<bool> SaveNewAdminPost ( AdminPostDataModel postDataModel );

    Task<AdminPostDataModel> GetAdminPostForEditPostID ( int postID );

    Task<bool> UpdateAdminPost ( AdminPostDataModel postDataModel );

    Task<bool> DeleteAdminPostImage ( int id,int postId );

    Task<bool> DeleteAdminPost ( int postId );
}

