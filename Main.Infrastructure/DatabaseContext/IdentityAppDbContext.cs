using Main.Common;
using Main.Common.Models;
using Main.Model.Base;
using Main.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.DatabaseContext;

public class IdentityAppDbContext: IdentityDbContext<ApplicationUser>
{
    private readonly ITenantSetter _tenantSetter;

    public IdentityAppDbContext (DbContextOptions options) : base (options)
    {
    }

    public IdentityAppDbContext (DbContextOptions options,
    ITenantSetter tenantSetter) : base (options)
    {
        _tenantSetter = tenantSetter;
    }

    public static readonly Guid[] guidArray = new[]
    {
        new Guid(1, 0, 0, new byte[8]),   // (Tenant 1)                            // index 0
        new Guid(2, 0, 0, new byte[8]),   // (Tenant 2)                            // index 1
        new Guid(19, 0, 0, new byte[8]),  // (Tenant 3)                            // index 18
        new Guid(20, 0, 0, new byte[8]),  // (Tenant 4)                            // index 19

        new Guid(3, 0, 0, new byte[8]),   // IdentityRole (ID)                     // index 2
        new Guid(4, 0, 0, new byte[8]),   // IdentityRole (ID)                     // index 3
        new Guid(5, 0, 0, new byte[8]),   // Global Admin (ID of User)             // index 4
        new Guid(6, 0, 0, new byte[8]),   // Tenant Users (Global Role: User)      // index 5
        new Guid(7, 0, 0, new byte[8]),   // Tenant Users (Global Role: User)      // index 6
        new Guid(8, 0, 0, new byte[8]),   // Tenant Users (Global Role: User)      // index 7
        new Guid(9, 0, 0, new byte[8]),   // Tenant Users (Global Role: User)      // index 8
        new Guid(10, 0, 0, new byte[8]),  // Tenant Users (Global Role: User)      // index 9
        new Guid(11, 0, 0, new byte[8]),  //                                       // index 10
        new Guid(12, 0, 0, new byte[8]),  //
        new Guid(13, 0, 0, new byte[8]),  //     
        new Guid(14, 0, 0, new byte[8]),  //                                       // index 13
        new Guid(15, 0, 0, new byte[8]),  //                                       // index 14
        new Guid(16, 0, 0, new byte[8]),  //                                       // index 15
        new Guid(17, 0, 0, new byte[8]),  // (Tenant 1 Theme)                      // index 16
        new Guid(18, 0, 0, new byte[8]),  // (Tenant 2 Theme)                      // index 17
        
    };

    public DbSet<UserRefreshToken> ApplicationUserRefreshTokens
    {
        get; set;
    }

    public DbSet<Tenant> Tenants
    {
        get; set;
    }

    public DbSet<TenantSmtpServer> TenantSmtpServers
    {
        get; set;
    }

    public DbSet<TenantUserRole> TenantUserRoles
    {
        get; set;
    }

    public DbSet<TenantTheme> TenantThemes
    {
        get; set;
    }

    public DbSet<TenantInvitation> TenantInvitations
    {
        get; set;
    }



    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        ConfigureEntitiesWithFluent (builder);

        ConfigureIndexes (builder);

