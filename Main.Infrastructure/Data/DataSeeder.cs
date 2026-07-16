using Domain.Model;

using Main.Common;
using Main.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class DataSeeder
{
    public static async Task SeedDataAsync (IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>> ( );
        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();




        // ==========================================
        // 1. SEED TENANTS AND PAGES
        // ==========================================



        // TENANT 1
        Tenant? tenant1 = context.Tenants.FirstOrDefault <Tenant> ( a => a.HostName == "lifestyle-local" );

        if ( tenant1 == null )
        {
            Tenant tenantNew1 = new ( )
            {
                Name = "LifeStyle Store",
                HostName = "lifestyle-local",
                Store = StoreType.LifeStyles
            };

            _ = context.Tenants.Add (tenantNew1);

            _ = await context.SaveChangesAsync ();

            tenantNew1.MyTenantId = tenantNew1.TenantId;
            _ = context.Update (tenantNew1);
            _ = await context.SaveChangesAsync ();


            Guid myTenantId2 = tenantNew1.TenantId;
            await PageSeed (context,myTenantId2);
        }


        // TENANT 2
        Tenant? tenant2 = context.Tenants.FirstOrDefault <Tenant> ( a => a.HostName == "fanarts-local" );

        if ( tenant2 == null )
        {

            Tenant tenantNew2 =  new()
            {
                Name = "Fine Arts Store",
                HostName = "fanarts-local",
                Store = StoreType.FineArts
            };

            _ = context.Tenants.Add (tenantNew2);
            _ = await context.SaveChangesAsync ();


            tenantNew2.MyTenantId = tenantNew2.TenantId;
            _ = context.Update (tenantNew2);
            _ = await context.SaveChangesAsync ();


            Guid myTenantId2 = tenantNew2.TenantId;
            await PageSeed (context,myTenantId2);
        }



        // ==========================================
        // 1. SEED GLOBAL ROLES
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
        // 2. SEED GLOBAL ADMIN
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
        // 3. SEED TENANT USERS WITH GLOBAL "User" ROLE
        // ==========================================
        // Define test users and their mapping details 

        var testUsersConfigurationSeed = new []
        {
            new { Email = "tenant1.admin@test.com", MyTenantId = tenant1!.TenantId , TenantRole = "Admin" },
            new { Email = "tenant1.content@test.com", MyTenantId = tenant1!.TenantId , TenantRole = "ContentManager" },
            new { Email = "tenant1.member@test.com", MyTenantId = tenant1!.TenantId , TenantRole = "Member" },

            new { Email = "tenant2.admin@test.com", MyTenantId = tenant2!.TenantId  , TenantRole = "Admin" },
            new { Email = "tenant2.content@test.com", MyTenantId = tenant2!.TenantId  , TenantRole = "ContentManager" },
            new { Email = "tenant2.member@test.com", MyTenantId = tenant2!.TenantId , TenantRole = "Member" }
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

                // Create the user globally
                var createResult = await userManager.CreateAsync(newUser, "Focus@1nm");

                if ( createResult.Succeeded )
                {
                    // Every regular person gets the global "User" identity role
                    _ = await userManager.AddToRoleAsync (newUser,"User");

                    // Link the user to their specific tenant and custom tenant role
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

        // Save all the UserTenants links to the database
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
