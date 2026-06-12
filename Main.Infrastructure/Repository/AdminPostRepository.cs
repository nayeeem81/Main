
using Domain.Model;
using IRepository;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AdminPostRepository : IAdminPostRepository
{
    private readonly BussinessAppDbContext _Context;

    public AdminPostRepository( BussinessAppDbContext context )
    {
        _Context = context;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<AdminPost>> GetAllAdminContentPosts()
    {
        var listPostEntity = await _Context.AdPosts
                                           .ToListAsync();

        return listPostEntity;
    }

    public async Task<bool> DeleteAdminPost(int postId)
    {
        var adminPost = _Context.AdPosts.ToList()
            .Single(a => a.AdminPostID == postId);

        if (adminPost != null)
        {
            _Context.AdPosts.Remove(adminPost);
        }
                
        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAdminPostImage(int id, int postId)
    {
        var adminImageFile = await _Context.AdImageFiles
                        .Where(
                           a => a.AdminImageFileID == id 
                           && a.AdminPostID == postId)
                        .FirstOrDefaultAsync();

        if (adminImageFile != null)
        {
            _Context.AdImageFiles.Remove(adminImageFile);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<AdminPost> GetAdminPostByPostID(int postId)
    {
        var postEntity = await _Context.AdPosts
                            .SingleAsync (a => a.AdminPostID == postId);

        return postEntity;
    }

    public async Task<bool> SaveNewAdminPost ( AdminPost adminPostEntity )
    {
        _Context.AdPosts.Add( adminPostEntity );

        int result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAdminPost ( AdminPost postEntity )
    {
        _Context.AdPosts.Update ( postEntity );

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }
}