        SeedData (builder);
    }

    private void ConfigureIndexes (ModelBuilder builder)
    {

    }

    private void ConfigureEntitiesWithFluent (ModelBuilder builder)
    {
        _ = builder.Entity<TenantInvitation> (static entity =>
            {
                _ = entity.Property (t => t.Email)
                      .IsRequired ();
                _ = entity.Property (t => t.Token)
                      .IsRequired ();
                _ = entity.Property (t => t.Status)
                      .IsRequired ();
                _ = entity.Property (t => t.CreatedOn)
                      .IsRequired ();
                _ = entity.Property (t => t.ExpiresOn)
                      .IsRequired ();
            });

        _ = builder.Entity<TenantInvitation> ()
            .Property (t => t.Status)
            .HasDefaultValue (InvitationStatus.Pending)
            .HasSentinel (( InvitationStatus ) ( -1 ));

        _ = builder.Entity<EmailOutboxMessage> (static entity =>
            {
                _ = entity.HasKey (t => t.Id);
                _ = entity.Property (t => t.ReceiverEmail).IsRequired ();
                _ = entity.Property (t => t.Subject).IsRequired ();
                _ = entity.Property (t => t.Body).IsRequired ();
                _ = entity.Property (t => t.CreatedOnUtc).IsRequired ();
                _ = entity.Property (t => t.RetryCount).IsRequired ();
            });

        _ = builder.Entity<UserRefreshToken> (entity =>
            {
                _ = entity.HasKey (e => e.Id);
                _ = entity.Property (e => e.Id)
                      .ValueGeneratedNever ();
                _ = entity.Property (e => e.Token)
                      .IsRequired ()
                      .HasMaxLength (4000);
                _ = entity.Property (e => e.UserId)
                      .IsRequired ()
                      .HasMaxLength (450);
                _ = entity.Property (e => e.CreatedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.ModifiedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.DeletedBy).HasMaxLength (256).IsRequired (false);
                _ = entity.Property (e => e.ReplacedByToken).HasMaxLength (4000).IsRequired (false);
                _ = entity.Property (e => e.TenantContinent).HasMaxLength (100).IsRequired (false);
                _ = entity.Property (e => e.TenantCountry).HasMaxLength (100).IsRequired (false);
            });
    }

    public void SeedData (ModelBuilder builder)
    {
        Guid TenantId1 = guidArray[0];
        Guid TenantId2 = guidArray[1];
        Guid TenantId18 = guidArray[18];
        Guid TenantId19 = guidArray[19];

        TenantSeed1 (builder,TenantId1);
        TenantSeed2 (builder,TenantId2);
        TenantSeed3 (builder,TenantId18);
        TenantSeed4 (builder,TenantId19);

        Guid IdentityRoleId1 = guidArray[2];
        Guid IdentityRoleId2 = guidArray[3];
        IdentityRolesSeed (builder,IdentityRoleId1,IdentityRoleId2);

        //Guid ThemeId1 = guidArray[16];
        //Guid ThemeId2 = guidArray[17];
        //Tenant1ThemeSeed (builder,ThemeId1,TenantId1);
        //Tenant2ThemeSeed (builder,ThemeId2,TenantId2);

        Guid UserIdGlobal1 = guidArray[4];
        var adminGlobalEmail = "admin@system.com";
        GlobalAdminUserSeed (builder,UserIdGlobal1,adminGlobalEmail,IdentityRoleId1);


        // UserId2, UserId3, UserId4, UserId5, UserId6, UserId7 
        // Tenant Users (Global Role: User)
        Guid UserId2 = guidArray[5];
        Guid UserId3 = guidArray[6];
        Guid UserId4 = guidArray[7];
        Guid UserId5 = guidArray[8];
        Guid UserId6 = guidArray[9];
        Guid UserId7 = guidArray[10];

        Guid UserId10 = guidArray[11];    //fine arts admin user
        Guid UserId11 = guidArray[12];    //life styles admin user

        TenantUserSeed (builder,IdentityRoleId2,UserId2,UserId3,UserId4,
            UserId5,UserId6,UserId7,UserId10,UserId11,
            TenantId1,TenantId2,TenantId18,TenantId19);

    }

    private void TenantUserSeed (
        ModelBuilder builder,
        Guid IdentityRoleId,Guid userId2,
        Guid userId3,Guid userId4,Guid userId5,
        Guid userId6,Guid userId7,
        Guid UserId10,Guid UserId11,
        Guid tenantId1,Guid tenantId2,
        Guid tenantId3,Guid tenantId4)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var testUsersConfigurationSeed = new []
        {
            new {
                UserId = userId2.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant1.admin@test.com",
                TenantId = tenantId1 ,
                TenantRole = "Admin",
                TenantUserRoleId = 2,
                EmailConfirmed = true
            },
            new {
                UserId = userId3.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant1.manager@test.com",
                TenantId = tenantId1 ,
                TenantRole = "Manager",
                TenantUserRoleId = 3,
                EmailConfirmed = true
            },
            new
            {
                UserId = userId4.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant1.member@test.com",
                TenantId = tenantId1 ,
                TenantRole = "Member",
                TenantUserRoleId = 4,
                EmailConfirmed = true
            },
            new {
                UserId = userId5.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant2.admin@test.com",
                TenantId = tenantId2  ,
                TenantRole = "Admin",
                TenantUserRoleId = 5,
                EmailConfirmed = true
            },
            new {
                UserId = userId6.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant2.manager@test.com",
                TenantId = tenantId2  ,
                TenantRole = "Manager",
                TenantUserRoleId = 6,
                EmailConfirmed = true,
            },
            new {
                UserId = userId7.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "tenant2.member@test.com",
                TenantId = tenantId2 ,
                TenantRole = "Member",
                TenantUserRoleId = 7,
                EmailConfirmed = true
            }    ,

            new {
                UserId = UserId10.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "finearts@test.com",
                TenantId = tenantId3 ,
                TenantRole = "Admin",
                TenantUserRoleId = 8,
                EmailConfirmed = true
            } ,
            new {
                UserId = UserId11.ToString(),
                RoleId = IdentityRoleId.ToString(),
                Email = "lifestyles@test.com",
                TenantId = tenantId4 ,
                TenantRole = "Admin",
                TenantUserRoleId = 9,
                EmailConfirmed = true
            }
        };

        // Create Tenant Users 
        foreach ( var config in testUsersConfigurationSeed )
        {

            var user = new ApplicationUser (config.UserId)
            {
                UserName = config.Email,
                Email = config.Email,
                EmailConfirmed = true
            };

            user.PasswordHash = hasher.HashPassword (user,"Focus@1nm");

            _ = builder.Entity<ApplicationUser> ().HasData (user);

            _ = builder.Entity<IdentityUserRole<string>> ().HasData (
               new IdentityUserRole<string> ()
               {
                   RoleId = config.RoleId.ToString (),
                   UserId = config.UserId.ToString ()
               });

            _ = builder.Entity<TenantUserRole> ().HasData
                (new TenantUserRole (config.TenantUserRoleId)
                {
                    UserId = config.UserId,
                    TenantRole = config.TenantRole,
                    TenantId = config.TenantId
                });
        }
    }

    private void GlobalAdminUserSeed (ModelBuilder builder,Guid UserIdGlobal1,string adminGlobalEmail,Guid identityRoleId1)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var newAdmin = new ApplicationUser
        {
            Id = UserIdGlobal1.ToString(),
            UserName = adminGlobalEmail,
            Email = adminGlobalEmail,
            EmailConfirmed = true
        };

        newAdmin.PasswordHash = hasher.HashPassword (newAdmin,"Focus@1nm");

        _ = builder.Entity<ApplicationUser> ().HasData (newAdmin);

        _ = builder.Entity<IdentityUserRole<string>> ().HasData (
       new IdentityUserRole<string> ()
       {
           RoleId = identityRoleId1.ToString (),
           UserId = UserIdGlobal1.ToString ()
       });
    }

    private void TenantSeed1 (ModelBuilder builder,Guid tenantId1)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId1)
        {
            TenantName = "Tenant 1 (Finearts: Collections)",
            HostType = HostType.Domain,
            Host = "tenant1",
            StoreType = StoreType.FineCollections
        });
    }

    private void TenantSeed2 (ModelBuilder builder,Guid tenantId2)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId2)
        {
            TenantName = "Tenant 2 (Finearts: Crafts)",
            HostType = HostType.Domain,
            Host = "tenant2",
            StoreType = StoreType.FineCrafts
        });
    }

    private void TenantSeed3 (ModelBuilder builder,Guid tenantId2)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId2)
        {
            TenantName = "Tenant 3 (Finearts: Arts)",
            HostType = HostType.Domain,
            Host = "finearts",
            StoreType = StoreType.FineArts
        });
    }

    private void TenantSeed4 (ModelBuilder builder,Guid tenantId2)
    {
        _ = builder.Entity<Tenant> ().HasData (new Tenant (tenantId2)
        {
            TenantName = "Tenant 4 (LifeStyles)",
            HostType = HostType.Domain,
            Host = "lifestyles",
            StoreType = StoreType.LifeStyles
        });
    }

    private void IdentityRolesSeed (ModelBuilder builder,Guid identityRoleId1,Guid identityRoleId2)
    {
        _ = builder.Entity<IdentityRole> ().HasData (
            new IdentityRole
            {
                Id = identityRoleId1.ToString (),
                Name = "GlobalAdmin",
                NormalizedName = "GLOBALADMIN"
            });


        _ = builder.Entity<IdentityRole> ().HasData (
            new IdentityRole
            {
                Id = identityRoleId2.ToString (),
                Name = "User",
                NormalizedName = "USER"
            });
    }

    private void Tenant1ThemeSeed (ModelBuilder builder,Guid themeId,Guid tenantId)
    {
        TenantTheme tenantTheme = new()
        {
            Id = themeId,
            BodyBackgroundColor = "",
            BodyColor = "",
            HeaderColor = "",
            LogoColor = "",
            ButtonBGBorderColor ="",
            MenuBackgroundColor  ="",
            MenuItemHoverBGColor ="",
            MenuItemHoverColor ="",
            FontStack = "Garamond, Baskerville, 'Baskerville Old Face', 'Hoefler Text', Georgia, 'Times New Roman', serif",
            LogoRelativeFilePath = "~/favicon.ico" ,
            TenantId = tenantId
        };

        _ = builder.Entity<TenantTheme> ().HasData (tenantTheme);
    }

    private void Tenant2ThemeSeed (ModelBuilder builder,Guid themeId,Guid tenantId)
    {
        TenantTheme tenantTheme = new()
        {
            Id = themeId,
            BodyBackgroundColor = "",
            BodyColor = "",
            HeaderColor = "",
            LogoColor = "",
            ButtonBGBorderColor ="",
            MenuBackgroundColor  ="",
            MenuItemHoverBGColor ="",
            MenuItemHoverColor ="",
            FontStack = "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
            LogoRelativeFilePath = "~/favicon.ico" ,
            TenantId = tenantId
        };

        _ = builder.Entity<TenantTheme> ().HasData (tenantTheme);
    }

    private void ApplyBaseDataTenantId ()
    {
        BaseDataModel createDataModel = _tenantSetter.CreateMetaData;
        BaseDataModel updateDataModel = _tenantSetter.UpdateMetaData;
        BaseDataModel deleteDataModel = _tenantSetter.DeleteMetaData;

        var entries = ChangeTracker.Entries()
        .Where(e =>
              e.State == EntityState.Added
               || e.State == EntityState.Modified
               || e.State == EntityState.Detached).ToArray();

        foreach ( var entry in entries )
        {
            var tenantEntity = (INeedRootBaseEntity) entry.Entity;

            if ( entry.State == EntityState.Added )
            {
                tenantEntity.CreateParameters (createDataModel);
            }
            else if ( entry.State == EntityState.Deleted )
            {
                tenantEntity.DeleteParameters (deleteDataModel);
            }
            else if ( entry.State == EntityState.Modified )
            {
                tenantEntity.ModifyParameters (updateDataModel);
            }
        }
    }

    public override int SaveChanges ()
    {
        return base.SaveChanges ();
    }

    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync (true,cancellationToken);
    }
}
