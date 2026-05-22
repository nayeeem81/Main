using IService;
using IRepository;
using BusinessModel;
using Main.Services.IServices;

namespace Main.Service;

public class AdminPostQueryService: IQueryAdminPostService
{
    private readonly IAdminPostRepository _AdminPostRepository;

    public AdminPostQueryService
           ( IAdminPostRepository adminPostRepository )
    {
        _AdminPostRepository = adminPostRepository;
    }

    public async Task<List<AdminPostDataModel>> GetAllAdminPosts ( )
    {
        return await _AdminPostRepository.GetAllAdminContentPosts();
    }

    public async Task<AdminPostDataModel> 
                 GetAdminPostForEditPostID ( int postID )
    {
        return await _AdminPostRepository
                    .GetAdminPostByPostID(postID);
    }
}