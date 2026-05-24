using Data;
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{
    private readonly BussinessAppDbContext _context;

    public AdminPostImageRepository( 
        BussinessAppDbContext context ) 
    { 
        _context = context;
    }


    public async Task<List<PanelPost>> 
        GetSelectAdminPosts(EnumCompanyName company)
    {
        List<AdminPost> listAdminPost = 
              await _context.AdminPosts
                    .Where(a => a.HostCompanyName == company)
                    .ToListAsync<AdminPost>();

        if ( listAdminPost == null )
        {
            return new List<PanelPost> ( );
        }

        List<PanelPost> listSelectPanelPostEntity 
            = new List<PanelPost>();

        PanelPost panelPostEntity;

        listAdminPost.ForEach ( adminPostEntity => 
        {
            adminPostEntity.ListAdminImageFiles.ToList ( ).ForEach ( file =>
            {
                panelPostEntity = new PanelPost ( );

                panelPostEntity.RootID = adminPostEntity.AdminPostID;
                panelPostEntity.EnumPostType = adminPostEntity.PostType;
                panelPostEntity.PostTitle = adminPostEntity.Title;
                panelPostEntity.ImageFileContent 
                                = file.ImageFileContent;

                listSelectPanelPostEntity.Add ( panelPostEntity );
            } );
        } );

        return listSelectPanelPostEntity
            .OrderBy ( a => a.PostTitle )
            .ToList<PanelPost> ( );
    }
}

