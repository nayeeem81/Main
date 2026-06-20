using Domain.Model;

using Main.Common.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Data;

namespace Main.Infrastructure;

public class ApplicationDbContext: DbContext
{

    public readonly ITenantSetter _tenantSetter;

    public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options,ITenantSetter tenantSetter )
        : base ( options )
    {
        _tenantSetter = tenantSetter;
    }

    public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : base ( options )
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers
    {
        get; set;
    }

    public DbSet<ApplicationRole> ApplicationRoles
    {
        get; set;
    }

    public DbSet<Tenant> Tenants
    {
        get; set;
    }

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

    protected override void OnModelCreating ( ModelBuilder builder )
    {
        base.OnModelCreating ( builder );

        EntityFluentApiSettings ( builder );

        TenantQueryFilter ( builder );

        SeedData ( builder );
    }

    private void SeedData ( ModelBuilder builder )
    {
        string seedTenancyId1 = "e02fd0e1-00fd-009a-ca30-0d00a2345ba0";
        string seedTenancyId2 = "e02fd0e1-00fd-008a-ca30-5d00a5242ba0";

        // Tenant 1
        TenantSeed ( builder,seedTenancyId1,"Fine Arts Store","fanarts-local",EnumShopType.FineArtsShop );

        // Tenant 2
        TenantSeed ( builder,seedTenancyId2,"LifeStyle Store","lifestyle-local",EnumShopType.LifeStylesShop );

        string RoleAdminID1 = "e02fd0e4-00fd-090a-ca30-0d00a0038ba0";
        string RoleUserID1 =  "e02fd0e4-00fd-090a-ca30-0d00a0038ba4";
        string RoleSuperAdminID1 =  "e02fd0e4-00fd-090a-ca30-0F00a0898ba4";

        // Tenant 1
        RoleSeed ( builder,RoleAdminID1,seedTenancyId1,"Admin" );
        RoleSeed ( builder,RoleUserID1,seedTenancyId1,"User" );
        RoleSeed ( builder,RoleSuperAdminID1,seedTenancyId1,"SuperAdmin" );


        string RoleAdminID2 = "d02fd0e4-10fd-090a-ca30-0d00a0038ba0";
        string RoleUserID2 =  "d02fd0e4-20fd-090a-ca30-0d00a0038ba4";
        string RoleSuperAdminID2 =  "d02fd0e4-30fd-090a-ca30-0F00a0898ba4";

        // Tenant 2
        RoleSeed ( builder,RoleAdminID2,seedTenancyId2,"Admin" );
        RoleSeed ( builder,RoleUserID2,seedTenancyId2,"User" );
        RoleSeed ( builder,RoleSuperAdminID2,seedTenancyId2,"SuperAdmin" );



        // Super Admin (Tenant 1)

        string Tenant1SuperAdminId = "e03fd0d4-00fd-090a-ca10-0d00a1118ba4";
        string Tenant1UserName = "SuperAdmin1";
        string Tenant1Email = "naimul.prodhan@gmail.com";
        string Tenant1Password = "Focus@1nm";

        UserRoleSeed ( builder,Tenant1SuperAdminId,Tenant1UserName,Tenant1Email,Tenant1Password,seedTenancyId1,RoleSuperAdminID1 );

        // Super Admin (Tenant 2)

        string Tenant2SuperAdminId = "e03fd0d4-00fd-090a-da10-0d00a2228ba4";
        string Tenant2UserName = "SuperAdmin2";
        string Tenant2Email = "naimul.prodhan@gmail.com";
        string Tenant2Password = "Focus@1nm";

        UserRoleSeed ( builder,Tenant2SuperAdminId,Tenant2UserName,Tenant2Email,Tenant2Password,seedTenancyId2,RoleSuperAdminID2 );

        // Admin (Tenant 1)

        string Tenant1AdminId =  "e03fd0e4-00fd-090a-ca10-0d00a0018ba4";
        string Tenant1AdminUserName = "Admin1";
        string Tenant1AdminEmail = "syedron@gmail.com";
        string Tenant1AdminPassword = "Focus@1nm";

        UserRoleSeed ( builder,Tenant1AdminId,Tenant1AdminUserName,Tenant1AdminEmail,Tenant1AdminPassword,seedTenancyId2,RoleAdminID1 );

        // Admin (Tenant 2)

        string Tenant2AdminId =  "e03fd0e4-55fd-095a-ca10-0d00a0018ba4";
        string Tenant2AdminUserName = "Admin2";
        string Tenant2AdminEmail = "syedron@gmail.com";
        string Tenant2AdminPassword = "Focus@1nm";

        UserRoleSeed ( builder,Tenant2AdminId,Tenant2AdminUserName,Tenant2AdminEmail,Tenant2AdminPassword,seedTenancyId2,RoleAdminID2 );

        int pageIDSeed = 0;
        pageIDSeed = PageSeed ( pageIDSeed,builder,seedTenancyId1 );
        pageIDSeed = PageSeed ( pageIDSeed,builder,seedTenancyId2 );
    }

    private void UserRoleSeed ( ModelBuilder builder,string userId,string tenantUserName,string tenant1Email,string password,string seedTenancyId,string roleId )
    {
        var hasher = new PasswordHasher<IdentityUser>();

        var user = new ApplicationUser
        {
            Id = userId,
            UserName = tenantUserName,
            NormalizedUserName = tenantUserName.ToUpper(),
            Email = tenant1Email,
            NormalizedEmail = tenant1Email.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D") ,
            TenantId = seedTenancyId
        };

        user.PasswordHash = hasher.HashPassword ( user,password );
        builder.Entity<ApplicationUser> ( ).HasData ( user );
        builder.Entity<IdentityUserRole<string>> ( )
               .HasData (
                   new IdentityUserRole<string>
                   {
                       UserId = userId,
                       RoleId = roleId
                   }
                );
    }

    private void EntityFluentApiSettings ( ModelBuilder builder )
    {
        // Adjust Unique Index for Users to include Tenant ID
        builder.Entity<ApplicationUser> ( entity =>
        {
            entity.HasIndex ( u => new { u.NormalizedUserName,u.TenantId } ).IsUnique ( ).HasDatabaseName ( "UserNameIndex" );
        } );

        // Adjust Unique Index for Roles to include Tenant ID
        builder.Entity<ApplicationRole> ( entity =>
        {
            entity.HasIndex ( r => new { r.NormalizedName,r.TenantId } ).IsUnique ( ).HasDatabaseName ( "RoleNameIndex" );
        } );

        builder.Entity<IdentityUserRole<string>> ( ).HasKey ( ["UserId","RoleId"] );

        builder.Entity<Tenant> ( )
        .HasKey ( t => t.TenantId );

        builder.Entity<Tenant> ( )
            .Property ( t => t.TenantId )
            .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.GuidValueGenerator> ( );

        builder.Entity<Post> ( b =>
        {
            // Configures the decimal column to use decimal(18, 2) in SQL Server
            b.Property ( p => p.Price )
             .HasColumnType ( "decimal(18,2)" )
             .IsRequired ( );
        } );


        builder.Entity<Product> ( b =>
        {
            b.Property ( p => p.Price )
             .HasColumnType ( "decimal(18,2)" )
             .IsRequired ( );

            b.Property ( p => p.Discount )
             .HasColumnType ( "decimal(18,2)" );

            b.Property ( p => p.SaleCommission )
             .HasColumnType ( "decimal(18,2)" );
        } );

    }

    private void TenantQueryFilter ( ModelBuilder builder )
    {
        // Apply global query filter for data isolation across multi-tenant tables
        string currentTenant = _tenantSetter.CurrentTenantId;


        builder.Entity<ApplicationUser> ( ).HasQueryFilter ( u => u.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<ApplicationRole> ( ).HasQueryFilter ( r => r.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Product> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<ProductImageFile> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<ProductComment> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminPost> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminImageFile> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminPostComment> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Post> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Panel> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Page> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AValue> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );
    }

    public override int SaveChanges ( bool acceptAllChangesOnSuccess )
    {
        ApplyTenantId ( );

        return base.SaveChanges ( acceptAllChangesOnSuccess );
    }

    public override Task<int> SaveChangesAsync ( bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default )
    {
        ApplyTenantId ( );

        return base.SaveChangesAsync ( acceptAllChangesOnSuccess,cancellationToken );
    }

    private void ApplyTenantId ( )
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            ( ( IMustHaveTenant ) entry.Entity ).TenantId = _tenantSetter.CurrentTenantId;
        }
    }

    private void TenantSeed ( ModelBuilder builder,string seedTenantId,string name,string domain,EnumShopType shopType )
    {
        builder.Entity<Tenant> ( ).HasData (
            new Tenant ( seedTenantId )
            {
                Name = name,
                Domain = domain,
                ShopType = shopType
            } );
    }

    private void RoleSeed ( ModelBuilder builder,
    string id,string seedTenantId,string role )
    {
        builder.Entity<ApplicationRole> ( ).HasData (
            new ApplicationRole
            {
                Id = id,
                Name = role,
                NormalizedName = role.ToUpper ( ),
                TenantId = seedTenantId
            } );
    }

    private int PageSeed ( int pageIDSeed,ModelBuilder modelBuilder,string seedTenantId )
    {
        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.Home,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.AdsDetail,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.Signup,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.Login,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.AllMarket,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.SubCategoryDropdownMarket,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.CategoryButtonMarket,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.NoticeAndNews,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.Resources,pageIDSeed += 1,seedTenantId ) );

        modelBuilder.Entity<Page> ( ).HasData (
            new Page ( EnumPublicPage.SpecialMarketButton,pageIDSeed += 1,seedTenantId ) );

        return pageIDSeed;
    }

}
