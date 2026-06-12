using Domain.Model;

using Main.Common.Enums;

using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure;

public class BussinessAppDbContext ( DbContextOptions<BussinessAppDbContext> options ): DbContext ( options )
{

    public DbSet<Page> Pages
    {
        get; set;
    }

    public DbSet<Panel> Panels
    {
        get; set;
    }

    public DbSet<Post> Posts
    {
        get; set;
    }

    public DbSet<Product> Products
    {
        get; set;
    }

    public DbSet<ProductImageFile> ProductImageFiles
    {
        get; set;
    }

    public DbSet<ProductComment> ProductComments
    {
        get; set;
    }

    public DbSet<AdminPost> AdPosts
    {
        get; set;
    }

    public DbSet<AdminPostComment> AdPostComments
    {
        get; set;
    }

    public DbSet<AdminImageFile> AdImageFiles
    {
        get; set;
    }


    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        base.OnModelCreating ( modelBuilder );

        PageSeed ( modelBuilder );
    }


    private void PageSeed ( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.Home,1 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.AdsDetail,2 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.Signup,3 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.Login,4 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.AllMarket,5 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.SubCategoryDropdownMarket,6 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.CategoryButtonMarket,8 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.NoticeAndNews,9 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.Resources,11 ) );

        modelBuilder.Entity<Page> ( ).HasData ( new Page ( EnumPublicPage.SpecialMarketButton,12 ) );
    }
}