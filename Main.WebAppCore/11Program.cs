
using Common;
using Model;
using Data;
using FineArtsWebApp;
using FineArtsWebApp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using FineArtsWebApp.Service.Interface;
using FineArtsWebApp.Areas.PageContent.Data.Interfaces;
using FineArtsWebApp.Areas.PageContent.Data;
using FineArtsWebApp.Areas.CompanyContent.Data;
using FineArtsWebApp.Areas.AdminContent.Data;
using FineArtsWebApp.Areas.AdminContent.Data.Interfaces;
using FineArtsWebApp.Areas.CompanyContent.Data.Interfaces;
using FineArtsWebApp.Infrastructure.Helper;
using Data.Page.Interfaces;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDbContext<ApplicationDbContext>(options =>

               options.UseSqlServer(builder.Configuration

                                           .GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<IdentityUser,IdentityRole> ( options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes ( 5 );
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.RequireUniqueEmail = true;
} );

.AddEntityFrameworkStores<ApplicationDbContext>()


.AddDefaultTokenProviders();


builder.Services.AddDbContext<WebBusinessEntityContext>(options =>
                {

                    options.UseLazyLoadingProxies(); 

                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

                });


// Identity Application Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
   
    options.LoginPath = "/Auth/Login";

   
    options.AccessDeniedPath = "/Auth/AccessDenied";


    options.Cookie.Name = "YourApp_AuthCookie";


    options.Cookie.HttpOnly = true;


    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);


    options.SlidingExpiration = true; // Refresh cookie if used mid-session
});


builder.Services.AddControllersWithViews();


// Add authorization
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

});


builder.Services.AddMemoryCache(options =>
{

    options.SizeLimit = 1024;
    
    options.CompactionPercentage = 0.25; 

});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); 
   
    options.Cookie.HttpOnly = true;
   
    options.Cookie.IsEssential = true; // Required to work without GDPR consent
});


builder.Services.AddControllersWithViews(options =>
{

    options.CacheProfiles.Add("Cache1dayServerNBrowser",

        new CacheProfile()
        {
            Duration = 86400,

            Location = ResponseCacheLocation.Any
        });


    options.CacheProfiles.Add("Cache30Mins",

       new CacheProfile()
       {
           Duration = 1800,

           Location = ResponseCacheLocation.Any
       });


    options.CacheProfiles.Add("Cache2Mins",

       new CacheProfile()
       {
           Duration = 120,

           Location = ResponseCacheLocation.Any
       });


    options.CacheProfiles.Add("Cache60Mins",

       new CacheProfile()
       {
           Duration = 3600,

           Location = ResponseCacheLocation.Any
       });


    options.CacheProfiles.Add("Never",

        new CacheProfile()
        {
            Location = ResponseCacheLocation.None,

            NoStore = true
        });

});


builder.Services.AddWebOptimizer(pipeline =>
{

    pipeline.CompileLessFiles(); // Compiles all .less files in wwwroot

});


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddControllersWithViews()

                .AddViewLocalization()

                .AddDataAnnotationsLocalization(options => {

                    options.DataAnnotationLocalizerProvider = 
                    
                    (type, factory) => factory.Create(typeof(SharedResource));

                });


builder.Services.AddScoped<IAdminPostImageRepository, AdminPostImageRepository>();
builder.Services.AddScoped<IAdminPostRepository,AdminPostRepository> ( );
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository> ( )
builder.Services.AddScoped<IPageRepository,PageRepository> ( );
builder.Services.AddScoped<IAValueRepository,AValueRepository> ( );


builder.Services.AddScoped<IAdminPostDataService,AdminPostDataService> ( );
builder.Services.AddScoped<IAdminPostMappingService,AdminPostMappingService> ( );
builder.Services.AddScoped<IProductRepository,ProductRepository> ( );
builder.Services.AddScoped<IProductDataService,ProductDataService> ( );
builder.Services.AddScoped<IProductMappingService,ProductMappingService> ( );
builder.Services.AddScoped<IModelBaseService,ModelBaseService> ( );
builder.Services.AddScoped<IPageDataService,PageDataService> ( );
builder.Services.AddScoped<IPagePanelDataService,PagePanelDataService> ( );





