using IService;
using IRepository;
using BusinessModel;

namespace Main.Service;

public class AdminPostDataService: ICommandAdminPostService
{
    private readonly IAdminPostRepository _AdminPostRepository;

    public AdminPostDataService 
           ( IAdminPostRepository adminPostRepository )
    {
        _AdminPostRepository = adminPostRepository;
    }

    public async Task<List<AdminPostDataModel>> GetAllAdminPosts ( )
    {
        return await _AdminPostRepository.GetAllAdminContentPosts();
    }

    public async Task<bool> SaveNewAdminPost ( 
                 AdminPostDataModel objAdminPostDm )
    {
        return await _AdminPostRepository
                   .SaveNewAdminPost      
                   (objAdminPostDm,
                   objAdminPostDm.ListAdminPostFileImages);
    }

    public async Task<AdminPostDataModel> 
                 GetAdminPostForEditPostID ( int postID )
    {
        return await _AdminPostRepository
                    .GetAdminPostByPostID(postID);
    }

    public async Task<bool> UpdateAdminPost 
                 ( AdminPostDataModel objAdminPostDm )
    {
        return await _AdminPostRepository
                   .UpdateAdminPost( objAdminPostDm );
    }

    public async Task<bool> DeleteAdminPostImage 
                 ( int id,int postId )
    {
        return await _AdminPostRepository
                   .DeleteAdminPostImage ( id, postId );
    }

    public async Task<bool> DeleteAdminPost 
                 ( int postId )
    {
        return await _AdminPostRepository
                   .DeleteAdminPost(postId);
    }
}