using Domain.Model;

using Main.Common;
using Main.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


public static class DataSeeder
{
    public static async Task SeedDataAsync (IServiceProvider serviceProvider)
    {


        // 2.  FIX: Resolve ALL services from scope.ServiceProvider, including the DbContext
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure database schema exists before executing queries
        _ = await context.Database.EnsureCreatedAsync ();

        // ==========================================
        // 1. SEED TENANTS AND PAGES
        // ==========================================

        // TENANT 1
        Tenant? tenant1 = context.Tenants.FirstOrDefault<Tenant>(a => a.HostName == "lifestyle-local");

        if ( tenant1 == null )
        {
            tenant1 = new Tenant () //  FIX: Assign directly to tenant1 to avoid downstream null exceptions
            {
                Name = "LifeStyle Store",
                HostName = "lifestyle-local",
                Store = StoreType.LifeStyles
            };

            _ = context.Tenants.Add (tenant1);
            _ = await context.SaveChangesAsync ();

            tenant1.MyTenantId = tenant1.TenantId;
            _ = context.Update (tenant1);
            _ = await context.SaveChangesAsync ();

            await PageSeed (context,tenant1.TenantId);
        }

        // TENANT 2
        Tenant? tenant2 = context.Tenants.FirstOrDefault<Tenant>(a => a.HostName == "fanarts-local");

        if ( tenant2 == null )
        {
            tenant2 = new Tenant () //  FIX: Assign directly to tenant2 to avoid downstream null exceptions
            {
                Name = "Fine Arts Store",
                HostName = "fanarts-local",
                Store = StoreType.FineArts
            };

            _ = context.Tenants.Add (tenant2);
            _ = await context.SaveChangesAsync ();

            tenant2.MyTenantId = tenant2.TenantId;
            _ = context.Update (tenant2);
            _ = await context.SaveChangesAsync ();

            await PageSeed (context,tenant2.TenantId);
        }

        // ==========================================
        // 2. SEED GLOBAL ROLES
        // ==========================================
        string[] globalRoles = ["GlobalAdmin", "User"];
        foreach ( var roleName in globalRoles )
        {
            if ( !await roleManager.RoleExistsAsync (roleName) )
            {
                _ = await roleManager.CreateAsync (new IdentityRole (roleName));
            }
        }

        // ==========================================
        // 3. SEED GLOBAL ADMIN
        // ==========================================
        var adminGlobalEmail = "admin@system.com";
        var adminUser = await userManager.FindByEmailAsync(adminGlobalEmail);
        if ( adminUser == null )
        {
            var newAdmin = new ApplicationUser { UserName = adminGlobalEmail, Email = adminGlobalEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(newAdmin, "Focus@1nm");
            if ( result.Succeeded )
            {
                _ = await userManager.AddToRoleAsync (newAdmin,"GlobalAdmin");
            }
        }

        // ==========================================
        // 4. SEED TENANT USERS WITH GLOBAL "User" ROLE
        // ==========================================
        // These will now read the safely reassigned tenant1/tenant2 tracking instances
        var testUsersConfigurationSeed = new[]
        {
            new { Email = "tenant1.admin@test.com", MyTenantId = tenant1.TenantId , TenantRole = "Admin" },
            new { Email = "tenant1.content@test.com", MyTenantId = tenant1.TenantId , TenantRole = "ContentManager" },
            new { Email = "tenant1.member@test.com", MyTenantId = tenant1.TenantId , TenantRole = "Member" },

            new { Email = "tenant2.admin@test.com", MyTenantId = tenant2.TenantId  , TenantRole = "Admin" },
            new { Email = "tenant2.content@test.com", MyTenantId = tenant2.TenantId  , TenantRole = "ContentManager" },
            new { Email = "tenant2.member@test.com", MyTenantId = tenant2.TenantId , TenantRole = "Member" }
        };

        foreach ( var config in testUsersConfigurationSeed )
        {
            var existingUser = await userManager.FindByEmailAsync(config.Email);

            if ( existingUser == null )
            {
                var newUser = new ApplicationUser
                {
                    UserName = config.Email,
                    Email = config.Email,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(newUser, "Focus@1nm");

                if ( createResult.Succeeded )
                {
                    _ = await userManager.AddToRoleAsync (newUser,"User");

                    var tenantMapping = new TenantUser
                    {
                        UserId = newUser.Id,
                        MyTenantId = config.MyTenantId,
                        TenantRole = config.TenantRole
                    };

                    _ = context.TenantUsers.Add (tenantMapping);
                }
            }
        }

        _ = await context.SaveChangesAsync ();
    }

    private static async Task PageSeed (ApplicationDbContext context,Guid seedTenancyId)
    {
        _ = context.Pages.Add (new Page (EnumPublicPage.Home,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.AdsDetail,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.Resources,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.CategoryButtonMarket,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.SubCategoryDropdownMarket,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.SpecialMarketButton,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.AllMarket,seedTenancyId,true));
        _ = context.Pages.Add (new Page (EnumPublicPage.NoticeAndNews,seedTenancyId,true));

        _ = await context.SaveChangesAsync ();
    }
}
