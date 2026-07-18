
using Domain.Model;
using Main.Infrastructure.Database;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;
namespace Main.Repository;

public class AdminPostRepository: IAdminPostRepository
{
    private readonly ApplicationDbContext _context;


    public AdminPostRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChanges ()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<AdminPost>> GetAllAdminContentPosts ()
    {
        var listPostEntity = await _context.AdPosts
                                           .ToListAsync();

        return listPostEntity;
    }

    public async Task<bool> DeleteAdminPost (int postId)
    {
        var adminPost = _context.AdPosts.ToList()
            .FirstOrDefault(a => a.AdminPostID == postId);

        if ( adminPost != null )
        {
            _ = _context.AdPosts.Remove (adminPost);
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAdminPostImage (int id,int postId)
    {
        var adminImageFile = await _context.AdImageFiles
                        .Where(
                           a => a.AdminImageFileID == id
                           && a.AdminPostID == postId)
                        .FirstOrDefaultAsync();

        if ( adminImageFile != null )
        {
            _ = _context.AdImageFiles.Remove (adminImageFile);
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<AdminPost> GetAdminPostByPostID (int postId)
    {
        var postEntity = await _context.AdPosts
                            .SingleAsync (a => a.AdminPostID == postId);

        return postEntity;
    }

    public async Task<bool> SaveNewAdminPost (AdminPost adminPostEntity)
    {
        _ = _context.AdPosts.Add (adminPostEntity);

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAdminPost (AdminPost postEntity)
    {
        _ = _context.AdPosts.Update (postEntity);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<AdminPost>> GetSelectAdminPosts ()
    {
        return await _context.AdPosts.ToListAsync<AdminPost> ();
    }
}

