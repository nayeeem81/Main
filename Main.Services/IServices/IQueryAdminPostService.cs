using BusinessModel;

namespace Main.Services.IServices;

public interface IQueryAdminPostService
{
    Task<List<AdminPostDataModel>> GetAllAdminPosts ( );

    Task<AdminPostDataModel>
                 GetAdminPostForEditPostID ( int postID );
}

