using BusinessModel;

namespace IService;

public interface ICommandAdminPostService
{
    Task<List<AdminPostDataModel>> GetAllAdminPosts ( );

    Task<bool> SaveNewAdminPost ( AdminPostDataModel objPostDm );

    Task<AdminPostDataModel> GetAdminPostForEditPostID ( int postID );

    Task<bool> UpdateAdminPost ( AdminPostDataModel objPostDm );

    Task<bool> DeleteAdminPostImage ( int id,int postId );

    Task<bool> DeleteAdminPost ( int postId );
}

