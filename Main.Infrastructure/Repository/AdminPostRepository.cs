
using Domain.Model;

using IRepository;

using Main.Common.Enums;
using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AdminPostRepository: IAdminPostRepository
{
    private readonly BussinessAppDbContext _context;

    public AdminPostRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<bool> SaveChanges ( )
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<AdminPost>> GetAllAdminContentPosts ( )
    {
        var listPostEntity = await _context.AdPosts
                                           .ToListAsync();

        return listPostEntity;
    }

    public async Task<bool> DeleteAdminPost ( int postId )
    {
        var adminPost = _context.AdPosts.ToList()
            .Single(a => a.AdminPostID == postId);

        if ( adminPost != null )
        {
            _context.AdPosts.Remove ( adminPost );
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAdminPostImage ( int id,int postId )
    {
        var adminImageFile = await _context.AdImageFiles
                        .Where(
                           a => a.AdminImageFileID == id
                           && a.AdminPostID == postId)
                        .FirstOrDefaultAsync();

        if ( adminImageFile != null )
        {
            _context.AdImageFiles.Remove ( adminImageFile );
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<AdminPost> GetAdminPostByPostID ( int postId )
    {
        var postEntity = await _context.AdPosts
                            .SingleAsync (a => a.AdminPostID == postId);

        return postEntity;
    }

    public async Task<bool> SaveNewAdminPost ( AdminPost adminPostEntity )
    {
        _context.AdPosts.Add ( adminPostEntity );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAdminPost ( AdminPost postEntity )
    {
        _context.AdPosts.Update ( postEntity );

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<AdminPost>> GetSelectAdminPosts ( EnumCompanyName company )
    {
        return await _context.AdPosts.Where ( a => a.HostCompanyName == company ).ToListAsync<AdminPost> ( );
    }
}