builder.Services.AddScoped<IAccountBillRepository, UserAccountBillRepository>();
builder.Services.AddScoped<IPostAddressRepository, PostAddressRepository>();

builder.Services.AddScoped<IBikashBillTransactionRepository, BikashBillTransactionRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IFabiaProviderRepository, FabiaProviderRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IGroupPanelConfigRepository, GroupPanelConfigRepository>();
builder.Services.AddScoped<IGroupPanelPostRepository, GroupPanelPostRepository>();
builder.Services.AddScoped<ILogBrowserInfoRepository, LogBrowserInfoRepository>();
builder.Services.AddScoped<ILogServerErrorRepository, LogServerErrorRepository>();
builder.Services.AddScoped<ILogPostRepository, LogPostRepository>();
builder.Services.AddScoped<ILogPostVisitRepository, LogPostVisitRepository>();
builder.Services.AddScoped<ILogUserSessionRepository, LogUserSessionRepository>();
builder.Services.AddScoped<IUserMessageRepository, UserMessageRepository>();
builder.Services.AddScoped<IPackageConfigRepository, PackageConfigRepository>();
builder.Services.AddScoped<IPostCommentRepository, PostRepository>();
builder.Services.AddScoped<IPostQueryRepository, PostQueryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPriceConfigRepository, PriceConfigRepository>();
builder.Services.AddScoped<IShoppingCommand, ShoppingCommand>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserCreditOrderRepository, UserCreditOrderRepository>();
builder.Services.AddScoped<IUserOrderRepository, UserOrderRepository>();
builder.Services.AddScoped<IUserPackageRepository, UserPackageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBikashBillTransactionService, BikashBillTransactionService>();
builder.Services.AddScoped<IBillManagementService, BillManagementService>();
builder.Services.AddScoped<IBrowserSessionReportQueryService, BrowserSessionReportQueryService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEmailNotificationService, EmailService>();
builder.Services.AddScoped<IFabiaProviderService, FabiaProviderServService>();
builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<IManageAccountSettingService, ManageAccountSettingService>();
builder.Services.AddScoped<IPackageConfigurationService, PackageConfigurationService>();
builder.Services.AddScoped<IPaymentOptionService, PaymentOptionService>();
builder.Services.AddScoped<IPostMangementService, PostManagementService>();
builder.Services.AddScoped<IPostMappingService, PostMappingService>();
builder.Services.AddScoped<IPostVisitService, PostVisitService>();
builder.Services.AddScoped<IPriceConfigurationService, PriceConfigurationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IShoppingCommandService, ShoppingCommandService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IUserCreditOrderService, UserCreditOrderService>();
builder.Services.AddScoped<IUserMessageService, UserMessageService>();
builder.Services.AddScoped<IUserOrderService, UserOrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepoDropDownDataList, RepoDropDownDataList>();
builder.Services.AddScoped<IPostCommentRepository, PostRepository>();



builder.Logging.ClearProviders();


builder.Logging.AddConsole();


var app = builder.Build();


var supportedCultures = new[] { "en", "bn" };


var localizationOptions = new RequestLocalizationOptions()
    
                            .SetDefaultCulture(supportedCultures[0])
   
                            .AddSupportedCultures(supportedCultures)
  
                            .AddSupportedUICultures(supportedCultures);



app.UseRequestLocalization(localizationOptions);



GlobalSettings.Configuration = app.Configuration;


GlobalResources.Localizer = app.Services.GetRequiredService<IStringLocalizer<SharedResource>>();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();


app.UseWebOptimizer();


app.UseStaticFiles();


app.MapStaticAssets();


app.UseRouting();


app.UseCors();


app.UseSession();


app.UseAuthentication();


app.UseAuthorization();


app.MapControllers();


app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
