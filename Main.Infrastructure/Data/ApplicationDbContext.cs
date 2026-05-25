using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure;

public class ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ): IdentityDbContext ( options )
{
    protected override void OnModelCreating ( ModelBuilder builder )
    {
        base.OnModelCreating ( builder );

        builder.Entity<IdentityUser> ( entity =>
        {
            entity.ToTable ( name: "IdentityUser" );
        } );
        builder.Entity<IdentityRole> ( entity =>
        {
            entity.ToTable ( name: "IdentityRole" );
        } );

        FineArtsSeedIdentity ( builder );

    }

    private void FineArtsSeedIdentity ( ModelBuilder builder )
    {

        #region Role Seeding
        builder.Entity<IdentityRole> ( ).HasData (
              new IdentityRole { Id = "e02fd0e4-00fd-090a-ca30-0d00a0038ba0",Name = "Admin",NormalizedName = "ADMIN" },
              new IdentityRole { Id = "e02fd0e4-00fd-090a-ca30-0d00a0038ba1",Name = "Company",NormalizedName = "COMPANY" },
              new IdentityRole { Id = "e02fd0e4-00fd-090a-ca30-0d00a0038ba2",Name = "Advertiser",NormalizedName = "ADVERTISER" },
              new IdentityRole { Id = "e02fd0e4-00fd-090a-ca30-0d00a0038ba4",Name = "User",NormalizedName = "USER" },
              new IdentityRole { Id = "e02fd0e4-00fd-090a-ca30-0F00a0898ba4",Name = "SuperAdmin",NormalizedName = "SUPERADMIN" }
        );
        #endregion

        // 1. Create a hasher
        var hasher = new PasswordHasher<IdentityUser>();

        #region Super Admin User
        // 2. Define the user
        var superAdmin = new IdentityUser
        {
            Id = "e03fd0d4-00fd-090a-ca10-0d00a1118ba4", // Use a constant string GUID
            UserName = "SuperAdmin",
            NormalizedUserName = "SUPERADMIN",
            Email = "naimul.prodhan@gmail.com",
            NormalizedEmail = "naimul.prodhan@gmail.com".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        // 3. Hash the password
        superAdmin.PasswordHash = hasher.HashPassword ( superAdmin,"Focus@1nm" );

        // 4. Seed the user
        builder.Entity<IdentityUser> ( ).HasData ( superAdmin );


        IdentityUserRole<string> superAdminUserRole = new IdentityUserRole<string>
        {
            RoleId = "e02fd0e4-00fd-090a-ca30-0F00a0898ba4", //  Super Admin role ID
            UserId = superAdmin.Id
        };

        builder.Entity<IdentityUserRole<string>> ( ).HasData ( superAdminUserRole );
        #endregion

        #region Admin User Seeding


        // 2. Define the user
        var adminUserFineArts = new IdentityUser
        {
            Id = "e03fd0e4-00fd-090a-ca10-0d00a0018ba4", // Use a constant string GUID
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "syedron@gmail.com",
            NormalizedEmail = "syedron@gmail.com".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        // 3. Hash the password
        adminUserFineArts.PasswordHash = hasher.HashPassword ( adminUserFineArts,"Arts@1nm" );

        // 4. Seed the user
        builder.Entity<IdentityUser> ( ).HasData ( adminUserFineArts );


        IdentityUserRole<string> adminUserRole = new IdentityUserRole<string>
        {
            RoleId = "e02fd0e4-00fd-090a-ca30-0d00a0038ba0", // Admin role ID
            UserId = adminUserFineArts.Id
        };

        builder.Entity<IdentityUserRole<string>> ( ).HasData ( adminUserRole );
        #endregion

        #region Company User Seeding
        // 1. Create a hasher
        hasher = new PasswordHasher<IdentityUser> ( );

        // 2. Define the user
        var companyUserFineArts = new IdentityUser
        {
            Id = "e03fd0e4-00fd-090a-ca10-0d00a0018ba5", // Use a constant string GUID
            UserName = "Company",
            NormalizedUserName = "COMPANY",
            Email = "finearts@gmail.com",
            NormalizedEmail = "FINEARTS@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        // 3. Hash the password
        companyUserFineArts.PasswordHash = hasher.HashPassword ( companyUserFineArts,"Arts@1nm" );

        // 4. Seed the user
        builder.Entity<IdentityUser> ( ).HasData ( companyUserFineArts );


        IdentityUserRole<string> companyUserRole = new IdentityUserRole<string>
        {
            RoleId = "e02fd0e4-00fd-090a-ca30-0d00a0038ba1", // Company role ID
            UserId = companyUserFineArts.Id
        };

        builder.Entity<IdentityUserRole<string>> ( ).HasData ( companyUserRole );
        #endregion
    }

}