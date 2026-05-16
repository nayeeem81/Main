using BusinessModel;

namespace IService;

public interface IAdminPostDataService
{
    Task<List<AdminPostDataModel>> GetAllAdminPosts ( );

    Task<bool> SaveNewAdminPost ( AdminPostDataModel objPostVm );

    Task<AdminPostDataModel> GetAdminPostForEditPostID ( int postID );

    Task<bool> UpdateAdminPost ( AdminPostDataModel objPostVm );

    Task<bool> DeleteAdminPostImage ( int id,int postId );

    Task<bool> DeleteAdminPost ( int postId );
}

