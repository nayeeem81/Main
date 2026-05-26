using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using IRepository;

namespace Main.Infrastructure;

public static class RegisterDatafrastructure
{
    public static IServiceCollection AddInfrastructureServices ( 
        this IServiceCollection services,IConfiguration configuration )
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Identity Context (User & Role)
        services.AddDbContext<ApplicationDbContext> ( options =>
        {
            options.UseSqlServer ( connectionString );
        } );

        services.AddIdentity<IdentityUser,IdentityRole> ( options =>
        {
            //SignIn
            options.SignIn.RequireConfirmedAccount = false;

            //Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes ( 5 );
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            //User
            options.User.RequireUniqueEmail = true;
        } )
        .AddEntityFrameworkStores<ApplicationDbContext> ( )
        .AddDefaultTokenProviders( )
        .AddSignInManager ( );

        services.ConfigureApplicationCookie ( options =>
        {
            options.LoginPath = "/Auth/Login";
            options.AccessDeniedPath = "/Auth/AccessDenied";
            options.Cookie.Name = "YourApp_AuthCookie";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes ( 30 );
            options.SlidingExpiration = true;
        } );
        //Identity Context End


        //(Web Application) Business DBContext 
        services.AddDbContext<BussinessAppDbContext> ( options =>
        {
            options.UseLazyLoadingProxies ( );
            options.UseSqlServer ( connectionString );
        } );
        

        //For console app
        //services.AddScoped<DbInitializer> ( );
        //services.AddScoped<IDatabaseSeeder,DatabaseSeeder> ( );

        //Register Repository
        services.AddScoped<IUserRepository,UserRepository> ( );
        services.AddScoped<IAdminPostImageRepository,AdminPostImageRepository> ( );
        services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );
        services.AddScoped<IProductImageRepository,ProductImageRepository> ( );
        services.AddScoped<IProductRepository,ProductRepository> ( );
        services.AddScoped<IPageRepository,PageRepository> ( );

        return services;

    }
}

